﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public GameObject bulletPrefab;

    public Transform firingPositionForward;
    public Transform firingPositionUpward;
    public Transform firingPositionBackward;
    public Transform firingPositionUpwardLeft;

    public float firingSpeed = 10f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameObject bulletInstance = GameObject.Instantiate(bulletPrefab);
            bulletInstance.name = "Bullet";

            bool lookUp = PlayerState.Instance.IsLookingUp();
            bool faceRight = PlayerState.Instance.IsFacingRight();

            if (lookUp)
            {
                bulletInstance.transform.position = firingPositionUpward.position;
                if (!faceRight) bulletInstance.transform.position = firingPositionUpwardLeft.position;
                bulletInstance.GetComponent<Rigidbody>().velocity = Vector3.up * firingSpeed;
            }
            else
            {
                bulletInstance.transform.position = firingPositionForward.position;
                if (!faceRight) bulletInstance.transform.position = firingPositionBackward.position;
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

        if (PlayerState.Instance.GetShape() == PlayerShape.Original) PlayerAnimatorManager.Instance.CurActiveAnimator.SetBool("Firing", ExistBullet());
    }

    bool ExistBullet()
    {
        return GameObject.FindGameObjectsWithTag("Bullet").Length > 0;
    }
}
