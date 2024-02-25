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
    public float rotationDeceleration;
    public float maxRotation = 10;
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

    public GameObject skateboard;
    public Transform trails;
    public float actuallSpeed { get; private set; }


    private Rigidbody2D rb;
    private GroundCheck check;
    private FailCheck fail;
    private PlayerAction currentAction;

    private float targetRotation;
    private float currentSpeed;
    private float speedMultiplier;

    private bool isDead;
    private bool forceDown;
    private float angularVelocityThreshold = 0.2f;


    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<GroundCheck>();
        fail = GetComponent<FailCheck>();
        fail.OnDead += Dead;
        Release();
    }

    private void OnDisable()
    {
        fail.OnDead -= Dead;
    }

    private void Dead()
    {
        isDead = true;
        skateboard.transform.parent = null;
        Release();
        trails.gameObject.SetActive(false);

        skateboard.AddComponent<Rigidbody2D>();
        skateboard.GetComponent<Rigidbody2D>().collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }


    private void FixedUpdate()
    {
        if (isDead)
        {
            return;
        }

        if (check.isGrounded)
        {
            rb.angularVelocity = 0;
            rb.rotation = Mathf.LerpAngle(rb.rotation, targetRotation, Time.deltaTime * 30);
        }
        else
        {
            if (currentAction == PlayerAction.Right)
            {
                rb.AddTorque(-rotationSpeed, ForceMode2D.Force);
            }
            else if (currentAction == PlayerAction.Left)
            {
                rb.AddTorque(rotationSpeed, ForceMode2D.Force);
            }
            else if (currentAction == PlayerAction.None)
            {
                if (rb.angularVelocity < 0 - angularVelocityThreshold)
                {
                    rb.angularVelocity += Time.deltaTime * rotationDeceleration;
                }
                else if (rb.angularVelocity > 0 + angularVelocityThreshold)
                {
                    rb.angularVelocity -= Time.deltaTime * rotationDeceleration;
                }
                else
                {
                    rb.angularVelocity = 0;
                }
            }

        }

        rb.AddForce(check.forwardDirection * currentSpeed, ForceMode2D.Force);

        if (forceDown)
        {
            rb.AddForce(Vector2.down * downSpeed, ForceMode2D.Force);
        }

    }

    private void Update()
    {      
        if(isDead)
        { 
            return; 
        }
        DetermineSpeed();
        SpeedControl();

        if(currentAction == PlayerAction.Down)
        {
            Down();
        }
        else if(currentAction == PlayerAction.None)
        {
            speedMultiplier = 1;
        }

        trails.rotation = Quaternion.LookRotation(Vector3.forward, rb.velocity);
        trails.Rotate(Vector3.forward * 90);
    }
    private void Down()
    {
        if (check.isGrounded)
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
        if (!check.isGrounded)
        {
            currentSpeed -= airDecrease * Time.deltaTime;
        }
        else
        {

            float remapedSlope = Tools.Remap(check.slopeAngle, -90, 90, 1, -1);
            currentSpeed += remapedSlope * slopeMultiplier * speedMultiplier;
        }
        //Debug.Log(currentSpeed);
        //Debug.Log(rb.angularVelocity);
    }

    private void SpeedControl()
    {
        if (check.isGrounded)
        {
            DirectionCheck();
        }


        if (actuallSpeed < minSpeed)
        {
            Vector2 speed = rb.velocity.normalized * minSpeed;
            rb.velocity = speed;
        }
        else if (rb.velocity.magnitude > maxSpeed)
        {
            Vector2 speed = rb.velocity.normalized * maxSpeed;
            rb.velocity = speed;
        }

        if(rb.angularVelocity > maxRotation)
        {
            rb.angularVelocity = maxRotation;
        }
        else if(rb.angularVelocity < -maxRotation)
        {
            rb.angularVelocity = -maxRotation;
        }
    }

    private void DirectionCheck()
    {
        if (Vector2.Dot(transform.right, rb.velocity) < 0)
        {
            Vector2 speed = rb.velocity.normalized * minSpeed * - 1;
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

    public void SetRotation(float rotation)
    {
        targetRotation = rotation;
    }

}
