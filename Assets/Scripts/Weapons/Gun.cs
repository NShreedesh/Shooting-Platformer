using UnityEngine;

public class Gun : PickableItem
{
    [Header("Gun Data Object")]
    public GunScriptableObject gunData;

    [Header("Data to be Given")]
    public Transform shootPoint;
    public GameObject muzzleFlash;
}
