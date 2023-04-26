using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMovement : MonoBehaviour
{
    Rigidbody2D rb;
    [SerializeField] Transform groundCheckCollider;
    [SerializeField] Transform overheadCheckCollider;
    [SerializeField] Transform wallCheckCollider;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] BoxCollider2D standingCollider;
    [SerializeField] CircleCollider2D crouchingCollider;
    [SerializeField] float speed = 1;
    [SerializeField] float jumpPower = 250;
    [SerializeField] GameObject damageParticles;
    [SerializeField] AudioClip footSteps, jump, wallJump, crouchSteps;

    const float groundCheckRadius = 0.2f;
    //const float overheadCheckRadius = 0.2f; // Vaihdoin t‰n Raycastiin
    float horizontalMovement;
    public float runValue = 1f;
    public float crouchValue = 0.5f;
    public float headCheckLength; //Raycast
    public int yMaxValue = 4;
    public int yMinValue = -4;
    public float wallSlidingSpeed;
    private float wallJumpingDirection;
    public float wallJumpingTime = 0.2f;
    private float wallJumpingCounter;
    public float wallJumpingDuration = 0.4f;

    public bool isWallSliding;
    private bool isFacingRight = true;
    public bool isGrounded = false;     //N‰‰ boolit on viel‰ kehitysvaiheessa public ni n‰kee mit‰ hahmo tekee pelin aikana
    public bool isWalking = false;
    public bool isCrouched = false;
    public bool isWallJumping;
    public Vector2 walljumpingPower = new Vector2(8f, 16f);
    public GameObject levelCompletePanel;
    public GameObject musicPlayer;
    AudioSource audioSource;
    Animator animator;
    Timer timer;
   
    // Start is called before the first frame update
    void Awake()
    {
        audioSource= GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = FindAnyObjectByType<Timer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal");
        Crouch();
        Jump();
        Walk();
        WallSlide();
        WallJump();

        if(!isWallJumping)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        Move();
        GroundCheck();   
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
        if(!isWallJumping)
        {
            rb.velocity = new Vector2(horizontalMovement * speed * 100 * runValue * crouchValue * Time.fixedDeltaTime, rb.velocity.y);
        }
        
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
        
        animator.SetFloat("yVelocity", rb.velocity.y);

        if (Input.GetButtonDown("Jump") && isGrounded && !isCrouched)
        {
            audioSource.clip = jump;
            audioSource.Play();
            rb.AddForce(new Vector2(0f, jumpPower * 100f));
            isGrounded = false;
            wallJumpingTime = 0;
            wallJumpingDuration = 0;
            wallJumpingCounter = 0;   
        }
        else if(!isGrounded)
        {
            wallJumpingTime = 0.2f;
            wallJumpingDuration = 0.4f;
            wallJumpingCounter = 0;
        }
        if(rb.velocity.y > yMaxValue || rb.velocity.y < yMinValue)   
        {
            animator.SetBool("Jump", true);
        }
        else 
        {
            animator.SetBool("Jump", false);
            
        }
        
    }

    void Walk()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isWalking = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isWalking = false;
        }

        if (isWalking)
        {
            runValue = 0.3f;
        }
        else
        {
            runValue = 1f;
        }
        Debug.Log(rb.velocity.x);
        // 0 idle, 4 walk, 8 run jos speed on 2 //blendtree arvot animatorissa (Moving tree)

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
        else
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

    private bool IsWalled()
    {
        return Physics2D.OverlapCircle(wallCheckCollider.position, 0.2f, wallLayer);   
    }

    private void WallSlide()
    {
        isWallSliding = false;
        if(IsWalled() && !isGrounded && horizontalMovement != 0 && rb.velocity.y < 0)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
            animator.SetBool("Sliding", true);
        }
        else
        {
            isWallSliding = false;
            animator.SetBool("Sliding", false);
        }
    }

    private void WallJump()
    {
        if(isWallSliding)
        {
            isWallJumping= false;
            wallJumpingDirection = -transform.localScale.x;
            wallJumpingCounter = wallJumpingTime;

            CancelInvoke(nameof(StopWallJumping));
        }
        else
        {
            wallJumpingCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump") && wallJumpingCounter > 0)
        {
            audioSource.clip = wallJump;
            audioSource.Play();
            isWallJumping = true;
            rb.velocity = new Vector2(wallJumpingDirection * walljumpingPower.x, walljumpingPower.y);
            wallJumpingCounter = 0f;

            if (transform.localScale.x != wallJumpingDirection)
            {
                isFacingRight = !isFacingRight;
                Vector3 localScale = transform.localScale;
                localScale.x *= -1f;
                transform.localScale = localScale;
            }
            Invoke(nameof(StopWallJumping), wallJumpingDuration);
        }
    }

    private void StopWallJumping()
    {
        isWallJumping = false;
    }

    private void OnDrawGizmos()
    {
        if (overheadCheckCollider == null) return;

        Vector2 from = overheadCheckCollider.position;
        Vector2 to = new Vector2(overheadCheckCollider.position.x, overheadCheckCollider.position.y + headCheckLength);

        Gizmos.DrawLine(from, to);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Hazard")
        {
            Instantiate(damageParticles, transform.position, Quaternion.identity);
            ScreenShakeController.instance.StartShake(0.2f, 0.2f);
        }

       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Goal")
        {
            timer.LogRecordtTime();
            levelCompletePanel.SetActive(true);
            Time.timeScale = 0;
            musicPlayer.SetActive(false);
        }
    }

   public void PlayFootsteps()
    {
        if (isGrounded)
        {
            audioSource.clip = footSteps;
            audioSource.Play();
        }
    }
   public void CrouchCteps()
    {
        audioSource.clip = crouchSteps;
        audioSource.Play();
    }



}