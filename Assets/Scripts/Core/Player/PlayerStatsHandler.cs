using System.Collections;
using UnityEngine;
using Core.UI.HUD.Stats;
using Core.Services.PauseService;

namespace Core.Player
{
    public class PlayerStatsHandler : MonoBehaviour
    {
        private StatsUpdater _updater;
        private PlayerStats _stats;
        private PauseHandler _pause;

        public void Construct(StatsUpdater updater, PauseHandler pause)
        {
            _stats = new PlayerStats(10, 10, 10);
            _updater = updater;
            _updater.UpdateAllStats(_stats.Health, _stats.Satiety, _stats.Fatigue);
            _pause = pause;
            StartCoroutine(DecreaseStatsOverTime());
        }

        public void IncreaseHealth(float value)
        {
            _stats.Health = IncreaseValue(_stats.Health, value);
            _updater.UpdateHealth(_stats.Health);
        }

        public void IncreaseSatiety(float value)
        {
            _stats.Satiety = IncreaseValue(_stats.Satiety, value);
            _updater.UpdateSatiety(_stats.Satiety);
        }

        public void IncreaseFatigue(float value)
        {
            _stats.Fatigue = IncreaseValue(_stats.Fatigue, value);
            _updater.UpdateFatigue(_stats.Fatigue);
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
                if (_pause.IsPause)
                    continue;
                IncreaseSatiety(-0.2f);
                IncreaseFatigue(-0.05f);
            }
        }
    }
}
