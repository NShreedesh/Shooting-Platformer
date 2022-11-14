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
        ReadInput();
        PickItems();
    }

    private void ReadInput()
    {
        pickInput = inputManager.PlayerAction.Pick.ReadValue<float>();
    }

    private void PickItems()
    {
        IPickable pickItem = null;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius, pickupLayer);
        if (colliders == null) return;
        foreach (Collider2D collider in colliders)
        {
            collider.TryGetComponent(out pickItem);
            if(!pickItem.IsPicked)
            {
                break;
            }
        }

        if(pickInput == 1)
        {
            pickItem.Pick();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
