using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDirection : MonoBehaviour
{
    public CapsuleCollider capsuleCollider;

    bool facingRight = true;
    bool lookingUp = false;

    void FixedUpdate()
    {
        if (!PlayerState.Instance.isMoveable()) return;

        bool holdingUp = Input.GetKey(KeyCode.UpArrow);
        if (lookingUp && !holdingUp)
        {
            lookingUp = false;

            capsuleCollider.center = Vector3.zero;
            capsuleCollider.height = 2f;
            
        }
        else if (!lookingUp && holdingUp)
        {
            lookingUp = true;

            capsuleCollider.center = new Vector3(0, 0.2f, 0);
            capsuleCollider.height = 2.4f;
        }

        PlayerAnimatorManager.Instance.CurActiveAnimator.SetBool("HoldingUp", lookingUp);
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
