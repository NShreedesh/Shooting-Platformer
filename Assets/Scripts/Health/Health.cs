using UnityEngine;

public class Health : MonoBehaviour, IDamagable
{
    [field: SerializeField]
    public int HealthAmount { get; private set; } = 3;

    public void Damage(int amount)
    {
        DecreaseHealth(amount);

        if (HealthAmount <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void DecreaseHealth(int amount)
    {
        HealthAmount--;
    }
}
