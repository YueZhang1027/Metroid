using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerState : MonoBehaviour
{
    PlayerInventory playerInventory;

    public GameObject standing;
    public GameObject morphed;

    bool isStanding = true;

    // Player health status
    int health = 30;
    public Text healthUI;

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


    void Awake()
    {
        playerInventory = this.GetComponent<PlayerInventory>();
        StartCoroutine("BornToNormal");
    }

    IEnumerator BornToNormal()
    {
        //yield return new WaitForSeconds(5f); // borning animation time
        SetAndSendAnimatorStatus(PlayerStatus.Normal);
        yield break;
    }

    void Update()
    {
        if (isStanding && Input.GetKeyDown(KeyCode.DownArrow) && playerInventory.HasMorphBall)
        {
            standing.SetActive(false);
            morphed.SetActive(true);
            isStanding = false;
        }

        if (!isStanding && (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.UpArrow)))
        {
            standing.SetActive(true);
            morphed.SetActive(false);

            isStanding = true;

            var dir = standing.GetComponentInParent<PlayerDirection>();
            dir?.StandAnimator?.SetBool("FaceRight", dir.IsFacingRight());

            Debug.Log(dir.IsFacingRight());
        }

        if (health <= 0)
        {
            SetAndSendAnimatorStatus(PlayerStatus.Death);
        }
    }

    public void HealthChange(int change)
    {
        if (change < 0)
        {
        }
    }


    void SetAndSendAnimatorStatus(PlayerStatus newStatus)
    {
        status = newStatus;

    }
}
