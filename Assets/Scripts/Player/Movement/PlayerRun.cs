using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : MonoBehaviour
{
    Rigidbody rigid;
    Animator animator;

    public float moveSpeed = 5;

    void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
        PlayerState.standingStateChangeDelegate += FetchActiveAnimator;
    }

    void FetchActiveAnimator()
    {
        animator = PlayerState.GetActiveAnimator();
    }

    void FixedUpdate()
    {
        if (!PlayerState.isMoveable()) return;

        float horizontalMove = Input.GetAxis("Horizontal") * moveSpeed;
        animator?.SetFloat("Speed", Mathf.Abs(horizontalMove));

        Vector3 newVelocity = rigid.velocity;
        newVelocity.x = horizontalMove;
        rigid.velocity = newVelocity;
    }

}
