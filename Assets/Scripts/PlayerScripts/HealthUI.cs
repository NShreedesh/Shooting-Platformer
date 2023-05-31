using HealthScripts;
using UnityEngine;
using UnityEngine.UI;

namespace PlayerScripts
{
    public class HealthUI : MonoBehaviour
    {
        [SerializeField]
        private Health health;

        [SerializeField]
        private Image healthFillImage;

        private void Awake()
        {
            health.healthUpdated += UpdateHealthUI;
        }

        private void UpdateHealthUI(int healthAmount)
        {
            healthFillImage.fillAmount = (float)healthAmount / health.MaxHealth;
        }

        private void OnDisable()
        {
            health.healthUpdated -= UpdateHealthUI;
        }
    }
}
