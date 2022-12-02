using UnityEngine;
using UnityEngine.Pool;

public class ObjectPool : MonoBehaviour
{
    [Header("Bullet")]
    public PoolObject prefab;

    [Header("Object Pooling")]
    [SerializeField]
    private int defaultCapacity = 10;
    [SerializeField]
    private int maxSize = 50;
    public IObjectPool<PoolObject> Pool { get; private set; }

    private void Awake()
    {
        Pool = new ObjectPool<PoolObject>(
                    CreatePooledItem,
                    OnTakeFromPool,
                    OnReleasedFromPool,
                    OnDestroyFromPool,
                    true,
                    defaultCapacity,
                    maxSize);
    }

    private PoolObject CreatePooledItem()
    {
        PoolObject spawnedBullet = Instantiate(prefab, transform.position, Quaternion.identity, transform);
        spawnedBullet.Initialize(Pool);
        return spawnedBullet;
    }

    private void OnTakeFromPool(PoolObject obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReleasedFromPool(PoolObject obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyFromPool(PoolObject obj)
    {
        Destroy(obj.gameObject);
    }
}
