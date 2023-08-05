using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite; 
    private float dirX;
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private float jumpForce = 14f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>(); 
    }

    // Update is called once per frame
    void Update()
    {
        dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y); 

        // GetKeyDown is for hardcoded string like "space" or "a", "d"
        // GetButtonDown is for standardize name from Project Setting

        if (Input.GetButtonDown("Jump"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        UpdateAnimationState(); 

    }

    private void UpdateAnimationState()
    {
        if (dirX > 0f)
        {
            sprite.flipX = false;
            anim.SetBool("tr_running", true);
        }
        else if (dirX < 0f)
        {
            sprite.flipX = true;
            anim.SetBool("tr_running", true);        
        }
        else
        {
            sprite.flipX = false;
            anim.SetBool("tr_running", false);
        }
    }
}
