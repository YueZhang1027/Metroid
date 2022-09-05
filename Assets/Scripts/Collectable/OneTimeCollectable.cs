using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeCollectable : Collectable
{
    public AudioClip firstTimeClip;
    WaitForSeconds audioPlayTime;

    void Start() 
    {
        audioPlayTime = new WaitForSeconds(firstTimeClip.length);
    }

    protected override void Collect()
    {
        // Set player stop moving

        PlayerInventory.Instance.SetCollectableStatus(type);
        StartCoroutine(PlayFirstTimeAudio());
    }

    IEnumerator PlayFirstTimeAudio() 
    {
        PlayerState.Instance.SetPlayerStatus(PlayerStatus.Uncontrollable);
        AudioManager.Instance.Pause();
        AudioSource.PlayClipAtPoint(firstTimeClip, transform.position);

        yield return audioPlayTime;
        PlayerState.Instance.SetPlayerStatus(PlayerStatus.Normal);
        AudioManager.Instance.UnPause();

        Destroy(gameObject);
    }
    
}
