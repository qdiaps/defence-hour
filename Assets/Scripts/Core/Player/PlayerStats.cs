namespace Core.Player
{
    public class PlayerStats
    {
        public float Health;
        public float Satiety;
        public float Fatigue;
        public float Thirst;

        public PlayerStats(float health, float satiety, float fatigue, float thirst)
        {
            Health = health > 0 ? health : 0;
            Satiety = satiety > 0 ? satiety : 0;
            Fatigue = fatigue > 0 ? fatigue : 0;
            Thirst = thirst > 0 ? thirst : 0;
        }
    }
}
