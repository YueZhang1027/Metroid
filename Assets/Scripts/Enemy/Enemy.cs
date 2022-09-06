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
    [SerializeField] public int hp;
    [SerializeField] public int atk;
    [SerializeField] protected Collider col = null;
    [SerializeField] protected Animator animator = null;

    [SerializeField] protected MovingDirection direction = MovingDirection.Down;

    [SerializeField] protected SpriteRenderer spriteRenderer;

    protected bool isHurt = false;
    WaitForSeconds hurtStopTime = new WaitForSeconds(0.3f);

    public void ReceiveDamage(int damage) 
    {
        if (isHurt) return;

        hp -= damage;
        // stop for a moment, change color
        if (hp <= 0) OnDeath();
        else StartCoroutine(StopForHurt());
    }

    IEnumerator StopForHurt() 
    {
        isHurt = true;
        spriteRenderer.color = Color.red;
        yield return hurtStopTime;
        spriteRenderer.color = Color.white;
        isHurt = false;
    }

    protected void OnDeath() 
    {
        // Play Death animation

        // Destroy enemy
        Destroy(this.gameObject);
    }
}
