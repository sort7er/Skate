using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    public Vector3 forwardDirection { get; private set; }

    private void Update()
    {
        SetForwardDirection();
    }

    private void SetForwardDirection()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundMask);
        if (hit.collider != null)
        {
            forwardDirection = Quaternion.Euler(0,0, -90) * hit.normal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + forwardDirection);
    }
}
