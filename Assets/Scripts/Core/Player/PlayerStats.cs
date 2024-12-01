namespace Core.Player
{
    public class PlayerStats
    {
        public float Health;
        public float Satiety;
        public float Fatigue;

        public PlayerStats(float health, float satiety, float fatigue)
        {
            Health = health > 0 ? health : 0;
            Satiety = satiety > 0 ? satiety : 0;
            Fatigue = fatigue > 0 ? fatigue : 0;
        }
    }
}
