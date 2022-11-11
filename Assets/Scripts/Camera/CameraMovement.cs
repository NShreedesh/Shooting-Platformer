using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class CameraMovement : MonoBehaviour
{
    [Header("Other Scripts")]
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Player player;

    [Header("Input Values")]
    [SerializeField]
    private Vector2 delta;

    [Header("Sensitivity Values")]
    [SerializeField]
    private float xSenesitivity;
    [SerializeField]
    private float ySenesitivity;

    private void Update()
    {
        ReadInput();
    }

    private void LateUpdate()
    {
        MoveCameraToCursor();
    }

    private void ReadInput()
    {
        delta = inputManager.MouseAction.Delta.ReadValue<Vector2>();
    }

    private void MoveCameraToCursor()
    {
        Vector3 camPosition = transform.position;
        camPosition += new Vector3(delta.x * xSenesitivity * Time.deltaTime, delta.y * ySenesitivity * Time.deltaTime, 0);
        camPosition.x = Mathf.Clamp(camPosition.x, player.transform.position.x - 5, player.transform.position.x + 5);
        camPosition.y = Mathf.Clamp(camPosition.y, player.transform.position.y - 3, player.transform.position.y + 3);
        transform.position = camPosition;
    }
}
