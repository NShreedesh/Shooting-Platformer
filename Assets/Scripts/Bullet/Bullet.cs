using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Scripts")]
    private ImpactObjectPooler impactObjectPooler;
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
        if(collision.TryGetComponent(out ITaggable taggable))
        {
            Impact(taggable);
        }
        if(collision.gameObject.TryGetComponent(out IDamagable damagable))
        {
            damagable.Damage(damageBarrel);
        }
        DisableBullet();
    }

    public void Initialize(ImpactObjectPooler impactObjectPooler)
    {
        this.impactObjectPooler = impactObjectPooler;
    }

    private void Impact(ITaggable taggable)
    {
        ParticleSystem impactParticle = impactObjectPooler.GetPool(taggable.Tag)?.Pool.Get().GetComponent<ParticleSystem>();
        if (impactParticle == null) return;
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
