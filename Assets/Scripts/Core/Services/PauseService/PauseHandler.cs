using UnityEngine;
using System.Collections.Generic;

namespace Core.Services.PauseService
{
    public class PauseHandler : MonoBehaviour
    {
        public bool IsPause { get; private set; }

        [SerializeField] private List<MonoBehaviour> _components = new();

        public void AddComponent(MonoBehaviour component)
        {
            if (component != null)
                _components.Add(component);
        }

        public void SetPause()
        {
            IsPause = true;
            SetActiveComponents(false);
        }

        public void SetPlay()
        {
            IsPause = false;
            SetActiveComponents(true);
        }

        private void SetActiveComponents(bool value)
        {
            foreach (MonoBehaviour component in _components)
                component.enabled = value;
        }
    }
}
