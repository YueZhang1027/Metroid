using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// collectible item struct

public class PlayerInventory : MonoBehaviour
{
    public static PlayerInventory Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private bool hasMorphBall = false;
    public bool HasMorphBall
    {
        get { return hasMorphBall; }
        set { hasMorphBall = value; }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MorphBall")
        {
            Destroy(other.gameObject);
            HasMorphBall = true;
        }
    }

}
