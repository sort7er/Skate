using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float groundDistance = 0.3f;
    public LayerMask groundMask;

    public Vector3 forwardDirection { get; private set; }

    public float slopeAngle { get; private set; }

    private Vector3 normal;

    private void Update()
    {
        if (IsGrounded())
        {
            CastRay();
        }
    }

    public bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(transform.position, groundDistance, groundMask))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void CastRay()
    {
        RaycastHit2D hitDown = Physics2D.Raycast(transform.position, Vector2.down, groundDistance, groundMask);
        if (hitDown.collider != null)
        {
            AllignWithGround(hitDown);
            return;
        }

        RaycastHit2D hitForward = Physics2D.Raycast(transform.position, Vector2.right, groundDistance, groundMask);
        if (hitForward.collider != null)
        {
            AllignWithGround(hitForward);
            return;
        }

    }

    private void AllignWithGround(RaycastHit2D hit)
    {
        normal = hit.normal;
        forwardDirection = Quaternion.Euler(0, 0, -90) * normal;
        transform.rotation = Quaternion.LookRotation(Vector3.forward, normal);

        CalculateSlopeAngle(transform.rotation.eulerAngles.z);

    }

    private void CalculateSlopeAngle(float rawAngle)
    {
        if(rawAngle < 180)
        {
            slopeAngle = rawAngle;
        }
        else
        {
            slopeAngle = rawAngle - 360;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + forwardDirection);

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + normal);
    }
}
