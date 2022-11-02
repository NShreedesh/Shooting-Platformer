using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class PlayerShoot : MonoBehaviour
{
    [Header("Scripts")]
    private Player player;
    [SerializeField]
    private InputManager inputManager;

    [Header("Inputs")]
    [SerializeField]
    private float shootInput;

    [Header("Shooting")]
    [SerializeField]
    private BulletPool bulletPool;
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private Transform shootPoint;
    [SerializeField]
    private Transform crosshairTransform;

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
        CheckShooting();
    }

    private void CheckShooting()
    {
        if (shootInput > 0)
        {
            if (canShoot)
            {
                Shoot();
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

    private void Shoot()
    {
        Vector2 dir = crosshairTransform.position - shootPoint.position;
        dir = dir.normalized;

        Bullet bullet = bulletPool.Pool.Get();
        bullet.transform.position = shootPoint.transform.position;
        bullet.Shoot(dir);

        muzzleFlash.SetActive(true);
        StartCoroutine(DisableMuzzleFlash());
    }

    private IEnumerator DisableMuzzleFlash()
    {
        yield return new WaitForSeconds(0.1f);
        muzzleFlash.SetActive(false);
    }

    private void ReadInput()
    {
        shootInput = inputManager.PlayerAction.Shoot.ReadValue<float>();
    }
}
