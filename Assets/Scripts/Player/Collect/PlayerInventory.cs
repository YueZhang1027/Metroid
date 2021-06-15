using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
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
