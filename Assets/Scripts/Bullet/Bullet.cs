using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Scripts")]
    private ReturnToPool returnToPool;

    [Header("Components")]
    private Rigidbody2D rb;

    [Header("Bullet Values")]
    [SerializeField]
    private float speed;

    private void Awake()
    {
        returnToPool = GetComponent<ReturnToPool>();
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
        DisableBullet();
    }

    private void DisableBullet()
    {
        returnToPool.Return(this);
    }

    public void Shoot(Vector2 dir)
    {
        rb.velocity = dir * speed;
    }
}
