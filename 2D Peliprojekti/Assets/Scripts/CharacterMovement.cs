using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] Transform overheadCheckCollider;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] BoxCollider2D standingCollider;
    [SerializeField] CircleCollider2D crouchingCollider;
    [SerializeField] float speed = 1;
    [SerializeField] float jumpPower = 250;

    const float groundCheckRadius = 0.2f;
   //const float overheadCheckRadius = 0.2f; // Vaihdoin tän Raycastiin
    float horizontalMovement;
    public float runValue = 1f;
    public float crouchValue = 0.5f;
    public float headCheckLength; //Raycast

    private bool isFacingRight = true;
    public bool isGrounded = false;
    public bool isRunning = false;
    public bool isCrouched = false;
    
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
        GroundCheck();
        Crouch();
        Flip();
        Jump();
        Run();
             
    }

    private void FixedUpdate()
    {
        Move();
        
    }

    void GroundCheck()
    {
        isGrounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheckCollider.position, groundCheckRadius, groundLayer);
        if (colliders.Length > 0)
        {
            isGrounded = true;
        }
    }

    void Move()
    {
        rb.velocity = new Vector2(horizontalMovement * speed * 100 * runValue * crouchValue  * Time.fixedDeltaTime, rb.velocity.y);
    }

    void Flip()
    {
        if (isFacingRight && horizontalMovement < 0f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
            isFacingRight = false;
        }
        else if (!isFacingRight && horizontalMovement > 0f)
        {
            transform.localScale = new Vector3(1, 1, 1);
            isFacingRight = true;
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0f, jumpPower));
        }
    }

    void Run()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
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
            runValue = 1f;
        }
        Debug.Log(rb.velocity.x);
        // 0 idle, 4 walk, 8 run

        animator.SetFloat("xVelocity", Mathf.Abs(rb.velocity.x));
    }

    void Crouch()
    {
        float yInput = Input.GetAxisRaw("Vertical");
        bool isHeadHitting = HeadDetect();

        if ((yInput < 0 || isHeadHitting) && isGrounded)
        {
            isCrouched = true;
            animator.SetBool("Crouch", true);
            standingCollider.enabled = false;
            crouchingCollider.enabled = true;
            crouchValue = 0.3f;
        }
        else //(!isHeadHitting)
        {
            isCrouched = false;
            animator.SetBool("Crouch", false);
            standingCollider.enabled = true;
            crouchingCollider.enabled = false;
            crouchValue = 1f;    
        }

        bool HeadDetect()
        {
            bool hit = Physics2D.Raycast(overheadCheckCollider.position, Vector2.up, headCheckLength, groundLayer);
            return hit;
        }

    }

    private void OnDrawGizmos()
    {
        if (overheadCheckCollider == null) return;

        Vector2 from = overheadCheckCollider.position;
        Vector2 to = new Vector2(overheadCheckCollider.position.x, overheadCheckCollider.position.y + headCheckLength);

        Gizmos.DrawLine(from, to);
    }


}