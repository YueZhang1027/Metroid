using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rigid;
    Collider col;

    public float jumpPower = 15;

    void Awake()
    {
        rigid = this.GetComponentInParent<Rigidbody>();
        col = this.GetComponent<Collider>();
    }

    void Update()
    {
        Vector3 newVelocity = rigid.velocity;

        if (Input.GetKeyDown(KeyCode.Z) && IsGrounded())
            newVelocity.y = jumpPower;

        rigid.velocity = newVelocity;
    }

    bool IsGrounded()
    {
        Ray ray = new Ray(col.bounds.center, Vector3.down);
        float radius = col.bounds.extents.x - .05f;
        float fullDistance = col.bounds.extents.y + .05f;

        if (Physics.SphereCast(ray, radius, fullDistance)) return true;
        else return false;
    }
}
