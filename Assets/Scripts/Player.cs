using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private bool isGrounded;
    private Transform groundCheck;
    private float groundCheckRadius = 0.1f;
    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        groundCheck = transform.Find("GroundCheck");
    }

    void Update()
    {
        // Check if the player is grounded
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);

        // Move the player left or right
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Flip the player sprite if moving left
        if (moveInput < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Flip the player sprite if moving right
        else if (moveInput > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}