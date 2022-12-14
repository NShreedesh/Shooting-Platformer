using UnityEngine;
using UnityEngine.Pool;

public class ReturnToPool : MonoBehaviour
{

    [Header("ObjectPool")]
    private PoolObject poolObject;

    public void Return(Bullet bullet)
    {
        bulletPool.Release(bullet);
    }

    public void Initialize(IObjectPool<Bullet> bulletPool)
    {
        this.bulletPool = bulletPool;
    }
}
