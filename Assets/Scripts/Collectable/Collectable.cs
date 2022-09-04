using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum CollectableType
{
    MorphBall,
    Health,
    Missile
}

public class Collectable : MonoBehaviour
{
    public CollectableType type;

    void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // functional 
        Collect();
    }

    protected virtual void Collect() { }
}
