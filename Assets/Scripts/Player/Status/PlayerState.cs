using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;

public enum PlayerStatus
{
    Borning,
    Normal,
    Uncontrollable,
    Hurt,
    Death
}

public enum PlayerShape
{
    Original = 0,
    MorphBall = 1
}

public class PlayerState : MonoBehaviour
{
    public static PlayerState Instance { get; private set; }

    // Player health status
    int health = 30;

    public GameObject[] playerShapes;
    PlayerShape shape = PlayerShape.Original;
    PlayerStatus status = PlayerStatus.Normal;

    Collider col;

    #region init
    void Awake()
    {
        Instance = this;

        //variables
        environmentMask = LayerMask.GetMask("Environment");

        StartCoroutine("Born");
    }

    private void Start()
    {
        SetUpShapeChange();
    }

    IEnumerator Born()
    {
        //yield return new WaitForSeconds(5f); // borning animation time
        //SetAndSendAnimatorStatus(PlayerStatus.Normal);
        yield break;
    }
    #endregion

    void Update()
    {
        if (!isMoveable()) return;

        if (shape != PlayerShape.MorphBall && Input.GetKeyDown(KeyCode.DownArrow) && PlayerInventory.Instance.HasCollectable(CollectableType.MorphBall))
        {
            SetUpShapeChange(PlayerShape.MorphBall);
        }
        else if (shape != PlayerShape.Original && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            SetUpShapeChange(PlayerShape.Original);
        }

        switch (shape)
        {
            case PlayerShape.Original:
                UpdateOriginal();
                break;
            case PlayerShape.MorphBall:
                UpdateMorphed();
                break;
        }
    }

    #region update player status
    public void SetPlayerStatus(PlayerStatus newStatus) => status = newStatus;
    public PlayerStatus GetStatus() { return status; }

    #endregion

    #region update player shapes
    public PlayerShape GetShape() { return shape; }

    private void UpdateOriginal()
    {
        if (!isMoveable()) return;

        CapsuleCollider capsuleCollider = playerShapes[(int)PlayerShape.Original].GetComponent<CapsuleCollider>();

        facingRight = Input.GetAxis("Horizontal") > 0.01 || (facingRight && Input.GetAxis("Horizontal") > -0.01f);

        bool holdingUp = Input.GetKey(KeyCode.UpArrow) && CanHoldUp();

        //TODO: check collider height and cast for holdup availability

        if (lookingUp && !holdingUp)
        {
            lookingUp = false;

            capsuleCollider.center = Vector3.zero;
            capsuleCollider.height = 2f;

        }
        else if (!lookingUp && holdingUp)
        {
            lookingUp = true;

            capsuleCollider.center = new Vector3(0, 0.2f, 0);
            capsuleCollider.height = 2.4f;
        }

        if (shape == PlayerShape.Original) PlayerAnimatorManager.Instance.CurActiveAnimator.SetBool("HoldingUp", lookingUp);
    }

    void UpdateMorphed() 
    {
        PlayerAnimatorManager.Instance.CurActiveAnimator.speed = Mathf.Abs(Input.GetAxis("Horizontal")) < 0.01 ? 0.0f : 1.0f;
    }

    void SetUpShapeChange(PlayerShape newShape = PlayerShape.Original)
    {
        playerShapes[(int)shape].SetActive(false);
        shape = newShape;
        playerShapes[(int)shape].SetActive(true);
        

        PlayerAnimatorManager.Instance.CurActiveAnimator = playerShapes[(int)shape].GetComponent<Animator>();
        col = playerShapes[(int)shape].GetComponent<Collider>();
    }
    #endregion

    #region collectables
    public void ReceiveCollectable(CollectableType type, int amount) 
    {
        switch (type) 
        {
            case CollectableType.Health:
                HealthChange(amount);
                break;
        }
    }

    public void HealthChange(int amount)
    {
        if (amount < 0)
        {
            health = Mathf.Max(health + amount, 0);
            UIManager.Instance.SetHealth(health);

            if (health <= 0)
            {
                StartCoroutine(OnDeath());
                return;
            }

            StartCoroutine(StopForHurt());
        }
        else 
        {
            //Play Sound
            health = Mathf.Min(health + amount, 30);
            UIManager.Instance.SetHealth(health);
        }
    }

    float hurtWaitTime = 0.4f;

    IEnumerator StopForHurt()
    {
        SetPlayerStatus(PlayerStatus.Hurt);
        PlayerAnimatorManager.Instance.CurActiveAnimator.SetTrigger("Hurt");
        yield return new WaitForSeconds(hurtWaitTime);
        SetPlayerStatus(PlayerStatus.Normal);
    }

    IEnumerator OnDeath() 
    {
        SetPlayerStatus(PlayerStatus.Death);
        PlayerAnimatorManager.Instance.CurActiveAnimator.SetTrigger("Death");
        yield return AudioManager.Instance.PlayAudioWithKey("Death");

        UIManager.Instance.OnDeath();

        if (Input.anyKey) 
        {
            SceneManager.LoadScene("Main");
        }
    }
    #endregion

    #region physic parameter query
    public bool isMoveable()
    {
        return status == PlayerStatus.Normal;
    }

    public bool isHurtOrDeath() 
    {
        return status == PlayerStatus.Hurt || status == PlayerStatus.Death;
    }

    bool CanHoldUp() 
    {
        Ray ray = new Ray(col.bounds.center, Vector3.up);
        float fullDistance = 1.2f;

        return !Physics.Raycast(ray, fullDistance, environmentMask);
    }

    public bool CanJump()
    {
        return isMoveable() && shape == PlayerShape.Original && IsGrounded();
    }

    bool lookingUp = false;
    bool facingRight = true;
    int environmentMask;

    public bool MeetWall(float x)
    {
        Ray ray = new Ray(col.bounds.center, x > 0 ? Vector3.right : Vector3.left);
        float fullDistance = col.bounds.extents.x + .05f;

        return Physics.Raycast(ray, fullDistance, environmentMask);
    }

    public bool IsGrounded()
    {
        // Physics spherecast from layer?

        Ray ray = new Ray(col.bounds.center, Vector3.down);
        float radius = col.bounds.extents.x - .05f;
        float fullDistance = col.bounds.extents.y + .05f;

        return Physics.SphereCast(ray, radius, fullDistance, environmentMask);
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }

    public bool IsLookingUp()
    {
        return lookingUp;
    }

    #endregion
}
