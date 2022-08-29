using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rigid;
    Collider col;

    public float jumpPower = 15;
    Animator StandAnimator;

    public AudioClip JumpClip;

    void Awake()
    {
        rigid = this.GetComponentInParent<Rigidbody>();
        col = this.GetComponent<Collider>();
        StandAnimator = this.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (!PlayerState.isMoveable()) return;

        Vector3 newVelocity = rigid.velocity;
        bool isGrounded = IsGrounded();
        StandAnimator?.SetBool("IsStanding", isGrounded);

        if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
        {
            newVelocity.y = jumpPower;
            StandAnimator?.SetTrigger("Jump");
            AudioSource.PlayClipAtPoint(JumpClip, this.transform.position);
        }

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
