using UnityEngine;

namespace InputScripts
{
    public class InputManager : MonoBehaviour
    {
        private Input input;
        public Input.PlayerActions PlayerAction { get; private set; }
        public Input.MouseActions MouseAction { get; private set; }

        private void Awake()
        {
            input = new Input();
            PlayerAction = input.Player;
            MouseAction = input.Mouse;
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
}
