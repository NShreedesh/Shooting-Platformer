using InputScripts;
using UnityEngine;

namespace Weapons
{
    public class RotateTowardsPlayer : MonoBehaviour
    {
        private Camera cam;
        private InputManager inputManager;

        private void Awake()
        {
            cam = Camera.main;
            inputManager = GetComponentInParent<InputManager>();
        }

        private void Update()
        {
            Vector2 direction = (cam.ScreenToWorldPoint(inputManager.MouseAction.Position.ReadValue<Vector2>()) - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = rotation;
        }
    }
}
