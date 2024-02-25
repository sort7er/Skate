using DG.Tweening;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public float groundDistance = 0.3f;
    public Transform groundPosFront;
    public Transform groundPosBack;
    public LayerMask groundMask;


    private PlayerMovement playerMovement;
    private Transform groundPos;

    public Vector3 forwardDirection { get; private set; }

    public float slopeAngle { get; private set; }

    public bool isGrounded { get; private set; }

    private Vector3 normal;

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (IsGrounded())
        {
            CastRay(groundPos);
        }
    }

    public bool IsGrounded()
    {
        if (Physics2D.OverlapCircle(groundPosFront.position, groundDistance, groundMask))
        {
            groundPos = groundPosFront;
            return true;
        }
        else if(Physics2D.OverlapCircle(groundPosBack.position, groundDistance, groundMask))
        {
            groundPos = groundPosBack;
            return true;
        }
        else
        {
            isGrounded = false;
            return false;
        }
    }

    private void CastRay(Transform groundTrans)
    {
        RaycastHit2D hitDown = Physics2D.Raycast(groundTrans.position, -groundTrans.up, groundDistance, groundMask);
        if (hitDown.collider != null)
        {
            AllignWithGround(hitDown);
            isGrounded= true;
            return;
        }

        //RaycastHit2D hitForward = Physics2D.Raycast(groundTrans.position, groundTrans.right, groundDistance, groundMask);
        //if (hitForward.collider != null)
        //{
        //    AllignWithGround(hitForward);
        //    isGrounded = true;
        //    return;
        //}
        isGrounded = false;

    }

    private void AllignWithGround(RaycastHit2D hit)
    {
        normal = hit.normal;
        forwardDirection = Quaternion.Euler(0, 0, -90) * normal;

        Quaternion targetRot = Quaternion.LookRotation(Vector3.forward, normal);
        playerMovement.SetRotation(targetRot.eulerAngles.z);

        CalculateSlopeAngle(targetRot.eulerAngles.z);

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
