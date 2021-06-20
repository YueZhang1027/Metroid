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
    [SerializeField] protected float damage = 0f;
    [SerializeField] protected Collider col = null;
    [SerializeField] protected Animator animator = null;

    [SerializeField] protected MovingDirection direction = MovingDirection.Down;

    List<string> WeaponTagList = new List<string>()
    {
        "Bullet"
    };

    private void OnTriggerEnter(Collider collision)
    {
        Debug.Log(collision.gameObject);
        var tag = collision.gameObject.tag;
        if (tag == "Player")
        {
            // Hit player
            PlayerState playerState = collision.gameObject.GetComponent<PlayerState>();
            if (playerState)
            {
                playerState.HealthChange(-damage);
            }
            return;
        }

        if (WeaponTagList.Contains(tag))
        {
            Weapon weapon = collision.gameObject.GetComponent<Weapon>();
            if (weapon)
            {
                hp -= weapon.GetDamage();
                CheckDeath();
            }
            return;
        }
    }


    void CheckDeath()
    {
        if (hp > 0) return;

        //animator.Play
        Destroy(this.gameObject);
    }

}
