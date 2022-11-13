using UnityEngine;
using UnityEngine.UIElements;

public class MoveCrosshairCameraWithPlayer : MonoBehaviour
{
    [Header("Other Scripts")]
    [SerializeField]
    private Player player;

    private void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;
        cameraPosition = player.transform.localPosition;
        cameraPosition.z = -10;
        transform.position = cameraPosition;
    }
}
