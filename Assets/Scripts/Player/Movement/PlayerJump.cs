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

    bool Jump = false;
    bool isGrounded = true;

    private void FixedUpdate()
    {
        if (!PlayerState.isMoveable()) return;

        Vector3 newVelocity = rigid.velocity;
        isGrounded = IsGrounded();

        if (Jump)
        {
            Debug.Log("PerformJump");

            StandAnimator?.SetBool("IsJumping", true);
            AudioSource.PlayClipAtPoint(JumpClip, this.transform.position);
            newVelocity.y = jumpPower;
            isGrounded = false;
            Jump = false;
        }

        StandAnimator.SetBool("IsGrounded", isGrounded);

        if (isGrounded)
        {
            StandAnimator?.SetBool("IsJumping", false);
        }

        rigid.velocity = newVelocity;
    }

    void Update()
    {
        if (!PlayerState.isMoveable()) return;

        isGrounded = IsGrounded();
        if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
        {
            Jump = true;
        }
    }

    bool IsGrounded()
    {
        bool isGrounded = Physics.Raycast(col.bounds.center, 
                               Vector3.down, 
                               col.bounds.extents.y + .05f, 
                               1 << 10);
        return isGrounded || (Mathf.Abs(rigid.velocity.x) < 0.01f && Mathf.Abs(rigid.velocity.y) < 0.01f);
    }
}
