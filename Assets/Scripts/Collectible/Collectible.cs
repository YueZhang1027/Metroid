using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    public enum CollectibleType
    {
        MorphBall,
        Health,
        Missile
    }

    public CollectibleType type;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Destroy(this.gameObject);
    }
}
