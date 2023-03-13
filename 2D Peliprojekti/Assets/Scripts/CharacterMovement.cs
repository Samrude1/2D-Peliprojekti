using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] LayerMask groundLayer;
    const float groundCheckRadius = 0.2f;
    float horizontalMovement;
    [SerializeField] float speed = 1;
    [SerializeField] float jumpPower = 250;
    private bool isFacingRight = true;
    public bool isGrounded = false;
    public bool isRunning = false;
    public float runValue = 1f;
    Animator animator;

    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        Flip();
        Jump();
        Run();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        Move();
        
    }

    void GroundCheck()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0 )
        {
            isGrounded = true;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(horizontalMovement * speed * 100 * runValue * Time.fixedDeltaTime, rb.velocity.y);
        
    }

    void Flip()
    {
       

        if(isFacingRight && horizontalMovement < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingRight = false;
        }
        else if(!isFacingRight && horizontalMovement > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isFacingRight = true;
        }
       
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded= false;
            rb.AddForce(new Vector2(0f, jumpPower));
        }
        
    }

    void Run()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            isRunning = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isRunning = false;
        }

        if (isRunning)
        {
            runValue = 2f;
        }
        else
        {
            runValue= 1f;
        }
        Debug.Log(rb.velocity.x);
        // 0 idle, 4 walk, 8 run

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));

    }





}
