using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    public static PlayerAnimatorManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private Animator curActiveAnimator;
    public Animator CurActiveAnimator
    {
        get { return curActiveAnimator; }
        set { curActiveAnimator = value; }
    }

    bool isRunJump = false;
    public bool IsRunJump 
    {
        set { isRunJump = value; }
    }

    private void FixedUpdate()
    {
        if (!PlayerState.Instance.isMoveable()) 
        {
            curActiveAnimator.speed = 0.0f;
            return;
        }

        curActiveAnimator.speed = 1.0f;

        if(PlayerState.Instance.GetShape() == PlayerShape.Original) SetAnimatorLayer();
    }

    void SetAnimatorLayer()
    {
        for (int i = 0; i < CurActiveAnimator.layerCount; ++i)
            CurActiveAnimator.SetLayerWeight(i, 0);


        if (!PlayerState.Instance.IsGrounded())
        {
            CurActiveAnimator.SetLayerWeight(isRunJump ? 3 : 2, 1);
        }
        else
        {
            //isRunJump = false;
            CurActiveAnimator.SetLayerWeight(Mathf.Abs(Input.GetAxis("Horizontal")) <= 0.01 ? 0 : 1, 1);
        }
    }
}
