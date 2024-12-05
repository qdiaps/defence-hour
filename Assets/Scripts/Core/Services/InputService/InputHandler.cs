using UnityEngine;
using Core.UI.HUD.Movement;

namespace Core.Services.InputService
{
    public class InputHandler
    {
        private FixedJoystick _joystickMovement;

        public InputHandler(FixedJoystick joystickMovement) =>
            _joystickMovement = joystickMovement;

        public InputData GetInput()
        {
            Vector2 movement = _joystickMovement.Input;
            var rotation = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            return new(movement, rotation);
        }
    }
}
