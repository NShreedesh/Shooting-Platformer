using UnityEngine;

public class Pickup : MonoBehaviour
{
    [Header("Input")]
    [SerializeField]
    private InputManager inputManager;

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
        IPickable pickItem = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, pickupLayer);
        if (colliders == null) return;
        foreach (Collider2D collider in colliders)
        {
            collider.TryGetComponent<IPickable>(out pickItem);
            if(!pickItem.IsPicked)
            {
                break;
            }
        }

        if (pickItem == null) return;
        if(inputManager.PlayerAction.Pick.WasPressedThisFrame())
        {
            pickItem.Pick();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
