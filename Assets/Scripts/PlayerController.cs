using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 4.0f;
    [SerializeField] float jumpForce = 7.5f;
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
        // �ܲ�
        if (!isAttacking && !isBlocking)
        {
            body2d.velocity = new Vector2(inputX * moveSpeed, body2d.velocity.y);
            if (inputX != 0)
            {
                animator.SetBool("Running", true);
            }
            else if (inputX == 0)
            {
                animator.SetBool("Running", false);
            }
        }
        

        // ��Ծ
        if (Input.GetKeyDown("space") && onGround)
        {
            body2d.velocity = new Vector2(body2d.velocity.x, jumpForce);
            animator.SetTrigger("Jump");
            onGround = false;
            animator.SetBool("OnGround", onGround);
            groundSensor.Disable(0.2f);
        }

        //����
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

        // ��
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

        // ����
        if (Input.GetKeyDown("left shift") && !isAttacking && !isBlocking)
        {
            body2d.velocity = new Vector2(inputX * rollForce, body2d.velocity.y);
            animator.SetTrigger("Roll");    
        }


    }
}
