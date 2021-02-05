using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;
    public CapsuleCollider capsuleCollider;

    public Sprite spriteLookingForward;
    public Sprite spriteLookingUpward;

    bool facingRight = true;
    bool lookingUp = false;

    public Animator StandAnimator;

    void Update()
    {
        float horizontalAxis = Input.GetAxis("Horizontal");
        if (facingRight && horizontalAxis < 0)
        {
            facingRight = false;
            //this.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (!facingRight && horizontalAxis > 0)
        {
            facingRight = true;
            //this.transform.localScale = new Vector3(1, 1, 1);
        }
        StandAnimator?.SetBool("FaceRight", facingRight);
        StandAnimator?.SetFloat("HorizontalInput", horizontalAxis);

        bool holdingUp = Input.GetKey(KeyCode.UpArrow);
        if (lookingUp && !holdingUp)
        {
            lookingUp = false;
            //spriteRenderer.sprite = spriteLookingForward;

            capsuleCollider.center = new Vector3(0, -0.1f, 0);
            capsuleCollider.height = 1.8f;
            
        }
        else if (!lookingUp && holdingUp)
        {
            lookingUp = true;
            spriteRenderer.sprite = spriteLookingUpward;

            capsuleCollider.center = new Vector3(0, 0.2f, 0);
            capsuleCollider.height = 2.4f;
        }

        StandAnimator?.SetBool("HoldingUp", lookingUp);
    }

    public bool IsFacingRight()
    {
        return facingRight;
    }

    public bool IsLookingUp()
    {
        return lookingUp;
    }
}
