using HealthScripts;
using UnityEngine;

namespace Destructibles
{
    public class DestroyingObject : MonoBehaviour
    {
        [SerializeField]
        private LayerMask destructionLayerMask;

        [SerializeField]
        private float radius = 5;
        [SerializeField]
        private int damageAmount = 1;
        
        public void Destroy()
        {   
            Collider2D col = Physics2D.OverlapCircle(transform.position, radius, destructionLayerMask);
            if(col is null) return;
            if (!col.TryGetComponent(out Health health)) return;
            health.Damage(damageAmount);
        }
        

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireSphere(transform.position, radius);
        }
#endif
    }
}