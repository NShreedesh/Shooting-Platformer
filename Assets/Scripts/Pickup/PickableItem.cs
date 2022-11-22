using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool IsPicked { get; private set; }

    [SerializeField]
    private BoxCollider2D boxCollider;

    private void Start()
    {
        ToggleCollider();
    }

    public void Pick()
    {
        if (IsPicked) return;

        IsPicked = true; 
        ToggleCollider();
    }

    private void ToggleCollider()
    {
        if (IsPicked)
            boxCollider.enabled = false;
        else
            boxCollider.enabled = true;
    }
}
