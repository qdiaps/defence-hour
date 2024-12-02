using UnityEngine;
using System;
using System.Collections.Generic;

namespace Core.Services.PauseService
{
    public class PauseHandler : MonoBehaviour
    {
        public bool IsPause { get; private set; }

        public event Action OnPause;
        public event Action OnPlay;

        [SerializeField] private List<MonoBehaviour> _components = new();

        public void AddComponent(MonoBehaviour component)
        {
            if (component != null)
                _components.Add(component);
        }

        public void SetPause()
        {
            IsPause = true;
            Time.timeScale = 0f;
            OnPause?.Invoke();
            SetActiveComponents(false);
        }

        public void SetPlay()
        {
            IsPause = false;
            Time.timeScale = 1f;
            SetActiveComponents(true);
            OnPlay?.Invoke();
        }

        private void SetActiveComponents(bool value)
        {
            foreach (MonoBehaviour component in _components)
                component.enabled = value;
        }
    }
}
