using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    PlayerDirection playerDirection;

    public GameObject bulletPrefab;

    public Transform firingPositionForward;
    public Transform firingPositionUpward;

    public float firingSpeed = 10f;

    void Awake()
    {
        playerDirection = this.GetComponentInParent<PlayerDirection>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject bulletInstance = GameObject.Instantiate(bulletPrefab);

            if (playerDirection.IsLookingUp())
            {
                bulletInstance.transform.position = firingPositionUpward.position;
                bulletInstance.GetComponent<Rigidbody>().velocity = Vector3.up * firingSpeed;
            }
            else
            {
                bulletInstance.transform.position = firingPositionForward.position;
                if (playerDirection.IsFacingRight())
                {
                    bulletInstance.GetComponent<Rigidbody>().velocity = Vector3.right * firingSpeed;
                }
                else
                {
                    bulletInstance.GetComponent<Rigidbody>().velocity = Vector3.left * firingSpeed;
                }
            }
        }
    }
}
