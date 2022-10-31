using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Input Input { get; private set; }
    public Input.PlayerActions PlayerAction { get; private set; }
    public Input.MouseActions MouseAction { get; private set; }

    private void Awake()
    {
        Input = new Input();
        PlayerAction = Input.Player;
        MouseAction = Input.Mouse;
    }

    private void OnEnable()
    {
        PlayerAction.Enable();
        MouseAction.Enable();
    }

    private void OnDisable()
    {
        PlayerAction.Disable();
        MouseAction.Disable();
    }
}
