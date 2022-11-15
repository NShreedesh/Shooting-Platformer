using UnityEngine;

public class CameraMovementToCrosshair : MonoBehaviour
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
        Vector3 camPosition = crosshair.transform.position;
        camPosition.x = Mathf.Clamp(camPosition.x, player.transform.position.x - minXOffset, player.transform.position.x + maxXOffset);
        camPosition.y = Mathf.Clamp(camPosition.y, player.transform.position.y - minYOffset, player.transform.position.y + maxYOffset);
        camPosition.z = -10;
        transform.position = camPosition;
    }
}
