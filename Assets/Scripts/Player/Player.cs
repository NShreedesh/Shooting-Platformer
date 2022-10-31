using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputManager))]
public class Player : MonoBehaviour
{
    [Header("Scripts")]
    private InputManager inputManager;

    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Input Values")]
    private float horizontalInput;
    private float verticalInput;

    [Header("Movement")]
    [SerializeField]
    private float xVelocity;
    [SerializeField]
    private float speed = 5;
    [SerializeField]
    private float speedMultiplier = 1;

    [Header("Jump")]
    [SerializeField]
    private float jumpForce = 5;
    [SerializeField]
    private bool canJump;
    [SerializeField]
    private bool isGrounded;

    [Header("Raycasting")]
    [SerializeField]
    private Transform groundCheckPoint;
    [SerializeField]
    private LayerMask groundMask;
    [SerializeField]
    private float radius;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        isGrounded = IsGrounded();
        PlayerInput();
        MovementValues();
        Flip();
    }

    private void FixedUpdate()
    {
        if(isGrounded)
        {
            rb.velocity = new Vector2(xVelocity * Time.deltaTime, rb.velocity.y);
        }
        else
        {
            if(horizontalInput == 0)
            {
                rb.velocity = new Vector2(rb.velocity.x / 1.01f, rb.velocity.y);
            }
            else
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y);
            }
        }

        if(canJump && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce * Time.deltaTime);
            isGrounded = false;
            canJump = false;
        }
    }


    private void PlayerInput()
    {
        horizontalInput = inputManager.PlayerAction.Movement.ReadValue<float>();
        verticalInput = inputManager.PlayerAction.Jump.ReadValue<float>();
    }

    private void MovementValues()
    {
        xVelocity = Mathf.MoveTowards(xVelocity, horizontalInput * speed, Time.deltaTime * speed * speedMultiplier);

        if (verticalInput > 0 && isGrounded)
        {
            canJump = true;
        }
    }

    private void Flip()
    {
        if (horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private bool IsGrounded()
    {
        Collider2D hitInfo = Physics2D.OverlapCircle(groundCheckPoint.position, radius, groundMask);
        return hitInfo != null;
    }

    private void OnDrawGizmos()
    {
        if (groundCheckPoint == null) return;
        Gizmos.DrawWireSphere(groundCheckPoint.position, radius);
    }
}
