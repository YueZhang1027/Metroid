using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRoom : MonoBehaviour
{
    public GameObject curCam;
    public GameObject nextCam;

    public bool toRight = true;
    int playerLayer;

    public SpriteRenderer[] doorSpriteRenderers; // Add going into door animation

    WaitForSeconds transportSeconds = new WaitForSeconds(2f);

    bool isTransporting = false;
    Vector3 direction;



    private void Awake()
    {
        playerLayer = LayerMask.GetMask("Player");
        direction = toRight ? Vector3.left : Vector3.right;
    }

    private void Update()
    {
        //Debug.Log(transform.position);
        //Debug.Log(Physics.BoxCast(transform.position, , direction, playerLayer));

        if (!isTransporting && Physics.Raycast(transform.position, direction, 1.1f, playerLayer) && Input.GetKeyDown(KeyCode.X)) 
        {
            StartCoroutine(Transport());
        }
    }

    IEnumerator Transport() 
    {
        isTransporting = true;
        // Trigger Player Float gesture

        PlayerState.Instance.SetPlayerStatus(PlayerStatus.Uncontrollable);
        nextCam.SetActive(true);
        curCam.SetActive(false);

        yield return transportSeconds;
        // gradually go right


        /*PlayerState.Instance.transform.position = newSpawnPosition.position;*/
        PlayerState.Instance.SetPlayerStatus(PlayerStatus.Normal);
        isTransporting = false;
    }
}
