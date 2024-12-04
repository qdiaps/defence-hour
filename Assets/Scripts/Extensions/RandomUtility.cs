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

        public static int GetRandomIndexFromListChances(float[] chances)
        {
            float totalChances = 0f;
            foreach (float chance in chances)
                totalChances += chance;
            if (totalChances > 100f)
            {
                Debug.LogError($"RandomUtility.GetRandomIndexFromListChances: totalChances > 100");
                return -1;
            }

            float value = Random.value;
            Debug.Log(value);
            float cumulative = 0f;
            for (int i = 0; i < chances.Length; i++)
            {
                cumulative += chances[i] / totalChances;
                if (value <= cumulative) 
                    return i;
            }
            return -1;
        }
    }
}
