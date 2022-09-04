using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RefillCollectable : Collectable
{
    public int refillValue;
    public AudioClip fetchClip;

    protected override void Collect() 
    {
        AudioSource.PlayClipAtPoint(fetchClip, transform.position);
        PlayerState.Instance.ReceiveCollectable(type, refillValue);
        Destroy(gameObject);
    }
}
