using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField]
    private Texture2D cursorTexture;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.SetCursor(cursorTexture, new Vector2(0, 0), CursorMode.ForceSoftware);
    }
}
