using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Input Input { get; private set; }
    public Input.PlayerActions PlayerAction { get; private set; }

    private void Awake()
    {
        Input = new Input();
        PlayerAction = Input.Player;
    }

    private void OnEnable()
    {
        PlayerAction.Enable();
    }

    private void OnDisable()
    {
        PlayerAction.Disable();
    }
}
