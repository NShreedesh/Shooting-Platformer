using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Scripts")]
    private ObjectPool impactPool;
    private PoolObject poolObject;

    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Bullet Values")]
    [SerializeField]
    private float speed;

    [Header("Damage Values")]
    [SerializeField]
    private int damageBarrel = 1;

    private void Awake()
    {
        poolObject = GetComponent<PoolObject>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        Invoke(nameof(DisableBullet), 1);
    }

    private void OnDisable()
    {
        CancelInvoke();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Impact();
        if(collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.Damage(damageBarrel);
        }
        DisableBullet();
    }

    public void Initialize(ObjectPool impactPool)
    {
        this.impactPool = impactPool;
    }

    private void Impact()
    {
        ParticleSystem impactParticle = impactPool.Pool.Get().GetComponent<ParticleSystem>();
        impactParticle.gameObject.transform.position = transform.position;
        impactParticle.Play();
    }

    private void DisableBullet()
    {
        if(gameObject.activeSelf)
            poolObject.Return();
    }

    public void Shoot(Vector2 dir)
    {
        rb.velocity = dir * speed;
    }
}
