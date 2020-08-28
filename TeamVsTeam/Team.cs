using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace TeamVsTeam
{
    public class Team
    {
        public static List<Unit> team = new List<Unit>();


        public static double Funds;
        public static double HealQuant;
        public static double FoodQuant;
        public static bool Secure = false;
        public static bool ScoutsSent;

        public Team()
        {
            Team.HealQuant = 2;
            Team.FoodQuant = 2;
        }

        public static void teamToArray()
        {
            Unit[] team = Team.team.ToArray();
        }
        public static bool healGuard()
        {
            if(HealQuant <= 0)
            {
                return false;
            }
            else
            {
                HealQuant--;
                return true;
            }
        }
        public static bool feedGuard(Unit u)
        {
            if(u.Hunger >= 2)
            {
                return false;
            }
            if (FoodQuant <= 0)
            {
                return false;
            }
            else
            {
                FoodQuant--;
                return true;
            }
        }

        public static void ViewTeam()
        {
            foreach(Unit u in Team.team)
            {
                Console.WriteLine(u);
            }
        }

        public static void ViewTeamChoice()
        {
            int counter = 1;
            foreach (Unit u in Team.team)
            {
                Console.WriteLine($"{counter}. {u}");
                counter++;
            }
        }

        public static int CountOfTeam()
        {
            int total = 0;
            foreach(Unit u in team)
            {
                total++;
            }

            return total;
        }
        public static int CountOfFighter()
        {
            int total = 0;
            foreach (Unit u in team)
            {
                if (u.Job == (Class)1)
                {
                    total++;
                }
            }

            return total;
        }
        public static int CountOfRogue()
        {
            int total = 0;
            foreach (Unit u in team)
            {
                if (u.Job == (Class)2)
                {
                    total++;
                }
            }

            return total;
        }
        public static int CountOfMage()
        {
            int total = 0;
            foreach (Unit u in team)
            {
                if (u.Job == (Class)3)
                {
                    total++;
                }
            }

            return total;
        }
    }
}
