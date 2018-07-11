using System;

namespace Game
{
    public static class Dice
    {
        public static Random Random;
        static Dice()
        {
            Random = new Random();
        }
        public static int Roll6()
        {
            return Random.Next(1, 7);
        }
        public static int Roll10()
        {
            return Random.Next(1, 11);
        }
        public static int Between(int min, int max)
        {
            return Random.Next(min, max);
        }
    }
}
