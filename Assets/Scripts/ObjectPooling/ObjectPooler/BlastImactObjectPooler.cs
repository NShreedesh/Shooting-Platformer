using UnityEngine;

public class BlastImactObjectPooler : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField]
    private ObjectPool blastImpactPool;

    public ObjectPool GetPool(Tag tag)
    {
        switch (tag)
        {
            case Tag.Barrel:
                return blastImpactPool;
        }

        return null;
    }
}
