using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;
    private BoxCollider2D boxcoll;
    

    [SerializeField] private LayerMask jumpableGround; 

    private float dirX;
    [SerializeField] private int initJumpCount = 3;
    private int jumpCount;

    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 20f;
    private enum MovementState { idle, running, jumping, falling };

    [SerializeField] private AudioSource jumpSoundEffect; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        boxcoll = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);

        // GetKeyDown is for hardcoded string like "space" or "a", "d"
        // GetButtonDown is for standardize name from Project Setting

        // Wukong Jump
        /**
        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, jumpForce);
        }
        **/

        if (Input.GetButtonDown("Jump") && jumpCount > 0)
        {
            jumpSoundEffect.Play(); 
            GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount--; 
        }

        UpdateAnimationState();
        IsGrounded();
    }

    private void UpdateAnimationState()
    {
        MovementState state;

        
        if (dirX > 0f)
        {
            state = MovementState.running;
            sprite.flipX = false;
        }
        else if (dirX < 0f)
        {
            state = MovementState.running;
            sprite.flipX = true;     
        }
        else
        {
            state = MovementState.idle;           
        }
        
        if (rb.velocity.y > .1f)
        {
            state = MovementState.jumping;
        }
        else if (rb.velocity.y < -.1f)
        {
            state = MovementState.falling;
        }

        anim.SetInteger("state", (int)state);
        
    }

    private bool IsGrounded()
    {
        bool isGrounded = Physics2D.BoxCast(boxcoll.bounds.center, boxcoll.bounds.size, 0f, Vector2.down, .1f, jumpableGround);
        if (isGrounded)
            jumpCount = initJumpCount;
        return isGrounded; 
    }
}
