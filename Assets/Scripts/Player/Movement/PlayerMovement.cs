using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rb;
    float moveSpeed = 6f;

    float jumpAmount = 17f;
    public AudioClip JumpClip;

    float gravity = 5.0f;
    float gravityScale = 1.0f;
    float fallingGravityScale = 1.2f;

    // Start is called before the first frame update
    void Awake()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!PlayerState.Instance.isMoveable()) return;

        #region run
        Vector3 newVelocity = rb.velocity;

        //detect collision from wall
        newVelocity.x = !PlayerState.Instance.MeetWall(Input.GetAxis("Horizontal")) ? 
                Input.GetAxis("Horizontal") * moveSpeed : 0.0f;
        PlayerAnimatorManager.Instance.CurActiveAnimator.SetFloat("HorizontalInput", newVelocity.x);
        rb.velocity = newVelocity;
        #endregion

        #region jump
        rb.AddForce(Vector3.down * gravity * (newVelocity.y < 0 ? fallingGravityScale : gravityScale));

        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (PlayerState.Instance.canJump())
            {
                rb.AddForce(Vector3.up * jumpAmount, ForceMode.Impulse);
                //PlayerAnimatorManager.Instance.CurActiveAnimator.SetTrigger("Jump");
                AudioSource.PlayClipAtPoint(JumpClip, this.transform.position);

                PlayerAnimatorManager.Instance.isRunJump = Mathf.Abs(newVelocity.x) >= 0.01f;
                //Debug.Log(PlayerAnimatorManager.Instance.isRunJump);
            }
        }
        #endregion
    }
}
