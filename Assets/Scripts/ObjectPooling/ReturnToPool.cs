using UnityEngine;

namespace ObjectPooling
{
    public class ReturnToPool : MonoBehaviour
    {
        [Header("Scripts")]
        private PoolObject poolObject;

        private void Awake()
        {
            poolObject = GetComponent<PoolObject>();
        }

        private void OnDisable()
        {
            poolObject.Return();
        }
    }
}
