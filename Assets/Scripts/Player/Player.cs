using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(InputManager))]
public class Player : MonoBehaviour
{
    [Header("Scripts")]
    private InputManager inputManager;

    [Header("Components")]
    [HideInInspector]
    public Rigidbody2D rb;

    [Header("Input Values")]
    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;

    [Header("Movement")]
    public float xVelocity;

    [Header("Jump")]
    public bool canJump;
    public bool isGrounded;

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
    }


    private void PlayerInput()
    {
        horizontalInput = inputManager.PlayerAction.Movement.ReadValue<float>();
        verticalInput = inputManager.PlayerAction.Jump.ReadValue<float>();
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
