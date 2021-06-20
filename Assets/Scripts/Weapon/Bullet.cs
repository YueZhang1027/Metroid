using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
    public float destroyTime;
    WaitForSeconds wait = new WaitForSeconds(1f);

    void Start()
    {
        Destroy(this.gameObject, destroyTime);
    }

    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DestroyAfterOneSecond());
    }

    IEnumerator DestroyAfterOneSecond()
    {
        // Wait for enemy trigger to proceed damage
        this.gameObject.SetActive(false);
        yield return wait;
        Destroy(this.gameObject);
    }
}
