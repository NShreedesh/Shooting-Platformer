using System;
using Destructibles;
using UnityEngine;

namespace HealthScripts
{
    public class Health : MonoBehaviour, IDamageable
    {
        [field: SerializeField]
        public int MaxHealth { get; private set; } = 3;
        [field: SerializeField]
        public int CurrentHealth { get; private set; } = 3;

        private DestroyingObject destroyingObject;

        public Action<int> healthUpdated;

        private void Awake()
        {
            destroyingObject = GetComponent<DestroyingObject>();
        }

        private void Start()
        {
            CurrentHealth = MaxHealth;
            healthUpdated?.Invoke(MaxHealth);
        }

        public void Damage(int amount)
        {
            CurrentHealth -= amount;

            if (CurrentHealth <= 0)
            {
                destroyingObject?.Destroy();
                Destroy(gameObject);
            }
            
            healthUpdated?.Invoke(CurrentHealth);
        }
    }
}
