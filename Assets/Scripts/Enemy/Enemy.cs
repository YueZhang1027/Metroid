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
    [SerializeField] protected int hp = 15;
    [SerializeField] protected Collider col = null;
    [SerializeField] protected Animator animator = null;

    [SerializeField] protected MovingDirection direction = MovingDirection.Down;

    protected bool terminateMovement = false;
    WaitForSeconds hurtStopTime = new WaitForSeconds(0.25f);


    public void ReceiveDamage(int damage) 
    {
        hp -= damage;
        // stop for a moment, change color
        Debug.Log(hp);
        if (hp <= 0) OnDeath();
        else StartCoroutine(StopForHurt());
    }

    IEnumerator StopForHurt() 
    {

        terminateMovement = true;
        yield return hurtStopTime;
        terminateMovement = false;


    }

    protected void OnDeath() 
    {
        // Play Death animation

        // Destroy enemy
        Destroy(this.gameObject);
    }
}
