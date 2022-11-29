using UnityEngine;
using UnityEngine.Pool;

public class PoolObject : MonoBehaviour
{
    private IObjectPool<PoolObject> objectPool;

    public void Initialize(IObjectPool<PoolObject> pool)
    {
        objectPool = pool;
    }

    public void Return()
    {
        objectPool.Release(this);
    }
}
