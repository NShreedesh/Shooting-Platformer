using UnityEngine;

public class BulletObjectPooler : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private ObjectPool pistolBulletPool;
    [SerializeField]
    private ObjectPool machineGunBulletPool;
    [SerializeField]
    private ObjectPool shotGunBulletPool;

    public ObjectPool GetPool(Tag tag)
    {
        switch (tag)
        {
            case Tag.MachineGun:
                return machineGunBulletPool;
            case Tag.Pistol:
                return pistolBulletPool;
            case Tag.ShotGun:
                return shotGunBulletPool;
        }

        return null;
    }
}
