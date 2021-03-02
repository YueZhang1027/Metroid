using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    public bool hasMorphBall = false;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MorphBall")
        {
            Destroy(other.gameObject);
            hasMorphBall = true;
        }
    }

    public bool HasMorphBall()
    {
        return hasMorphBall;
    }
}
