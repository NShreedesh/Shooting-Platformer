using UnityEngine;

public class MoveCrosshairCameraWithPlayer : MonoBehaviour
{
    [Header("Other Scripts")]
    [SerializeField]
    private Player player;

    private void Update()
    {
        Vector3 cameraPosition = player.transform.localPosition;
        cameraPosition.z = -10;
        transform.position = cameraPosition;
    }
}
