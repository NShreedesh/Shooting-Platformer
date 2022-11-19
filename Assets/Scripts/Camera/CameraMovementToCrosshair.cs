using UnityEngine;

public class CameraMovementToCrosshair : MonoBehaviour
{
    [Header("Other Scripts")]
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Camera cam;

    [Header("Sensitivity Values")]
    [SerializeField]
    private float minXOffset;
    [SerializeField]
    private float maxXOffset;
    [SerializeField]
    private float minYOffset;
    [SerializeField]
    private float maxYOffset;

    [Header("Camera Movement")]
    [SerializeField]
    private float cameraSpeed = 10;
    private Vector3 cameraGotoPosition;

    private void Start()
    {
        inputManager.MouseAction.Position.performed += ctx => cameraGotoPosition = cam.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        MoveCameraToCursor();
    }

    private void MoveCameraToCursor()
    {
        cameraGotoPosition.x = Mathf.Clamp(cameraGotoPosition.x, player.transform.position.x - minXOffset, player.transform.position.x + maxXOffset);
        cameraGotoPosition.y = Mathf.Clamp(cameraGotoPosition.y, player.transform.position.y - minYOffset, player.transform.position.y + maxYOffset);
        cameraGotoPosition.z = -10;
        transform.position = Vector3.MoveTowards(transform.position, cameraGotoPosition, cameraSpeed * Time.deltaTime);
    }
}
