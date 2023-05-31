using TagScripts;
using UnityEngine;

namespace ObjectPooling.ObjectPooler
{
    public class BlastImpactObjectPooler : MonoBehaviour
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
}
