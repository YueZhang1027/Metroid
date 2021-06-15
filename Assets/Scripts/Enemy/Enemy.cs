using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MovingDirection
{
    Down,
    Left,
    Up,
    Right
}

public class Enemy : MonoBehaviour
{
    [SerializeField] protected float hp = 0f;
    [SerializeField] protected Collider col = null;
    [SerializeField] protected Animator animator = null;

    [SerializeField] protected MovingDirection direction = MovingDirection.Down;

}
