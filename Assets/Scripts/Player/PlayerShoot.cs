using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Scripts")]
    private Player player;
    [SerializeField]
    private InputManager inputManager;

    [Header("Inputs")]
    [SerializeField]
    private float shootInput;

    [Header("Shoot Time")]
    [SerializeField]
    private float shootTime;
    [SerializeField]
    private float shootDelayTime = 0.3f;
    [SerializeField]
    private bool canShoot = true;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        ReadInput();
        Shoot();
    }

    private void Shoot()
    {
        if (shootInput > 0)
        {
            if (canShoot)
            {
                print("pew pew");
                shootTime = 0;
                canShoot = false;
            }

            shootTime += Time.deltaTime;

            if (shootTime >= shootDelayTime)
            {
                canShoot = true;
            }
        }
        else
        {
            shootTime = 0;
            canShoot = true;
        }
    }

    private void ReadInput()
    {
        shootInput = inputManager.PlayerAction.Shoot.ReadValue<float>();
    }
}
