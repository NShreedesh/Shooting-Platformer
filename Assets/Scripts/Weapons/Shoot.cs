using System.Collections;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private InputManager inputManager;

    [Header("Object Pools")]
    [SerializeField]
    private ImpactObjectPooler impactObjectPooler;
    [SerializeField]
    private ObjectPool bulletPool;

    [Header("Gun Data")]
    [SerializeField]
    private Gun gun;
    private Recoil recoil;

    [Header("Shooting")]
    [SerializeField]
    private GameObject muzzleFlash;
    [SerializeField]
    private Transform shootPoint;

    [Header("Inputs")]
    private float shootInput;

    [Header("Shoot Time")]
    [SerializeField]
    private float shootTime;

    private void Start()
    {
        recoil = GetComponent<Recoil>();
    }

    private void Update()
    {
        if (!gun.IsPicked) return;
        ReadInput();
        CheckShooting();
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
        bullet.transform.eulerAngles = transform.eulerAngles;

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
