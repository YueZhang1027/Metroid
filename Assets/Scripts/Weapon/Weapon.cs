using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] protected float damageAmount = 0f;
    public float GetDamage()
    {
        return damageAmount;
    }



}
