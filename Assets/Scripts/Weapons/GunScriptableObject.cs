using UnityEngine;

[CreateAssetMenu(fileName = "Gun Name", menuName = "Gun", order = 1)]
public class GunScriptableObject : ScriptableObject
{
    [Header("Shoot Data")]
    public float shootDelayTime = 0.3f;
}
