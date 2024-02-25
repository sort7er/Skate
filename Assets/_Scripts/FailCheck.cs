using System;
using UnityEngine;

public class FailCheck : MonoBehaviour
{
    public event Action OnDead;

    private GroundCheck check;

    private void Awake()
    {
        check = GetComponent<GroundCheck>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.gameObject.layer == 6)
        {
            if (!check.isGrounded)
            {
                if (!check.IsGrounded())
                {
                    OnDead?.Invoke();
                }
            }
        }
    }
}
