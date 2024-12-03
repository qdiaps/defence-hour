using UnityEngine;

namespace Extensions
{
    public class RandomUtility
    {
        public static Vector2 GetRandomPositionInCirle(Transform center, float minRadius,
            float maxRadius)
        {
            float radius = Random.Range(minRadius, maxRadius);
            float angle = Random.Range(0, 360);
            float x = center.position.x + radius * Mathf.Cos(angle * Mathf.Rad2Deg);
            float y = center.position.y + radius * Mathf.Sin(angle * Mathf.Rad2Deg);
            return new Vector2(x, y);
        }
    }
}
