using UnityEngine;
using DG.Tweening;

namespace Core.Beings.Peaceful
{
    public class PeacefulRemover : MonoBehaviour
    {
        private PeacefulSpawner _spawner;

        public void Construct(PeacefulSpawner spawner) =>
            _spawner = spawner;

        public void RemoveWithAnim(GameObject peaceful)
        {
            peaceful.transform.DOScale(0, 0.2f)
                .OnComplete(() => Remove(peaceful));
        }

        public void Remove(GameObject peaceful)
        {
            _spawner.DeletePeaceful(peaceful);
            Destroy(peaceful);
        }
    }
}
