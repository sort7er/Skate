using DG.Tweening;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public enum PlayerAction
    {
        Down, 
        Left, 
        Right,
        None
    }

    [Header("In air")]
    public float rotationSpeed;
    public float downSpeed;

    [Header("Crouch")]
    public float crouchMultiplierDown;
    public float crouchMultiplierUp;
    public Transform graphicTrans;

    [Header("Speed")]
    public float slopeMultiplier = 0.1f;
    public float minSpeed = 5;
    public float maxSpeed = 30;
    public float airDecrease = 1f;

    public float actuallSpeed { get; private set; }



    private Rigidbody2D rb;
    private GroundCheck check;
    private PlayerAction currentAction;

    private float currentSpeed;
    private float speedMultiplier;

    private bool forceDown;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<GroundCheck>();
        Release();
    }


    private void FixedUpdate()
    {
        rb.AddForce(check.forwardDirection * currentSpeed, ForceMode2D.Force);

        if(forceDown)
        {
            rb.AddForce(Vector2.down * downSpeed, ForceMode2D.Force);
        }
    }

    private void Update()
    {      
        DetermineSpeed();
        SpeedControl();

        if (currentAction == PlayerAction.Left)
        {
            if (!check.IsGrounded())
            {
                Rotate(1);
            }
        }
        else if(currentAction == PlayerAction.Right)
        {
            if (!check.IsGrounded())
            {
                Rotate(-1);
            }
        }
        else if(currentAction == PlayerAction.Down)
        {
            Down();
        }
        else
        {
            speedMultiplier = 1;
        }
    }
    private void Rotate(float direction)
    {
        transform.Rotate(Vector3.forward * direction * rotationSpeed * Time.deltaTime);
    }
    private void Down()
    {
        if (check.IsGrounded())
        {
            if (check.slopeAngle > 0)
            {
                speedMultiplier = crouchMultiplierUp;
            }
            else
            {
                speedMultiplier = crouchMultiplierDown;
            }
            forceDown = false;
        }
        else
        {
            forceDown = true;
        }
    }

    private void DetermineSpeed()
    {
        actuallSpeed = rb.velocity.magnitude;

        if (actuallSpeed < minSpeed)
        {
            currentSpeed = minSpeed;
            return;
        }
        if (actuallSpeed > maxSpeed)
        {
            return;
        }
        if (!check.IsGrounded())
        {
            currentSpeed -= airDecrease * Time.deltaTime;
        }
        else
        {

            float remapedSlope = Tools.Remap(check.slopeAngle, -90, 90, 1, -1);
            currentSpeed += remapedSlope * slopeMultiplier * speedMultiplier;
        }
        Debug.Log(currentSpeed);
    }

    private void SpeedControl()
    {
        if (actuallSpeed < minSpeed)
        {
            float directionCheck;
            if(rb.velocity.x < 0)
            {
                directionCheck = -1;
            }
            else
            {
                directionCheck = 1;
            }

            Vector2 speed = rb.velocity.normalized * minSpeed * directionCheck;
            rb.velocity = speed;
        }
        else if (rb.velocity.magnitude > maxSpeed)
        {
            Vector2 speed = rb.velocity.normalized * maxSpeed;
            rb.velocity = speed;
        }
    }

    public void Crouch()
    {
        currentAction= PlayerAction.Down;
        graphicTrans.DOScaleY(0.5f, 0.1f);

    }
    public void RotateLeft()
    {
        currentAction = PlayerAction.Left;

    }
    public void RotateRight()
    {
        currentAction = PlayerAction.Right;
    }
    public void Release()
    {
        if(currentAction == PlayerAction.Down)
        {
            graphicTrans.DOScaleY(1, 0.1f);
            forceDown = false;
        }

        currentAction = PlayerAction.None;
    }

}
