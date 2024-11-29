using UnityEngine;
using UnityEngine.UI;

namespace Core.UI.HUD.Stats
{
    public class StatsUpdater : MonoBehaviour
    {
        [SerializeField] private Image _health;
        [SerializeField] private Image _satiety;
        [SerializeField] private Image _thirst;
        [SerializeField] private Image _fatigue;

        private void Awake()
        {
            if (_health == null || _satiety == null || _thirst == null || _fatigue == null)
                Debug.LogError($"{name}: field(-s) is null!");
        }

        public void UpdateAllStats(float health, float satiety, float thirst, float fatigue)
        {
            UpdateHealth(health);
            UpdateSatiety(satiety);
            UpdateThirst(thirst);
            UpdateFatigue(fatigue);
        }

        public void UpdateHealth(float value) =>
            UpdateStat(_health, value);

        public void UpdateSatiety(float value) =>
            UpdateStat(_satiety, value);

        public void UpdateThirst(float value) =>
            UpdateStat(_thirst, value);

        public void UpdateFatigue(float value) =>
            UpdateStat(_fatigue, value);

        private void UpdateStat(Image stat, float value) =>
            stat.fillAmount = NormalizeValue(value);

        private float NormalizeValue(float value) =>
            (value - 0f) / (10f - 0f);
    }
}
