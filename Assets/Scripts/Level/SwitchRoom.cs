using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRoom : MonoBehaviour
{
    public GameObject curCam;
    public GameObject nextCam;

    public SpriteRenderer[] doorSpriteRenderers; // Add going into door animation

    WaitForSeconds transportSeconds = new WaitForSeconds(2f);

    public Transform newSpawnPosition;

    private void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.tag);

        if (collision.CompareTag("Player") && Input.GetKeyDown(KeyCode.X)) 
        {
            //transportation corountine
            StartCoroutine(Transport());
        }
    }

    IEnumerator Transport() 
    {
        PlayerState.Instance.SetPlayerStatus(PlayerStatus.Uncontrollable);
        nextCam.SetActive(true);
        curCam.SetActive(false);

        yield return transportSeconds;
        PlayerState.Instance.transform.position = newSpawnPosition.position;
        PlayerState.Instance.SetPlayerStatus(PlayerStatus.Normal);
    }
}
