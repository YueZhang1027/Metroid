using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        collectableCounter = new HashSet<CollectableType>();
    }

    HashSet<CollectableType> collectableCounter;

    public bool HasCollectable(CollectableType type) { return collectableCounter.Contains(type); }
    public void SetCollectableStatus(CollectableType type) { collectableCounter.Add(type); }
}
