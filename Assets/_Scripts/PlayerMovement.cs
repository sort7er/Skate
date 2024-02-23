using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float minSpeed = 5;
    public float normalSpeed = 8;
    public float boostSpeed = 10;

    private Rigidbody2D rb;
    private GroundCheck check;

    private float currentSpeed;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        check = GetComponent<GroundCheck>();
        NormalSpeed();
    }

    private void BoostSpeed()
    {
        currentSpeed = boostSpeed;
    }
    private void NormalSpeed()
    {
        currentSpeed = normalSpeed;
    }

    private void FixedUpdate()
    {
        rb.AddForce(check.forwardDirection * currentSpeed, ForceMode2D.Force);
    }

    private void Update()
    {
        Debug.Log(rb.velocity.magnitude);
        SpeedControl();
    }

    private void SpeedControl()
    {
        if (rb.velocity.magnitude < minSpeed)
        {
            Vector2 speed = rb.velocity.normalized * minSpeed;
            rb.velocity = speed;
        }
    }
}
