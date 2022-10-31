using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Scripts")]
    private Player player;

    [Header("Movement")]
    [SerializeField]
    private float speed = 300;
    [SerializeField]
    private float groundedSpeedMultiplier = 5;
    [SerializeField]
    private float airborneSpeedMultiplier = 2;
    private float xVelocity;

    [Header("Jump")]
    [SerializeField]
    private float jumpForce = 400;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        MovementValues();
        Flip();
    }

    private void FixedUpdate()
    {
        player.rb.velocity = new Vector2(xVelocity * Time.fixedDeltaTime, player.rb.velocity.y);

        if (player.canJump && player.isGrounded)
        {
            player.rb.velocity = new Vector2(player.rb.velocity.x, jumpForce * Time.fixedDeltaTime);
            player.isGrounded = false;
            player.canJump = false;
        }
    }

    private void MovementValues()
    {
        if(player.isGrounded)
            xVelocity = Mathf.MoveTowards(xVelocity, player.horizontalInput * speed, Time.deltaTime * speed * groundedSpeedMultiplier);
        else
            xVelocity = Mathf.MoveTowards(xVelocity, player.horizontalInput * speed, Time.deltaTime * speed * airborneSpeedMultiplier);

        if (player.verticalInput > 0 && player.isGrounded)
        {
            player.canJump = true;
        }
    }

    private void Flip()
    {
        if (player.horizontalInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (player.horizontalInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}