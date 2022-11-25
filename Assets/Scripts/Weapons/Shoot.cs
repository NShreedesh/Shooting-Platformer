using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private BulletPool bulletPool;
    private Gun gun;

    [Header("Shooting")]
    private GameObject muzzleFlash;
    private Transform shootPoint;

    [Header("Inputs")]
    private float shootInput;

    [Header("Shoot Time")]
    [SerializeField]
    private float shootTime;

    private void Update()
    {
        ReadInput();
        CheckShooting();
    }

    public void Initialize(Gun gun, Transform shootPoint, GameObject muzzleFlash)
    {
        this.gun = gun;
        this.shootPoint = shootPoint;
        this.muzzleFlash = muzzleFlash;

        shootTime = gun.shootDelayTime;
    }

    private void CheckShooting()
    {
        if (shootInput > 0)
        {
            shootTime -= Time.deltaTime;

            if (shootTime <= 0)
            {
                Hit();
                shootTime = gun.shootDelayTime;
            }
        }
        else if(shootTime > 0)
        {
            shootTime -= Time.deltaTime;
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
