using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchRoom : MonoBehaviour
{
    public GameObject curCam;
    public GameObject nextCam;

    public bool toRight = true;
    float distance = 4f;
    int playerLayer;

    public SpriteRenderer[] doorSpriteRenderers; // Add going into door animation

    WaitForSeconds transportSeconds = new WaitForSeconds(2f);

    bool isTransporting = false;
    Vector3 direction;
    Vector3 halfExtents = new Vector3(0.5f, 1.5f, 0.05f);

    int slideTransitionSteps = 30;
    float slideTransitionDuration = 2f;

    Transform playerTransform;

    private void Awake()
    {
        playerTransform = GameObject.Find("Player").transform;
        playerLayer = LayerMask.GetMask("Player");
        direction = toRight ? Vector3.right : Vector3.left;
    }

    private void Update()
    {
        if (!isTransporting && Physics.OverlapBox(transform.position, halfExtents, Quaternion.identity, playerLayer).Length > 0 && Input.GetKeyDown(KeyCode.X)) 
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

        // gradually go right
        for (int i = 0; i < slideTransitionSteps; i++)
        {
            playerTransform.position += distance * direction / (float)slideTransitionSteps;
            yield return new WaitForSecondsRealtime(slideTransitionDuration / (float)slideTransitionSteps);
        }
        
        /*PlayerState.Instance.transform.position = newSpawnPosition.position;*/
        PlayerState.Instance.SetPlayerStatus(PlayerStatus.Normal);
        isTransporting = false;
    }
}
