using System;
using System.Collections.Generic;
using System.Text;

namespace TeamVsTeam 
{

    public enum Class
    {
        Fighter = 1,
        Rogue,
        Mage,
        Koblin,
        Wispher,
        Undeed,
        Golimb,
        Shudde,
        Esperthiv,
        Harthie,
    }

    public class Unit : IComparable<Unit>
    {

        public Class Job { get; set; }
        public int Level { get; set; }
        public double Health { get; set; }
        public double TotalHealth { get; set; }
        public double Experience { get; set; }
        public double TotalExp { get; set; }
        public double Hunger { get; set; }
        public double Power { get; set; }
        public double DamageReduction { get; set; }
        public double Speed { get; set; }
        public double Value { get; set; }
        public bool WellFed { get; set; }
        public bool Starving { get; set; }
        public bool Critical { get; set; }

        public Unit()
        {
            TotalExp = 50;
        }
        public int CompareTo(Unit unit)
        {
            return Health.CompareTo(unit.Health);
        }

        public override string ToString()
        {
            string fedDisplay = "";
            string levelDisplay = "";
            string criticalDisplay = "";

            if(WellFed == true)
            {
                fedDisplay = "+";
            }
            if(WellFed == false)
            {
                fedDisplay = "";
            }
            if (WellFed == false && Starving == true)
            {
                fedDisplay = "-";
            }
            if(Critical == true)
            {
                criticalDisplay = "!!~";
            }
            if(Level > 1)
            {
                for(int i = 0; i<Level-1; i++)
                {
                    levelDisplay += "*";
                }
            }
            
            if(Critical == true)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                return $"{criticalDisplay}{Job}{fedDisplay}{levelDisplay}";
            }
            if(Health >= TotalHealth*.8)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                return $"{criticalDisplay}{Job}{fedDisplay}{levelDisplay}";
            }
            if(Health < TotalHealth*.4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                return $"{criticalDisplay}{Job}{fedDisplay}{levelDisplay}";
            }
            if(Health < TotalHealth*.8)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                return $"{criticalDisplay}{Job}{fedDisplay}{levelDisplay}";
            }
            Console.ForegroundColor = ConsoleColor.White;
            return $"{criticalDisplay}{Job}{fedDisplay}{levelDisplay}";
        }
        public void GenerateHumanUnit()
        {
            Random rand = new Random();
            Unit unit = new Unit();

            Job = (Class)rand.Next(1,4);
            Value = 0;

            if (Job == (Class)1) //Fighter
            {
                TotalHealth = 12;
                Health = 12;
                Power = 5;
                DamageReduction = 2;
                Speed = 5;
                Hunger = 1;
            }
            if (Job == (Class)2) //Rogue
            {
                TotalHealth = 9;
                Health = 9;
                Power = 3;
                DamageReduction = 1;
                Speed = 7;
                Hunger = 1;
            }
            if (Job == (Class)3) //Mage
            {
                TotalHealth = 6;
                Health = 6;
                Power = 8;
                DamageReduction = 0;
                Speed = 3;
                Hunger = 1;
            }
        }
        public void GenerateEnemyUnit()
        {
            Random rand = new Random();
            
            Job = (Class)rand.Next(4,11);
            if ((int)Job == 4) //Koblin
            {
                TotalHealth = 5;
                Health = 5;
                Power = 4;
                DamageReduction = 0;
                Speed = 7;
                Value = rand.Next(5);
                Experience = rand.Next(5);
            }
            if ((int)Job == 5) //Wisph
            {
                TotalHealth = 7;
                Health = 7;
                Power = 4;
                DamageReduction = 1;
                Speed = 5;
                Value = rand.Next(7);
                Experience = rand.Next(7);
            }
            if ((int)Job == 6) //Undeed
            {
                TotalHealth = 10;
                Health = 10;
                Power = 6;
                DamageReduction = 1;
                Speed = 3;
                Value = rand.Next(10);
                Experience = rand.Next(10);
            }
            if ((int)Job == 7) //Golim
            {
                TotalHealth = 12;
                Health = 12;
                Power = 7;
                DamageReduction = 1;
                Speed = 2;
                Value = rand.Next(12);
                Experience = rand.Next(12);
            }
            if ((int)Job == 8) //Shude
            {
                TotalHealth = 10;
                Health = 10;
                Power = 6;
                DamageReduction = 1;
                Speed = 6;
                Value = rand.Next(10);
                Experience = rand.Next(10);
            }
            if ((int)Job == 9) //Esperth
            {
                TotalHealth = 2;
                Health = 2;
                Power = 20;
                DamageReduction = 3;
                Speed = 4;
                Value = rand.Next(20);
                Experience = rand.Next(20);
            }
            if ((int)Job == 10) //Harthie
            {
                TotalHealth = 15;
                Health = 15;
                Power = 5;
                DamageReduction = 1;
                Speed = 5;
                Value = rand.Next(15);
                Experience = rand.Next(15);
            }
        }
    }
}
