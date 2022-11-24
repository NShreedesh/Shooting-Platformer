using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool IsPicked { get; private set; }

    public virtual void Pick()
    {
        if (IsPicked) return;

        IsPicked = true; 
    }
}
