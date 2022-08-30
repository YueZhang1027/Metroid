using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerStatus
{
    Borning,
    Normal,
    Uncontrollable,
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
    public Text healthUI;

    public GameObject[] playerShapes;
    PlayerShape shape = PlayerShape.Original;
    PlayerStatus status = PlayerStatus.Normal;

    Collider col;

    public bool isMoveable()
    {
        return status == PlayerStatus.Normal;
    }


    void Awake()
    {
        Instance = this;
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

    void FixedUpdate()
    {
        if (shape != PlayerShape.MorphBall && Input.GetKeyDown(KeyCode.DownArrow) && PlayerInventory.Instance.HasMorphBall)
        {
            playerShapes[(int)shape].SetActive(false);
            playerShapes[(int)PlayerShape.MorphBall].SetActive(true);
            shape = PlayerShape.MorphBall;

            SetUpShapeChange();
        }
        else if (shape != PlayerShape.Original && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            playerShapes[(int)shape].SetActive(false);

            playerShapes[(int)PlayerShape.Original].SetActive(true);
            shape = PlayerShape.Original;

            SetUpShapeChange();
        }

        if (health <= 0)
        {
            //SetAndSendAnimatorStatus(PlayerStatus.Death);
        }

        switch (shape)
        {
            case PlayerShape.Original:
                UpdateOriginal();
                break;
        }
    }

    private void UpdateOriginal()
    {
        if (!isMoveable()) return;

        CapsuleCollider capsuleCollider = playerShapes[(int)PlayerShape.Original].GetComponent<CapsuleCollider>();

        facingRight = Input.GetAxis("Horizontal") > 0.01 || (facingRight && Input.GetAxis("Horizontal") > -0.01f);

        bool holdingUp = Input.GetKey(KeyCode.UpArrow);
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

        PlayerAnimatorManager.Instance.CurActiveAnimator.SetBool("HoldingUp", lookingUp);
    }

    void SetUpShapeChange()
    {
        PlayerAnimatorManager.Instance.CurActiveAnimator = playerShapes[(int)shape].GetComponent<Animator>();
        col = playerShapes[(int)shape].GetComponent<Collider>();
    }

    public void HealthChange(int change)
    {
        if (change < 0)
        {
        }
    }


    #region physic parameter query
    bool lookingUp = false;
    bool facingRight = true;

    public bool IsGrounded()
    {
        Ray ray = new Ray(col.bounds.center, Vector3.down);
        float radius = col.bounds.extents.x - .05f;
        float fullDistance = col.bounds.extents.y + .05f;

        if (Physics.SphereCast(ray, radius, fullDistance)) return true;
        else return false;
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
