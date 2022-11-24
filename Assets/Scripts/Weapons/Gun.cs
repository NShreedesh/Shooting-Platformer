using UnityEngine;

public class Gun : PickableItem
{
    [Header("Components")]
    private BoxCollider2D boxCollider;

    [Header("Gun Data Object")]
    public GunScriptableObject gunData;

    [Header("Data to be Given")]
    public Transform shootPoint;
    public GameObject muzzleFlash;

    private void Start()
    {
        boxCollider= GetComponent<BoxCollider2D>();
        ToggleCollider();
    }

    public override void Pick()
    {
        base.Pick();
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
