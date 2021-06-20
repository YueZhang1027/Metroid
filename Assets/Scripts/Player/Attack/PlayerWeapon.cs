using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    PlayerDirection playerDirection;
    Animator StandAnimator;

    public GameObject bulletPrefab;

    public Transform firingPositionForward;
    public Transform firingPositionUpward;
    public Transform firingPositionBackward;
    public Transform firingPositionUpwardLeft;

    public float firingSpeed = 10f;

    void Awake()
    {
        playerDirection = this.GetComponentInParent<PlayerDirection>();
        StandAnimator = this.GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject bulletInstance = GameObject.Instantiate(bulletPrefab);
            bulletInstance.name = "Bullet";

            bool lookUp = playerDirection.IsLookingUp();
            bool faceRight = playerDirection.IsFacingRight();

            if (lookUp)
            {
                bulletInstance.transform.position = firingPositionUpward.position;
                bulletInstance.GetComponent<Rigidbody>().velocity = Vector3.up * firingSpeed;
            }
            else
            {
                bulletInstance.transform.position = firingPositionForward.position;
                if (faceRight)
                {
                    bulletInstance.GetComponent<Rigidbody>().velocity = Vector3.right * firingSpeed;
                }
                else
                {
                    bulletInstance.GetComponent<Rigidbody>().velocity = Vector3.left * firingSpeed;
                }
            }
        }

        if (ExistBullet()) StandAnimator?.SetBool("IsFiring", true);
        else StandAnimator?.SetBool("IsFiring", false);
    }

    bool ExistBullet()
    {
        return GameObject.FindGameObjectsWithTag("Bullet").Length > 0;
    }
}
