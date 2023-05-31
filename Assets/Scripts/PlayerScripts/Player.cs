using InputScripts;
using UnityEngine;

namespace PlayerScripts
{
    [RequireComponent(typeof(Rigidbody2D))]
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
        public float jumpInput;
        [HideInInspector]
        public float crouchInput;

        [Header("Movement")]
        public float xVelocity;

        [Header("Jump")]
        public bool canJump;
        public bool isGrounded;

        [Header("Crouch")]
        public bool isCrouching;

        [Header("RayCasting")]
        public Transform groundCheckPoint;
        [SerializeField]
        private LayerMask groundMask;
        [SerializeField]
        private float radius;

        private void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            inputManager = GetComponent<InputManager>();
        }

        private void Update()
        {
            isGrounded = IsGrounded();
            PlayerInput();
        }

        private void PlayerInput()
        {
            horizontalInput = inputManager.PlayerAction.Movement.ReadValue<float>();
            jumpInput = inputManager.PlayerAction.Jump.ReadValue<float>();
            crouchInput = inputManager.PlayerAction.Crouch.ReadValue<float>();
        }

        private bool IsGrounded()
        {
            Collider2D hitInfo = Physics2D.OverlapCircle(groundCheckPoint.position, radius, groundMask);
            return hitInfo is not null;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (groundCheckPoint == null) return;
            Gizmos.DrawWireSphere(groundCheckPoint.position, radius);
        }
#endif
    }
}
