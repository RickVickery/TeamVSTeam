using System;
using System.Collections.Generic;
using System.Text;

namespace TeamVsTeam
{
    enum Event
    {
        Low,
        Middle,
        High,
        Gold,
        EXP,
        Boss,
        Extra,
        Less
    }
    public class DayTracker
    {
        public static int Day = 1;

        public static void Goodnight()
        {
            Day += 1;
        }

        public static void EventLoader()
        {
            if(Day == 5)
            {
                GenerateEnemies();
                GenerateEnemies();
            }
            else
            {
                GenerateEnemies();
            }
           

            

        }

        public static void GenerateEnemies()
        {
            Random rand = new Random();
            int enemyWave = rand.Next(3, 5);
            for (int i = 0; i < enemyWave; i++)
            {
                Unit unit = new Unit();
                unit.GenerateEnemyUnit();
                EnemyTeam.team.Add(unit);
            }
        }
    }
}
