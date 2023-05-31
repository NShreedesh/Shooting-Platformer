using UnityEngine;

namespace PlayerScripts
{
    public class PlayerAnimation : MonoBehaviour
    {
        [Header("Scripts")]
        private Player player;

        [Header("Components")]
        private Animator anim;

        [Header("Animation")]
        private const string Jump = "jump";
        private int JumpHash => Animator.StringToHash(Jump);
        private const string Velocity = "velocity";
        private int VelocityHash => Animator.StringToHash(Velocity);
        private const string Crouch = "crouch";
        private int CrouchHash => Animator.StringToHash(Crouch);

        private void Awake()
        {
            anim = GetComponentInChildren<Animator>();
            player = GetComponent<Player>();
        }

        private void Update()
        {
            anim.SetFloat(VelocityHash, Mathf.Abs(player.xVelocity));
            anim.SetBool(JumpHash, !player.isGrounded);
            anim.SetBool(CrouchHash, player.isCrouching);
        }
    }
}
