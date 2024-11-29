using System.Collections;
using UnityEngine;
using Core.UI.HUD.Stats;

namespace Core.Player
{
    public class PlayerStatsHandler : MonoBehaviour
    {
        private StatsUpdater _updater;
        private PlayerStats _stats;

        public void Construct(StatsUpdater updater)
        {
            _stats = new PlayerStats(10, 10, 10, 10);
            _updater = updater;
            _updater.UpdateAllStats(_stats.Health, _stats.Satiety, _stats.Thirst, 
                _stats.Fatigue);
            StartCoroutine(DecreaseStatsOverTime());
        }

        public void IncreaseHealth(float value)
        {
            if (ValidateValue(value) == false)
                return;
            _stats.Health = IncreaseValue(_stats.Health, value);
            _updater.UpdateHealth(_stats.Health);
        }

        public void IncreaseSatiety(float value)
        {
            if (ValidateValue(value) == false)
                return;
            _stats.Satiety = IncreaseValue(_stats.Satiety, value);
            _updater.UpdateSatiety(_stats.Satiety);
        }

        public void IncreaseThirst(float value)
        {
            if (ValidateValue(value) == false)
                return;
            _stats.Thirst = IncreaseValue(_stats.Thirst, value);
            _updater.UpdateThirst(_stats.Thirst);
        }

        public void IncreaseFatigue(float value)
        {
            if (ValidateValue(value) == false)
                return;
            _stats.Fatigue = IncreaseValue(_stats.Fatigue, value);
            _updater.UpdateFatigue(_stats.Fatigue);
        }

        private bool ValidateValue(float value)
        {
            if (value > 10 || value < -10)
                return false;
            return true;
        }

        private float IncreaseValue(float stat, float value)
        {
            stat += value;
            if (stat > 10)
                stat = 10;
            else if (stat < 0)
                stat = 0;
            return stat;
        }

        private IEnumerator DecreaseStatsOverTime()
        {
            while (true)
            {
                yield return new WaitForSeconds(1f);
                IncreaseSatiety(-0.2f);
                IncreaseThirst(-0.2f);
                IncreaseFatigue(-0.05f);
            }
        }
    }
}
