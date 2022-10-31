using UnityEngine;

public class CursorMovement : MonoBehaviour
{
    private Camera cam;
    [SerializeField]
    private InputManager inputManager;

    private void Awake()
    {
        cam = Camera.main;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 cursorPosition = cam.ScreenToWorldPoint(inputManager.MouseAction.Position.ReadValue<Vector2>());
        cursorPosition.z = 0;
        transform.position = cursorPosition;
    }
}
