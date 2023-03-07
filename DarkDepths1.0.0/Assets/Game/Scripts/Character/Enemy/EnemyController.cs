using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : CharacterController
{
    [Header("Movement")]
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    Vector2 moveDirection = new Vector2();
    [SerializeField]
    private List<Vector2> currentPath = new List<Vector2>();


    [Header("Vision")]
    [SerializeField, Range(0.5f, 10f)]
    private float visionRange = 4f;
    [SerializeField, Range(0, 360)]
    private float visionAngle = 90;
    [SerializeField, Range(0.5f, 2f)]
    private float detectionRange = 1;

    [SerializeField]
    private LayerMask targetLayer;
    [SerializeField]
    private LayerMask obstructionLayer;
    [SerializeField]
    private LayerMask bomb;

    public Vector2 MoveDirection { get => moveDirection; set => moveDirection = value; }
    public List<Vector2> CurrentPath { get => currentPath; set => currentPath = value; }


    protected override void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (health > 0)
        {
            Move();
        }
    }

    public void Move()
    {
        if (currentPath.Count > 0)
        {
            moveDirection = (Vector2)transform.position - currentPath[currentPath.Count - 1];
            moveDirection.Normalize();

            transform.position = Vector2.MoveTowards(transform.position, currentPath[currentPath.Count - 1], moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, currentPath[currentPath.Count - 1]) < 0.01f)
            {
                currentPath.Remove(currentPath[currentPath.Count - 1]);
            }
        }
    }
    public bool IsBombAhead()
    {
        return Physics2D.OverlapCircle(transform.position, detectionRange, bomb);
    }
    public bool IsTargetVisible()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, visionRange, targetLayer);

        return IsVisible(rangeCheck);
    }
    private bool IsVisible(Collider2D[] rangeCheck)
    {

        if (rangeCheck.Length <= 0)
        {
            return false;
        }

        Transform firstTarget = rangeCheck[0].transform;
        Vector2 toTarget = firstTarget.position - transform.position;
        Vector2 visionDirection = VisionDirection();

        if (Vector2.Angle(visionDirection, toTarget) > visionAngle / 2)
        {
            return false;
        }

        float distanceToTarget = Vector2.Distance(transform.position, firstTarget.position);

        if (Physics2D.Raycast(transform.position, toTarget, distanceToTarget, obstructionLayer))
        {
            return false;
        }

        //Target = rangeCheck[0].gameObject;

        return true;
    }
    private Vector2 VisionDirection()
    {
        if (moveDirection != Vector2.zero)
        {
            var x = -Mathf.Round(moveDirection.x);
            var y = -Mathf.Round(moveDirection.y);

            return new Vector2(x, y);
        }

        return new Vector2(0, 0);
    }


    #region Gizmos
    private Color currentColor;
    public Color CurrentColor { get => currentColor; set => currentColor = value; }
    public void OnDrawGizmos()
    {
        foreach (var item in currentPath)
        {
            Gizmos.color = currentColor;
            Gizmos.DrawSphere(new Vector2(item.x, item.y), 0.15f);
        }

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, visionRange);

        Vector3 visionDirection = VisionDirection();
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, visionAngle / 2) * visionDirection * visionRange);
        Gizmos.DrawLine(transform.position, transform.position + Quaternion.Euler(0, 0, -visionAngle / 2) * visionDirection * visionRange);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectionRange);
    }
    #endregion
}
