using UnityEngine;

namespace Core.Services.InputService
{
    public class InputData
    {
        public readonly Vector2 Movement;
        public readonly float Rotation;

        public InputData(Vector2 movement, float rotation)
        {
            Movement = movement;
            Rotation = rotation;
        }
    }
}
