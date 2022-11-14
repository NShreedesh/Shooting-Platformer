using UnityEngine;

public class PickableItem : MonoBehaviour, IPickable
{
    [field: SerializeField]
    public bool IsPicked { get; private set; }

    public void Pick()
    {
        if (IsPicked) return;
        IsPicked = true;
        print("Picked an Item");
    }
}
