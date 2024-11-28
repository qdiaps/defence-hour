using UnityEngine;
using Core.UI.HUD.Movement;

namespace Core.Services.InputService
{
    public class InputHandler
    {
        private FixedJoystick _joystick;

        public InputHandler(FixedJoystick joystick) =>
            _joystick = joystick;

        public Vector2 GetInput() =>
            _joystick.Input;
    }
}
