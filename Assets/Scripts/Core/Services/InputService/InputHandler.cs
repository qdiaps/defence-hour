using UnityEngine;
using Core.UI.HUD.Movement;

namespace Core.Services.InputService
{
    public class InputHandler
    {
        private FixedJoystick _joystickMovement;
        private FixedJoystick _joystickRotation;

        public InputHandler(FixedJoystick joystickMovement, FixedJoystick joystickRotation)
        {
            _joystickMovement = joystickMovement;
            _joystickRotation = joystickRotation;
        }

        public InputData GetInput()
        {
            Vector2 movement = _joystickMovement.Input;
            Vector2 input = _joystickRotation.Input;
            var rotation = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
            return new(movement, rotation);
        }
    }
}
