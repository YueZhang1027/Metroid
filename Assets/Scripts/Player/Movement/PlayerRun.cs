using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// for both morph ball and original status

public class PlayerRun : MonoBehaviour
{
    Rigidbody rb;

    public float moveSpeed = 5;

    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!PlayerState.Instance.isMoveable()) return;
        Vector3 newVelocity = rb.velocity;

        newVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;

        PlayerAnimatorManager.Instance.CurActiveAnimator.SetFloat("HorizontalInput", newVelocity.x);

        rb.velocity = newVelocity;
    }
}
