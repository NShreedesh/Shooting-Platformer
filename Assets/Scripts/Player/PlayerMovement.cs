using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Scripts")]
    private Player player;

    [Header("Other Scripts")]
    [SerializeField]
    private Crosshair crossHair;

    [Header("Components")]
    private CapsuleCollider2D playerCollider;

    [Header("Movement")]
    [SerializeField]
    private float speed;
    [SerializeField]
    private float crouchSpeed = 100;
    [SerializeField]
    private float runSpeed = 300;
    [SerializeField]
    private float groundedSpeedMultiplier = 5;
    [SerializeField]
    private float airborneSpeedMultiplier = 2;

    [Header("Jump")]
    [SerializeField]
    private float jumpForce = 400;

    [Header("Crouch")]
    private Vector2 standColliderOffset;
    private Vector2 standColliderSize;
    private Vector2 standGroundCheckPosition;
    [SerializeField]
    private Vector2 crouchColliderOffset = new Vector2(0, -0.4f);
    [SerializeField]
    private Vector2 crouchColliderSize = new Vector2(0.5f, 0.5f);
    [SerializeField]
    private Vector2 crouchGroundCheckPosition = new Vector2(0, 0.2f);

    private void Awake()
    {
        player = GetComponent<Player>();
        playerCollider = GetComponent<CapsuleCollider2D>();

        standColliderOffset = playerCollider.offset;
        standColliderSize = playerCollider.size;
        standGroundCheckPosition = player.groundCheckPoint.localPosition;
    }

    private void Update()
    {
        MovementValues();
        Crouch();
    }

    private void FixedUpdate()
    {
        SideMovement();
        Jump();
    }

    private void SideMovement()
    {
        player.rb.velocity = new Vector2(player.xVelocity * Time.fixedDeltaTime, player.rb.velocity.y);
        Flip();
    }

    private void Jump()
    {
        if (player.canJump && player.isGrounded)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, jumpForce * Time.fixedDeltaTime);
            player.isGrounded = false;
            player.canJump = false;
        }
    }

    private void Crouch()
    {
        if(player.isCrouching)
        {
            speed = crouchSpeed;
            playerCollider.offset = standColliderOffset + crouchColliderOffset;
            playerCollider.size = standColliderSize - crouchColliderSize;
            player.groundCheckPoint.localPosition = standGroundCheckPosition - crouchGroundCheckPosition;
        }
        else
        {
            speed = runSpeed;
            playerCollider.offset = standColliderOffset;
            playerCollider.size = standColliderSize;
            player.groundCheckPoint.localPosition = standGroundCheckPosition;
        }
    }

    private void MovementValues()
    {
        VelocityValue();
        JumpValue();
        CrouchValue();
    }

    private void VelocityValue()
    {
        if(player.isGrounded)
            player.xVelocity = Mathf.MoveTowards(player.xVelocity, player.horizontalInput * speed, Time.deltaTime * speed * groundedSpeedMultiplier);
        else 
            player.xVelocity = Mathf.MoveTowards(player.xVelocity, player.horizontalInput * speed, Time.deltaTime * speed * airborneSpeedMultiplier);
    }

    private void JumpValue()
    {
        if (player.jumpInput > 0 && player.isGrounded)
            player.canJump = true;
    }

    private void CrouchValue()
    {
        if(player.isGrounded && player.crouchInput > 0)
            player.isCrouching = true;
        else
            player.isCrouching = false;
    }

    private void Flip()
    {
        Vector2 turnDirection = crossHair.transform.position - transform.position;

        if(turnDirection.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
