using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private BulletPool bulletPool;
    [SerializeField]
    private PickableItem pickable;

    [Header("Shooting")]
    private GameObject muzzleFlash;
    private Transform shootPoint;

    [Header("Inputs")]
    private float shootInput;

    [Header("Shoot Time")]
    [SerializeField]
    private bool canShoot = true;
    [SerializeField]
    private float shootTime;
    private Gun gun;

    private void Update()
    {
        if (!pickable.IsPicked) return;
        ReadInput();
        CheckShooting();
    }

    public void Initialize(Gun gun, Transform shootPoint, GameObject muzzleFlash)
    {
        this.gun = gun;
        this.shootPoint = shootPoint;
        this.muzzleFlash = muzzleFlash;
    }

    private void CheckShooting()
    {
        if (shootInput > 0)
        {
            shootTime += Time.deltaTime;

            if (shootTime >= gun.shootDelayTime)
                canShoot = true;

            if (canShoot)
            {
                Hit();
                shootTime = 0;
                canShoot = false;
            }
        }
        else
        {
            shootTime = 0;
            canShoot = true;
        }
    }

    private void Hit()
    {
        Bullet bullet = bulletPool.Pool.Get();
        bullet.transform.position = shootPoint.transform.position;
        bullet.Shoot(shootPoint.transform.right);

        muzzleFlash.SetActive(true);
        StartCoroutine(DisableMuzzleFlash());
    }

    private IEnumerator DisableMuzzleFlash()
    {
        yield return new WaitForSeconds(0.05f);
        muzzleFlash.SetActive(false);
    }

    private void ReadInput()
    {
        shootInput = inputManager.PlayerAction.Shoot.ReadValue<float>();
    }
}
