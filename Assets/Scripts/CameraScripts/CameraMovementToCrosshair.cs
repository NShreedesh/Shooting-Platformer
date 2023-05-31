using InputScripts;
using PlayerScripts;
using UnityEngine;

namespace CameraScripts
{
    public class CameraMovementToCrosshair : MonoBehaviour
    {
        [Header("Other Scripts")]
        private InputManager inputManager;
        private Player player;
        private UnityEngine.Camera cam;

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

        private void Awake()
        {
            inputManager = FindObjectOfType<InputManager>();
            player = FindObjectOfType<Player>();
            cam = GetComponent<UnityEngine.Camera>();
        }

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
            Vector3 playerPosition = player.transform.position;
            cameraGotoPosition.x = Mathf.Clamp(cameraGotoPosition.x, playerPosition.x - minXOffset, playerPosition.x + maxXOffset);
            cameraGotoPosition.y = Mathf.Clamp(cameraGotoPosition.y, playerPosition.y - minYOffset, playerPosition.y + maxYOffset);
            cameraGotoPosition.z = -10;
            Vector3 cameraPosition = transform.position;
            cameraPosition = Vector3.MoveTowards(cameraPosition, cameraGotoPosition, cameraSpeed * Time.deltaTime * Mathf.Abs(cameraGotoPosition.x - cameraPosition.x));
            transform.position = cameraPosition;
        }
    }
}
