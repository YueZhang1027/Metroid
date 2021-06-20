using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTriggerEnter : MonoBehaviour
{
    WaitForSeconds wait = new WaitForSeconds(1f);
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(DestroyAfterOneSecond());
    }

    IEnumerator DestroyAfterOneSecond()
    {
        this.gameObject.SetActive(false);
        yield return wait;
        Destroy(this.gameObject);
    }
}
