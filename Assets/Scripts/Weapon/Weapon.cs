using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public int damage = 5;
    public float destroyTime = 0.35f;

    private void Start()
    {
        if (destroyTime > 0.0f) Destroy(gameObject, destroyTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<Enemy>().ReceiveDamage(damage);
            // play animation
        }

        Destroy(gameObject);
    }
}
