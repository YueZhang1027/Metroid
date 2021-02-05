using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassUpTriggerEnterToInventory : MonoBehaviour
{
    PlayerInventory playerInventory;

    void Awake()
    {
        playerInventory = this.GetComponentInParent<PlayerInventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        playerInventory.OnTriggerEnter(other);
    }
}
