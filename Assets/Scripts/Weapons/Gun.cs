using UnityEngine;

public class Gun : PickableItem
{
    [Header("Components")]
    private BoxCollider2D boxCollider;

    [Header("Scripts")]
    [HideInInspector]
    public Recoil recoil;

    [Header("Data to be Given")]
    public float shootDelayTime = 0.3f;
    public Transform shootPoint;
    public GameObject muzzleFlash;

    private void Start()
    {
        boxCollider= GetComponent<BoxCollider2D>();
        recoil= GetComponent<Recoil>();
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
