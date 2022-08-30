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

    private void FixedUpdate()
    {
        SetAnimatorLayer();
    }

    void SetAnimatorLayer()
    {
        for (int i = 0; i < CurActiveAnimator.layerCount; ++i)
            CurActiveAnimator.SetLayerWeight(i, 0);


        if (!PlayerState.Instance.IsGrounded()) CurActiveAnimator.SetLayerWeight(2, 1);
        else if (Mathf.Abs(Input.GetAxis("Horizontal")) <= 0.01) CurActiveAnimator.SetLayerWeight(0, 1);
        else CurActiveAnimator.SetLayerWeight(1, 1);
    }
}
