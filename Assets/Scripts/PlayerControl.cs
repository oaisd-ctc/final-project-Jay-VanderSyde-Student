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
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Item"))
        {
            // Call the LevelManager to load the next level
            LevelManager.Instance.LoadNextLevel();

            // Destroy the item
            Destroy(other.gameObject);
        }
    }
}
