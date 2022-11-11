using UnityEngine;

public class MoveCrosshairCameraWithPlayer : MonoBehaviour
{
    [Header("Other Scripts")]
    [SerializeField]
    private Player player;

    private void LateUpdate()
    {
        transform.position = player.transform.position;
    }
}
