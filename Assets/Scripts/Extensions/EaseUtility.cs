using UnityEngine;

namespace Extensions
{
    public class EaseUtility
    {
        /// <summary>
        /// Функция EaseInOut
        /// Ускорение вначале, замедление в конце
        /// </summary>
        public static float InOut(float time, float allTime) =>
            time < (allTime / 2) ? 4f * time * time * time : 1f - Mathf.Pow(-2f * time + 2f, 3f) / 2f;
    }
}
