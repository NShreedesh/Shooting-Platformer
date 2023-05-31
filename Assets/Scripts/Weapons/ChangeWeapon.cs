using InputScripts;
using PickupScripts;
using UnityEngine;

namespace Weapons
{
    public class ChangeWeapon : MonoBehaviour
    {
        [Header("Scripts")]
        [SerializeField]
        private Transform weapon;
        private InputManager inputManager;
        private Pickup pickUp;

        private void Awake()
        {
            inputManager = GetComponent<InputManager>();
            pickUp= GetComponent<Pickup>();
        }

        private void Start()
        {
            inputManager.MouseAction.Scroll.started += ctx =>
            {
                if (ctx.ReadValue<float>() > 0)
                    Change(-1);
                if (ctx.ReadValue<float>() < 0)
                    Change(1);
            };
        }

        private void Change(int changeValue)
        {
            for (int i = 0; i < weapon.childCount; i++)
            {
                if (weapon.GetChild(i).gameObject.activeSelf)
                {
                    if (i + changeValue < 0 || i + changeValue > weapon.childCount - 1) return;

                    weapon.GetChild(i).gameObject.SetActive(false);
                    weapon.GetChild(i + changeValue).gameObject.SetActive(true);
                    pickUp.ChangeCurrentGunTransform(weapon.GetChild(i + changeValue));
                    break;
                }
            }
        }
    }
}
