using UnityEngine;

public class HitImpactObjectPooler : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private ObjectPool sandImpactPool;
    [SerializeField]
    private ObjectPool barrelImpactPool;

    public ObjectPool GetPool(Tag tag)
    {
        switch (tag)
        {
            case Tag.Sand:
                return sandImpactPool;
            case Tag.Barrel:
                return barrelImpactPool;
        }

        return null;
    }
}
