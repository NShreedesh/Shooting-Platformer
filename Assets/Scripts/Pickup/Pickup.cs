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
    private Transform weapon;
    [SerializeField]
    private Transform currentGun;

    [Header("Raycast Values")]
    [SerializeField]
    private float radius;
    [SerializeField]
    private LayerMask pickupLayer;

    private void Update()
    {
        PickItems();
    }

    private void PickItems()
    {

        if (inputManager.PlayerAction.Pick.WasPressedThisFrame())
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

            colliders[0].TryGetComponent(out IPickable pickItem);
            Transform newGunTransform = colliders[0].transform;

            newGunTransform.parent = weapon;
            newGunTransform.SetPositionAndRotation(currentGun.position, currentGun.rotation);
            newGunTransform.localScale = currentGun.localScale;
            currentGun.gameObject.SetActive(false);
            currentGun = newGunTransform;

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
