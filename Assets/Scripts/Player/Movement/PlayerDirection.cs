using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    /*private static Controls controls;
    public static Controls Controls
    {
        get
        {
            if (controls != null) return controls;
            return controls = new Controls();
        }
    }*/

    public SpriteRenderer spriteRenderer;
    public CapsuleCollider capsuleCollider;


    public bool facingRight = true;
    bool lookingUp = false;


    public Animator StandAnimator;

    void Update()
    {
        if (!PlayerState.isMoveable()) return;

        float horizontalAxis = Input.GetAxis("Horizontal");
        if ((facingRight && horizontalAxis < 0) || (!facingRight && horizontalAxis > 0))
        {
            facingRight = !facingRight;
            this.transform.localScale = facingRight ? new Vector3(1, 1, 1) : new Vector3(-1, 1, 1);
        }

        bool holdingUp = Input.GetKey(KeyCode.UpArrow);
        if (lookingUp && !holdingUp)
        {
            lookingUp = false;
            //spriteRenderer.sprite = spriteLookingForward;

            capsuleCollider.center = Vector3.zero;
            capsuleCollider.height = 2f;
            
        }
        else if (!lookingUp && holdingUp)
        {
            lookingUp = true;
            //spriteRenderer.sprite = spriteLookingUpward;

            capsuleCollider.center = new Vector3(0, 0.2f, 0);
            capsuleCollider.height = 2.4f;
        }


        if (lookingUp) StandAnimator.SetLayerWeight(1, 1.0f);
        else StandAnimator.SetLayerWeight(1, 0.0f);
        //StandAnimator?.SetBool("HoldingUp", lookingUp);
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
