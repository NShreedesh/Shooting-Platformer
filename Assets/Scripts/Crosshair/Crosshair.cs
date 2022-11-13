using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private InputManager inputManager;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void LateUpdate()
    {
        Vector3 crosshairPosition = cam.ScreenToWorldPoint(inputManager.MouseAction.Position.ReadValue<Vector2>());
        crosshairPosition.z = 0;
        transform.position = crosshairPosition;
    }
}
