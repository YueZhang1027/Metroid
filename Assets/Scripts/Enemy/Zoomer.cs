using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zoomer : Enemy
{
    [SerializeField] private float speed = 1f;

    private bool isTurnLocked = false;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        PerformMovement();

        if (isTurnLocked) return;

        Debug.Log(IsDirectionAvailable(direction));

        if (IsDirectionAvailable(direction))
        {
            if (IsDirectionAvailable(FindNextDirection(direction)))
            {
                StartCoroutine(PerformTurn(FindNextDirection(direction)));
            }
        }
        else 
        {
            StartCoroutine(PerformTurn(FindCounterDirection(FindNextDirection(direction))));
            // Find an available one
        }
    }

    void PerformMovement()
    {
        Vector3 pos = transform.position;
        switch (direction)
        {
            case MovingDirection.Down:
                pos = new Vector3(transform.position.x,
                                  transform.position.y - speed * Time.deltaTime,
                                  transform.position.z);
                break;
            case MovingDirection.Left:
                pos = new Vector3(transform.position.x - speed * Time.deltaTime,
                                  transform.position.y,
                                  transform.position.z);
                break;
            case MovingDirection.Up:
                pos = new Vector3(transform.position.x,
                                  transform.position.y + speed * Time.deltaTime,
                                  transform.position.z);
                break;
            case MovingDirection.Right:
                pos = new Vector3(transform.position.x + speed * Time.deltaTime,
                                  transform.position.y,
                                  transform.position.z);
                break;
        }
        transform.position = pos;
    }

    WaitForFixedUpdate wait = new WaitForFixedUpdate();

    IEnumerator PerformTurn(MovingDirection targetDirection)
    {
        isTurnLocked = true;
        
        direction = targetDirection;

        switch (direction)
        {
            case MovingDirection.Down:
                animator.SetTrigger("Down");
                break;
            case MovingDirection.Left:
                animator.SetTrigger("Left");
                break;
            case MovingDirection.Up:
                animator.SetTrigger("Up");
                break;
            case MovingDirection.Right:
                animator.SetTrigger("Right");
                break;
        }

        yield return wait;
        yield return wait;

        MovingDirection nextDirection = FindNextDirection(direction);

        while (IsDirectionAvailable(nextDirection))
        {
            Debug.Log(nextDirection);
            yield return null;
        }

        isTurnLocked = false;
    }

    MovingDirection FindNextDirection(MovingDirection curDirection)
    {
        return curDirection == MovingDirection.Right ? MovingDirection.Down : direction + 1;
    }

    MovingDirection FindCounterDirection(MovingDirection curDirection)
    {
        MovingDirection targetDirection = MovingDirection.Down;
        switch (curDirection) 
        {
            case MovingDirection.Down:
                targetDirection = MovingDirection.Up;
                break;
            case MovingDirection.Up:
                targetDirection = MovingDirection.Down;
                break;
            case MovingDirection.Left:
                targetDirection = MovingDirection.Right;
                break;
            case MovingDirection.Right:
                targetDirection = MovingDirection.Left;
                break;
        }
        return targetDirection;
    }

    bool IsHorizontalDirection(MovingDirection curDirection)
    {
        return curDirection == MovingDirection.Left ||
            curDirection == MovingDirection.Right;
    }

    bool IsDirectionAvailable(MovingDirection curDirection)
    {
        Vector3 targetDir = Vector3.zero;
        switch (curDirection)
        {
            case MovingDirection.Down:
                targetDir = Vector3.down;
                break;
            case MovingDirection.Left:
                targetDir = Vector3.left;
                break;
            case MovingDirection.Up:
                targetDir = Vector3.up;
                break;
            case MovingDirection.Right:
                targetDir = Vector3.right;
                break;
        }

        Ray ray = new Ray(col.bounds.center, targetDir);
        float radius = IsHorizontalDirection(curDirection)? col.bounds.extents.x - .05f : col.bounds.extents.y - .05f;
        float fullDistance = IsHorizontalDirection(curDirection) ? col.bounds.extents.x + .05f : col.bounds.extents.y +.05f;
        Debug.DrawRay(col.bounds.center, targetDir, Color.green);

        if (Physics.SphereCast(ray, radius, fullDistance, 1 << 10)) return false;
        else return true;
    }
}
