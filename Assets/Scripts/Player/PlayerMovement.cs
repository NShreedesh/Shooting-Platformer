using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Scripts")]
    private Player player;

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
    private float standColliderYOffset;
    private float standColliderYSize;
    [SerializeField]
    private float crouchColliderYOffset = -0.17f;
    [SerializeField]
    private float crouchColliderYSize = 0.58f;

    private void Awake()
    {
        player = GetComponent<Player>();
        playerCollider = GetComponent<CapsuleCollider2D>();

        standColliderYOffset = playerCollider.offset.y;
        standColliderYSize = playerCollider.size.y;
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
            playerCollider.offset = new Vector2(playerCollider.offset.x, crouchColliderYOffset); 
            playerCollider.size = new Vector2(playerCollider.size.x, crouchColliderYSize); 
        }
        else
        {
            speed = runSpeed;
            playerCollider.offset = new Vector2(playerCollider.offset.x, standColliderYOffset);
            playerCollider.size = new Vector2(playerCollider.size.x, standColliderYSize);
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
        if (player.xVelocity > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (player.xVelocity < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
