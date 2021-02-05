using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody rigid;
    public float moveSpeed = 5;
    public float jumpPower = 15;

    private void Awake()
    {
        rigid = this.GetComponent<Rigidbody>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newVelocity = rigid.velocity;

        newVelocity.x = Input.GetAxis("Horizontal") * moveSpeed;
        if (Input.GetKeyDown(KeyCode.Z) && IsGrounded())
            newVelocity.y = jumpPower;

        rigid.velocity = newVelocity;
    }

    bool IsGrounded()
    {
        Collider col = this.GetComponentInChildren<Collider>();

        Ray ray = new Ray(col.bounds.center, Vector3.down);
        float radius = col.bounds.extents.x - .05f;
        float fullDistance = col.bounds.extents.y + .05f;

        if (Physics.SphereCast(ray, radius, fullDistance)) return true;

        return false;
    }
}
