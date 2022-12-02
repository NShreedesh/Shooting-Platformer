using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private ImpactObjectPooler impactObjectPooler;
    private ObjectPool bulletPool;
    private Gun gun;
    private Recoil recoil;

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

    public void Initialize(Gun gun)
    {
        this.gun = gun;
        shootPoint = gun.shootPoint;
        muzzleFlash = gun.muzzleFlash;
        recoil = gun.recoil;

        shootTime = gun.shootDelayTime;
        bulletPool = gun.bulletPool;
    }

    private void CheckShooting()
    {
        if (shootTime > 0)
            shootTime -= Time.deltaTime;
        else
            shootTime = 0;

        if (shootInput > 0)
        {
            if (shootTime <= 0)
            {
                recoil.StartRecoil();
                Hit();
                shootTime = gun.shootDelayTime;
            }
        }
        else
        {
            recoil.StopRecoil();
        }
    }

    private void Hit()
    {
        Bullet bullet = bulletPool.Pool.Get().GetComponent<Bullet>();
        bullet.Initialize(impactObjectPooler);
        bullet.transform.position = shootPoint.transform.position;
        bullet.Shoot(shootPoint.transform.right);
        bullet.transform.eulerAngles = gun.transform.eulerAngles;

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
