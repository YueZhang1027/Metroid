using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    #region player global state
    public enum PlayerStatus
    {
        Borning,
        Normal,
        Uncontrollable,
        Death
    }

    static PlayerStatus status = PlayerStatus.Borning;

    public static bool isMoveable()
    {
        return status == PlayerStatus.Normal;
    }

    IEnumerator BornToNormal()
    {
        //yield return new WaitForSeconds(5f); // borning animation time
        SetAndSendAnimatorStatus(PlayerStatus.Normal);
        yield break;
    }

    void SetAndSendAnimatorStatus(PlayerStatus newStatus)
    {
        status = newStatus;

    }
    #endregion

    #region morphed and standing state
    public GameObject standing;
    public GameObject morphed;
    static Animator curActiveAnimator;
    bool isStanding = true;

    public delegate void OnStandingStateChangeDelegate();
    public static OnStandingStateChangeDelegate standingStateChangeDelegate;

    void CheckSwitchState()
    {
        if ((isStanding && Input.GetKeyDown(KeyCode.DownArrow) && playerInventory.HasMorphBall) ||
            !isStanding && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            isStanding = !isStanding;
        }

        standing.SetActive(isStanding);
        morphed.SetActive(!isStanding);

        curActiveAnimator = isStanding ? standing.GetComponent<Animator>() : morphed.GetComponent<Animator>();
        standingStateChangeDelegate.Invoke();
    }

    public static Animator GetActiveAnimator()
    {
        return curActiveAnimator;
    }
    #endregion

    #region state UI
    PlayerInventory playerInventory;
    // Player health status
    float hp = 30f;
    public Text healthUI;
    public void HealthChange(float change)
    {
        hp += change;
        healthUI.text = ((int)hp).ToString();

        if (change < 0)
        {
            if (hp <= 0)
            {
                SetAndSendAnimatorStatus(PlayerStatus.Death);
                return;
            }
            // Player damage
        }
    }
    #endregion

    
    void Awake()
    {
        playerInventory = this.GetComponent<PlayerInventory>();
        StartCoroutine("BornToNormal");
    }

    void Update()
    {
        CheckSwitchState();
    }

}
