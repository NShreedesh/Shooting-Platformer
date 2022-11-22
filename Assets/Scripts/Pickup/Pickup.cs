using System.Linq;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Input")]
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private BulletPool bulletPool;

    [Header("PickUp Position")]
    [SerializeField]
    private Transform hand;
    [SerializeField]
    private Transform gun;

    [Header("Raycast Values")]
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask pickupLayer;

    [Header("Input Values")]
    [SerializeField]
    private float pickInput;

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
                d.TryGetComponent<IPickable>(out IPickable pickItem);
                if (!pickItem.IsPicked) return d;
                return false;
            })
            .ToArray();

        if (inputManager.PlayerAction.Pick.WasPressedThisFrame())
        {
            colliders[0].TryGetComponent<IPickable>(out IPickable pickItem);
            Transform newGunTransform = colliders[0].transform;

            newGunTransform.parent = hand;
            newGunTransform.SetPositionAndRotation(gun.position, gun.rotation);
            newGunTransform.localScale = gun.localScale;
            Destroy(gun.gameObject);
            gun = newGunTransform;
            pickItem.Pick();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
