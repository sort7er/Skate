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

    [Header("Crouch")]
    public float crouchMultiplierDown;
    public float crouchMultiplierUp;
    public Transform graphicTrans;

    [Header("Speed")]
    public float slopeMultiplier = 0.1f;
    public float minSpeed = 5;
    public float maxSpeed = 30;
    public float airDecrease = 1f;

    private Rigidbody2D rb;
    private GroundCheck check;

    private float currentSpeed;
    private float speedMultiplier;
    private PlayerAction currentAction;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<GroundCheck>();
        Release();
    }


    private void FixedUpdate()
    {
        rb.AddForce(check.forwardDirection * currentSpeed, ForceMode2D.Force);
    }

    private void Update()
    {      
        DetermineSpeed();
        SpeedControl();

        if (currentAction == PlayerAction.Left)
        {
            Debug.Log("RotateLeft");
        }
        else if(currentAction == PlayerAction.Right)
        {
            Debug.Log("RotateRight");
        }
        else if(currentAction == PlayerAction.Down)
        {
            if (check.slopeAngle > 0)
            {
                speedMultiplier = crouchMultiplierUp;
            }
            else
            {
                speedMultiplier = crouchMultiplierDown;
            }
        }
        else
        {
            speedMultiplier = 1;
        }
    }

    private void DetermineSpeed()
    {
        if (rb.velocity.magnitude < minSpeed)
        {
            currentSpeed = minSpeed;
            return;
        }
        if (rb.velocity.magnitude > maxSpeed)
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
        if (rb.velocity.magnitude < minSpeed || rb.velocity.x < minSpeed)
        {
            Vector2 speed = rb.velocity.normalized * minSpeed;
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
        }

        currentAction = PlayerAction.None;
    }

}
