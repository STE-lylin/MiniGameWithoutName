using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4.0f;
    [SerializeField] float jumpForce = 7.5f;

    private int facingDirection = 1;
    private bool onGround=false;

    private ColliderSensor groundSensor;

    private Rigidbody2D body2d;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        groundSensor = transform.Find("GroundSensor").GetComponent<ColliderSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        float inputX = Input.GetAxis("Horizontal");
        animator.SetFloat("SpeedY", body2d.velocity.y);

        if (!onGround && groundSensor.CheckCollider())
        {
            onGround = true;
            animator.SetBool("OnGround", onGround);
        }
        else if (onGround && !groundSensor.CheckCollider())
        {
            onGround = false;
            animator.SetBool("OnGround", onGround);
        }

        if (inputX > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
            facingDirection = 1;
        }
        else if (inputX < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
            facingDirection = -1;
        }
        // ≈‹≤Ω
        body2d.velocity = new Vector2(inputX * moveSpeed, body2d.velocity.y);
        if (inputX != 0)
        {
            animator.SetBool("Running", true);
        }
        else if (inputX == 0)
        {
            animator.SetBool("Running", false);
        }

        // Ã¯‘æ
        if (Input.GetKeyDown("space") && onGround)
        {
            body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            onGround = false;
            animator.SetBool("OnGround", onGround);
            groundSensor.Disable(0.2f);
        }

        
    }
}
