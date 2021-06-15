using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    Rigidbody rigid;

    public float moveSpeed = 5;

    void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (!PlayerState.isMoveable()) return;
        Vector3 newVelocity = rigid.velocity;

        newVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;

        rigid.velocity = newVelocity;
    }
}
