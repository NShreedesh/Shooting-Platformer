using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Other Scripts")]
    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Player player;
    [SerializeField]
    private Crosshair crosshair;

    [Header("Sensitivity Values")]
    [SerializeField]
    private float cameraSpeed = 40;

    [Header("Sensitivity Values")]
    [SerializeField]
    private float minXOffset;
    [SerializeField]
    private float maxXOffset;
    [SerializeField]
    private float minYOffset;
    [SerializeField]
    private float maxYOffset;


    private void LateUpdate()
    {
        MoveCameraToCursor();
    }

    private void MoveCameraToCursor()
    {
        Vector3 camPosition = transform.position;
        camPosition = Vector3.MoveTowards(camPosition, crosshair.transform.position, Time.deltaTime * cameraSpeed);
        camPosition.x = Mathf.Clamp(camPosition.x, player.transform.position.x - minXOffset, player.transform.position.x + maxXOffset);
        camPosition.y = Mathf.Clamp(camPosition.y, player.transform.position.y - minYOffset, player.transform.position.y + maxYOffset);
        camPosition.z = -10;
        transform.position = camPosition;
    }
}
