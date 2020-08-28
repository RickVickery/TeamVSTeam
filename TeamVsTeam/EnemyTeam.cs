using System;
using System.Collections.Generic;
using System.Text;

namespace TeamVsTeam
{
    public class EnemyTeam
    {
        public static List<Unit> team = new List<Unit>();

        public static double Funds { get; set; }

        public static void ViewTeam()
        {
            foreach (Unit u in team)
            {
                Console.WriteLine(u);
            }
        }

        public static void ViewTeamChoice()
        {
            int counter = 1;
            foreach (Unit u in team)
            {
                Console.WriteLine($"{counter}. {u}");
                counter++;
            }
        }

        public static double CountOfTeam()
        {
            double total = 0;
            foreach (Unit u in team)
            {
                total++;
            }

            return total;
        }
    }
}
