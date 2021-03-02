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
        Normal,
        Borning,
        Hurt,
        Transferring,
        Death
    }

    PlayerStatus status = PlayerStatus.Borning;

    void Awake()
    {
        playerInventory = this.GetComponent<PlayerInventory>();
        StartCoroutine("BornToNormal");
    }

    IEnumerator BornToNormal()
    {
        yield return new WaitForSeconds(10f); // borning animation time
        SetAndSendAnimatorStatus(PlayerStatus.Normal);
    }

    void Update()
    {
        if (isStanding && Input.GetKeyDown(KeyCode.DownArrow) && playerInventory.HasMorphBall())
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
