using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4.0f;
    [SerializeField] float jumpForce = 6.0f;
    [SerializeField] float rollForce = 6.0f;
    [SerializeField] float minAttackInterval = 0.25f;
    [SerializeField] float maxComboInterval = 1.0f;

    private int facingDirection = 1;
    private bool onGround = false;
    private int currentAttack = 0;
    private float timeSinceLastAttack = 0;
    public bool isAttacking = false;
    public bool isBlocking = false;
    public bool isRolling = false;

    private int health = 100;

    private ColliderSensor groundSensor;
    private ColliderSensor wallSensorL;
    private ColliderSensor wallSensorR;

    private Rigidbody2D body2d;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        body2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        groundSensor = transform.Find("GroundSensor").GetComponent<ColliderSensor>();
        wallSensorL = transform.Find("WallSensorL").GetComponent<ColliderSensor>();
        wallSensorR = transform.Find("WallSensorR").GetComponent<ColliderSensor>();
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
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
        // ÅÜ²½
        if (!isAttacking && !isBlocking)
        {
            if ((inputX > 0 && !wallSensorR.CheckCollider()) || (inputX < 0 && !wallSensorL.CheckCollider()))
            {
                body2d.velocity = new Vector2(inputX * moveSpeed, body2d.velocity.y);
                animator.SetBool("Running", true);
            }

            else
            {
                animator.SetBool("Running", false);
            }
        }
        

        // ÌøÔ¾
        if (Input.GetKeyDown("space") && onGround)
        {
            body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            onGround = false;
            animator.SetBool("OnGround", onGround);
            groundSensor.Disable(0.2f);
        }

        //¹¥»÷
        if (Input.GetMouseButtonDown(0) && (timeSinceLastAttack>=minAttackInterval))
        {
            if (timeSinceLastAttack < maxComboInterval) 
            {
                currentAttack++;
                if (currentAttack > 3) currentAttack = 1;
            } 
            else currentAttack = 1;
            animator.SetTrigger("Attack" + currentAttack);
            timeSinceLastAttack = 0;
        }

        // ¸ñµ²
        if (Input.GetMouseButtonDown(1))
        {
            isBlocking = true;
            animator.SetBool("Block", isBlocking);
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isBlocking = false;
            animator.SetBool("Block", isBlocking);
        }

        // ·­¹ö
        if (Input.GetKeyDown("left shift") && !isAttacking && !isBlocking)
        {
            body2d.velocity = new Vector2(inputX * rollForce, body2d.velocity.y);
            animator.SetTrigger("Roll");    
        }


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("Hurt");
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            animator.SetTrigger("Hurt");
        }
    }
}
