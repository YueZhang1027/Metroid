using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    Rigidbody rigid;

    public float jumpPower = 15;

    public AudioClip JumpClip;

    void Awake()
    {
        rigid = this.GetComponentInParent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (!PlayerState.Instance.isMoveable()) return;

        Vector3 newVelocity = rigid.velocity;
        bool isGrounded = PlayerState.Instance.IsGrounded();
        //StandAnimator?.SetBool("IsStanding", isGrounded);

        if (Input.GetKeyDown(KeyCode.Z) && isGrounded)
        {
            newVelocity.y = jumpPower;
            PlayerAnimatorManager.Instance.CurActiveAnimator.SetTrigger("Jump");
            AudioSource.PlayClipAtPoint(JumpClip, this.transform.position);
        }

        rigid.velocity = newVelocity;
    }

    
}
