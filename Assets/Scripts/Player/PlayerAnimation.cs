using UnityEngine;

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

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
        player = GetComponent<Player>();
    }

    private void Start()
    {
        print(JumpHash);
    }

    private void Update()
    {
        anim.SetBool(JumpHash, !player.isGrounded);
        anim.SetFloat(VelocityHash, Mathf.Abs(player.xVelocity));
    }
}
