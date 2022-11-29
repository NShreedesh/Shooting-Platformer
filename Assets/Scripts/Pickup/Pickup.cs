using System.Linq;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Input")]
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private ObjectPool bulletPool;

    [Header("PickUp Position")]
    [SerializeField]
    private Shoot shoot;

    [Header("PickUp Position")]
    [SerializeField]
    private Transform hand;
    [SerializeField]
    private Transform currentGun;

    [Header("Raycast Values")]
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask pickupLayer;

    private void Start()
    {
        if(currentGun.TryGetComponent(out Gun gun))
            shoot.Initialize(gun);
    }

    private void Update()
    {
        PickItems();
    }

    private void PickItems()
    {
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, pickupLayer);
        if (colliders.Length == 0) return;
        colliders = colliders.OrderBy(d => Vector2.Distance(d.transform.position, transform.position))
            .Where(d =>
            {
                d.TryGetComponent(out IPickable pickItem);
                if (!pickItem.IsPicked) return d;
                return false;
            })
            .ToArray();

        if (inputManager.PlayerAction.Pick.WasPressedThisFrame())
        {
            colliders[0].TryGetComponent(out IPickable pickItem);
            Transform newGunTransform = colliders[0].transform;

            newGunTransform.parent = hand;
            newGunTransform.SetPositionAndRotation(currentGun.position, currentGun.rotation);
            newGunTransform.localScale = currentGun.localScale;
            Destroy(currentGun.gameObject);
            currentGun = newGunTransform;

            if(currentGun.TryGetComponent(out Gun gun)) 
                shoot.Initialize(gun);

            pickItem.Pick();
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
#endif
}
