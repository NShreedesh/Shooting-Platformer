using UnityEngine;

public class DestructibleObjects : MonoBehaviour
{
    [SerializeField]
    private ObjectPool objectPool;

    private void OnDisable()
    {
        if (objectPool == null) return;
        GameObject g = objectPool.Pool.Get().gameObject;
        g.transform.position = transform.position;
    }
}
