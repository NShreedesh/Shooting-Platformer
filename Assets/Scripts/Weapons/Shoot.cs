using System.Collections;
using BulletScripts;
using InputScripts;
using ObjectPooling.ObjectPooler;
using TagScripts;
using UnityEngine;

namespace Weapons
{
    public class Shoot : MonoBehaviour
    {
        [Header("Scripts")]
        private InputManager inputManager;

        [Header("Object Pools")]
        private BulletObjectPooler bulletPooler;
        ITaggable tagger;

        [Header("Gun Data")]
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
        private float shootDelayTime;
        private float shootTime;

        private void Awake()
        {
            gun = GetComponent<Gun>();
            inputManager = FindObjectOfType<InputManager>();
            bulletPooler = FindObjectOfType<BulletObjectPooler>();
            recoil = GetComponent<Recoil>();
            tagger = GetComponent<ITaggable>();
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
                    shootTime = shootDelayTime;
                }
            }
            else
            {
                recoil.StopRecoil();
            }
        }

        private void Hit()
        {
            Bullet bullet = bulletPooler.GetPool(tagger.Tag).Pool.Get().GetComponent<Bullet>();
            Transform point = shootPoint.transform;
            bullet.transform.position = point.position;
            bullet.Shoot(point.right);
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
}
