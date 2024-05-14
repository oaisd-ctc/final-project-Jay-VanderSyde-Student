using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public LayerMask damageableLayer; // Layer mask for objects that can take damage
    public float raycastDistance = 1f; // Distance of the raycast to detect objects below the player

    private Rigidbody2D rb;
    private bool isGrounded = true;

    // Animation variables
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        // Get the Animator component
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Update animation based on player input and state
        UpdateAnimation(moveInput);

        if (isGrounded)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                Jump();
            }
        }
    }

    private void Jump()
    {
        // Apply jump force to the player
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

        // Check for objects below the player and apply damage if any
        RaycastHit2D hit = Physics2D.Raycast(transform.position + Vector3.up * 0.1f, Vector2.down, raycastDistance, damageableLayer);
        if (hit.collider != null)
        {
            // Get the health component of the object
            PlayerHealth objectHealth = hit.collider.GetComponent<PlayerHealth>();
            if (objectHealth != null)
            {
                // Deal damage to the object
                objectHealth.TakeDamage(1);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void UpdateAnimation(float moveInput)
    {
        // Update animation based on player movement
        if (moveInput != 0)
        {
            animator.SetBool("IsRunning", true);

            // Flip player sprite if moving left
            if (moveInput < 0)
            {
                FlipSprite(true);
            }
            // Flip player sprite if moving right
            else
            {
                FlipSprite(false);
            }
        }
        else
        {
            animator.SetBool("IsRunning", false);
        }
    }

    private void FlipSprite(bool facingLeft)
    {
        // Assuming the sprite renderer is attached to the same GameObject
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Flip the sprite based on direction
            spriteRenderer.flipX = facingLeft;
        }
    }
}
