using UnityEngine;
using Config;
using Extensions;
using DG.Tweening;
using System.Linq;

namespace Core.Loot
{
    public class LootSpawner
    {
        public void Spawn(LootConfig[] loot, Transform point)
        {
            float[] chances = new float[loot.Length];
            for (int i = 0; i < chances.Length; i++)
                chances[i] = loot[i].DropChance;
            int countLoot = loot.Length - 1;
            for (int i = 0; i <= countLoot; i++)
            {
                int index = RandomUtility.GetRandomIndexFromListChances(chances);
                var item = loot[index];
                int count = Random.Range(item.MinQuantity, item.MaxQuantity + 1);
                for (int j = 0; j < count; j++)
                {
                    var position = RandomUtility.GetRandomPositionInCirle(point, 0.1f, 1);
                    var obj = MonoUtility.Instantiate(item.Prefab, point.position, Quaternion.identity);
                    Sequence anim = DOTween.Sequence();
                    anim.Append(obj.transform.DOScale(obj.transform.localScale, 0.2f).From(0).SetEase(Ease.Linear))
                        .Join(obj.transform.DOMove(position, 0.2f).SetEase(Ease.Linear))
                        .OnComplete(() => obj.GetComponents<CircleCollider2D>()
                            .FirstOrDefault(col => col.isTrigger == true).enabled = true);
                }
            }
        }
    }
}
