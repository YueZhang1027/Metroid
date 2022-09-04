using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float moveSpeed = 6f;
    float jumpAmount = 15f;

    float fallScale = 2.5f;
    float jumpScale = 2f;

    public AudioClip JumpClip;
    public AudioClip RunClip;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerState.Instance.isMoveable()) 
        {
            rb.velocity = new Vector3();
            return;
        }

        #region run
        Vector3 newVelocity = rb.velocity;

        //detect collision from wall
        float horizontalMovement = Input.GetAxis("Horizontal");
        newVelocity.x = (!PlayerState.Instance.MeetWall(horizontalMovement)) ?
                horizontalMovement * moveSpeed : 0.0f;
        PlayerAnimatorManager.Instance.CurActiveAnimator.SetFloat("HorizontalInput", newVelocity.x);

        #endregion

        #region jump

        // cite: https://www.youtube.com/watch?v=7KiK0Aqtmzc

        // use vector instead of speed
        if ((Input.GetKeyDown(KeyCode.Z) && PlayerState.Instance.canJump())) 
        {
            newVelocity += Vector3.up * jumpAmount;
            AudioSource.PlayClipAtPoint(JumpClip, transform.position);
            PlayerAnimatorManager.Instance.IsRunJump = Mathf.Abs(newVelocity.x) >= 0.01f;
        }

        newVelocity += Vector3.up * Physics.gravity.y * (newVelocity.y < 0 ? fallScale : jumpScale - 1) * Time.deltaTime;

        

        // limit player falling velocity;

        //rb.velocity = new Vector3(rb.velocity.x, Mathf.Max(-maxFallingVelocity, rb.velocity.y), 0.0f);
        #endregion

        rb.velocity = newVelocity;
    }
}
