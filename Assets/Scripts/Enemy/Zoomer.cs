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
        PerformMovement();

        if (!isTurnLocked && IsDirectionAvailable(FindNextDirection(direction)))
        {
           StartCoroutine(PerformTurn());
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

    IEnumerator PerformTurn()
    {
        isTurnLocked = true;
        
        direction = FindNextDirection(direction);

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

        MovingDirection nextDirection = FindNextDirection(direction);
        while (IsDirectionAvailable(nextDirection))
        {
            yield return null;
        }

        isTurnLocked = false;
    }

    MovingDirection FindNextDirection(MovingDirection curDirection)
    {
        return curDirection == MovingDirection.Right ? MovingDirection.Down : direction + 1;
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
        Debug.Log(curDirection);
        float radius = IsHorizontalDirection(curDirection)? col.bounds.extents.y - .05f : col.bounds.extents.x - .05f;
        float fullDistance = IsHorizontalDirection(curDirection) ? col.bounds.extents.x + .05f : col.bounds.extents.y + .05f;

        if (Physics.SphereCast(ray, radius, fullDistance)) return false;
        else return true;
    }
}
