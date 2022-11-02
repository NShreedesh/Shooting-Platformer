using UnityEngine;
using UnityEngine.Pool;

public class BulletPool : MonoBehaviour
{
    [Header("Bullet")]
    public Bullet bullet;


    [Header("Object Pooling")]
    [SerializeField]
    private int defaultCapacity = 10;
    [SerializeField]
    private int maxSize = 50;
    public IObjectPool<Bullet> Pool { get; private set; }

    private void Awake()
    {
        Pool = new ObjectPool<Bullet>(
                    CreatePooledItem,
                    OnTakeFromPool,
                    OnReleasedFromPool,
                    OnDestroyFromPool,
                    true,
                    defaultCapacity,
                    maxSize);
    }

    private Bullet CreatePooledItem()
    {
        Bullet spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity, transform);
        spawnedBullet.GetComponent<ReturnToPool>().Initialize(Pool);
        return spawnedBullet;
    }

    private void OnTakeFromPool(Bullet obj)
    {
        obj.gameObject.SetActive(true);
    }

    private void OnReleasedFromPool(Bullet obj)
    {
        obj.gameObject.SetActive(false);
    }

    private void OnDestroyFromPool(Bullet obj)
    {
        Destroy(obj.gameObject);
    }
}
