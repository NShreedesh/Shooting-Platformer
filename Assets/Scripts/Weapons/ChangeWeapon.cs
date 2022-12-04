using UnityEngine;

public class ChangeWeapon : MonoBehaviour
{
    [SerializeField]
    private Transform weapon;
    [SerializeField]
    private InputManager inputManager;

    private void Update()
    {
        if (inputManager.MouseAction.Scroll.ReadValue<float>() > 0)
        {
            Change(1);
        }
        if (inputManager.MouseAction.Scroll.ReadValue<float>() < 0)
        {
            Change(-1);
        }
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
            }
        }
    }
}
