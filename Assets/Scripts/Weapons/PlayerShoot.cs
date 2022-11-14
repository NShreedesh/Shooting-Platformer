using System.Collections;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    [Header("Scripts")]
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

    [Header("Shoot Time")]
    [SerializeField]
    private float shootTime;
    [SerializeField]
    private float shootDelayTime = 0.3f;
    [SerializeField]
    private bool canShoot = true;

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
        Bullet bullet = bulletPool.Pool.Get();
        bullet.transform.position = shootPoint.transform.position;
        bullet.Shoot(shootPoint.transform.right);

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
