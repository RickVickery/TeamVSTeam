using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace TeamVsTeam
{
    class Program
    {
        static void Main(string[] args)
        {
            //If(File?LoadFile);
            //Else {
            InitializeGame();
            //}
            bool running = true;
            while (running)
            {
                OrderOfEvents();
            }
        }
        public static void AddGuards()
        {
            // 1-2 Random heroes per night.
            Console.WriteLine("Spend $20 to hire another guard?");
            Console.WriteLine($"Your funds: {Team.Funds}");
            Console.WriteLine("");
            if (Team.Funds >= 20)
            {
                Console.WriteLine("1. Yes");
                Console.WriteLine("2. No ");
                GetValidChoice(2, out int choice);
                if (choice == 1)
                {
                    Team.Funds -= 20;
                    Unit unit = new Unit();
                    unit.GenerateHumanUnit();
                    Team.team.Add(unit);
                }
            }
            else
            {
                Console.WriteLine("Looks like you can't afford it.");
                Console.WriteLine("Press enter to return.");
                Console.ReadLine();
            }
        }
        public static void ApplyFighterBuffs(int fighters)
        {
            foreach(Unit u in Team.team)
            {
                u.TotalHealth += fighters * 3;
                u.Health += fighters * 3;
            }
        }
        public static void ApplyMageBuffs(int mages)
        {
            foreach (Unit u in Team.team)
            {
                u.Power += mages;
            }
        }
        public static bool AreRoutesSecure()
        {
            if (Team.Secure == true)
            {
                return true;
            }
            return false;
        }
        public static void CampPhase()
        {
            bool camping = true;
            while (camping)
            {
                ShowCampMenu(out camping);
            }
        }       
        public static void CheckHunger(Unit u)
        {
            if(u.Hunger > 0)
            {
                u.WellFed = true;
                u.Starving = false;
            }
            if(u.Hunger == 0)
            {
                u.WellFed = false;
                u.Starving = false;
            }
            if(u.Hunger < 0)
            {
                u.WellFed = false;
                u.Starving = true;
            }
        }  
        public static void CheckLevelUp(Unit u)
        {
            if(u.Experience >= u.TotalExp)
            {
                StatChange(u);
            }
        }
        public static void CriticalCheck()
        {
            foreach (Unit u in Team.team)
            {
                if (u.Critical == true)
                {
                    Team.team.Remove(u);
                }
            }
        }
        public static void StatChange(Unit u)
        {
            u.Level += 1;
            u.Experience = 0;
            u.TotalExp += Math.Ceiling(u.TotalExp * .2);
            u.TotalHealth += Math.Ceiling(u.TotalHealth * .10);
            u.Health = u.TotalHealth;
            u.Power += Math.Ceiling(u.Power * .10);
        }
        public static void CombatPhase()
        {           
            bool combat = true;
            bool enemyalive = true;           
            while (combat)
            {
                Team.ViewTeam(); EnemyTeam.ViewTeam();
                if (DecideRound())
                {
                    //Player
                    for (int i = 0; i < Team.CountOfTeam(); i++)
                    {

                        if (EnemyTeam.team.Count == 0)
                        {
                            enemyalive = false;
                            combat = false;
                            break;
                        }
                        DamageExchange();
                        if (EnemyTeam.team[0].Health <= 0)
                        {
                            Team.Funds += EnemyTeam.team[0].Value;
                            Team.team[0].Experience += EnemyTeam.team[0].Experience;
                            GetEXP(Team.team[0], EnemyTeam.team[0]);
                            CheckLevelUp(Team.team[0]);
                            EnemyTeam.team.Remove(EnemyTeam.team[0]);
                        }
                        else
                        {
                            EnemyMoveToRear();
                        }
                        MoveToRear();
                    }

                    //Enemy
                    if (enemyalive)
                    {
                        for (int i = 0; i < EnemyTeam.CountOfTeam(); i++)
                        {
                            if (Team.team.Count == 0)
                            {
                                YouLose();
                            }

                            EnemyDamageExchange();
                            EnemyMoveToRear();

                            if (Team.team[0].Health <= 0)
                            {
                                Team.team.Remove(Team.team[0]);
                            }
                            else
                            {
                                MoveToRear();
                            }
                        }
                    }
                }

                else if (!DecideRound())
                {
                    //Enemy
                    for (int i = 0; i < EnemyTeam.CountOfTeam(); i++)
                    {
                        if (Team.team.Count == 0)
                        {
                            YouLose();
                        }
                        EnemyDamageExchange();
                        EnemyMoveToRear();
                        if (Team.team[0].Health <= 0)
                        {
                            Team.team.Remove(Team.team[0]);
                        }
                        else
                        {
                            MoveToRear();
                        }
                    }

                    //Player
                    for (int i = 0; i < Team.CountOfTeam(); i++)
                    {

                        if (EnemyTeam.team.Count == 0)
                        {
                            enemyalive = false;
                            combat = false;
                            break;
                        }
                        DamageExchange();
                        if (EnemyTeam.team[0].Health <= 0)
                        {
                            Team.Funds += EnemyTeam.team[0].Value;
                            Team.team[0].Experience += EnemyTeam.team[0].Experience;
                            GetEXP(Team.team[0], EnemyTeam.team[0]);
                            CheckLevelUp(Team.team[0]);
                            EnemyTeam.team.Remove(EnemyTeam.team[0]);
                        }
                        else
                        {
                            EnemyMoveToRear();
                        }
                        MoveToRear();
                    }
                }
                else
                {
                    Team.ViewTeam(); EnemyTeam.ViewTeam();
                    if (DecideRound())
                    {
                        //Player
                        for (int i = 0; i < Team.CountOfTeam(); i++)
                        {

                            if (EnemyTeam.team.Count == 0)
                            {
                                enemyalive = false;
                                combat = false;
                                break;
                            }
                            DamageExchange();
                            if (EnemyTeam.team[0].Health <= 0)
                            {
                                Team.Funds += EnemyTeam.team[0].Value;
                                Team.team[0].Experience += EnemyTeam.team[0].Experience;
                                GetEXP(Team.team[0], EnemyTeam.team[0]);
                                CheckLevelUp(Team.team[0]);
                                EnemyTeam.team.Remove(EnemyTeam.team[0]);
                            }
                            else
                            {
                                EnemyMoveToRear();
                            }
                            MoveToRear();
                        }

                        //Enemy
                        if (enemyalive)
                        {
                            for (int i = 0; i < EnemyTeam.CountOfTeam(); i++)
                            {
                                if (Team.team.Count == 0)
                                {
                                    combat = false;
                                    YouLose();
                                }

                                EnemyDamageExchange();
                                EnemyMoveToRear();

                                if (Team.team[0].Health <= 0)
                                {
                                    Team.team.Remove(Team.team[0]);
                                }
                                else
                                {
                                    MoveToRear();
                                }
                            }
                        }
                    }
                }
            }                       
        }
        public static void CountFighters(out int x)
        {
            x = Team.CountOfFighter();
        }
        public static void CountMages(out int x)
        {
            x = Team.CountOfMage();
        }
        public static void CountRogues(out int x)
        {
            x = Team.CountOfRogue();
        }
        public static void DamageExchange()
        {
            try
            {
                Console.Clear();
                if (Team.team[0].WellFed == true)
                {
                    double totalDamage = Math.Ceiling(Team.team[0].Power * 1.5) - (EnemyTeam.team[0].DamageReduction);
                    Console.WriteLine($"{Team.team[0]} is surging with power from a perfectly cooked meal!!");
                    Console.WriteLine($"{Team.team[0]} has dealt {totalDamage} damage to {EnemyTeam.team[0]}");
                    EnemyTeam.team[0].Health -= totalDamage;
                    Console.WriteLine("");
                    Console.WriteLine("Push enter to resume.");
                    Console.ReadLine();
                }
                else if(Team.team[0].WellFed == false && Team.team[0].Starving == false)
                {
                    double totalDamage = Team.team[0].Power - EnemyTeam.team[0].DamageReduction;
                    Console.WriteLine($"{Team.team[0]} has dealt {totalDamage} damage to {EnemyTeam.team[0]}");
                    EnemyTeam.team[0].Health -= totalDamage;
                    Console.WriteLine("");
                    Console.WriteLine("Push enter to resume.");
                    Console.ReadLine();
                }
                else
                {
                    double totalDamage = (Math.Ceiling(Team.team[0].Power * .8)) - EnemyTeam.team[0].DamageReduction;
                    Console.WriteLine($"{Team.team[0]} is feeling the effects of starvation...");
                    Console.WriteLine($"{Team.team[0]} has dealt {totalDamage} damage to {EnemyTeam.team[0]}");
                    EnemyTeam.team[0].Health -= totalDamage;
                    Console.WriteLine("");
                    Console.WriteLine("Push enter to resume.");
                    Console.ReadLine();
                }
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
            }
        }
        public static bool DecideRound()
        {
            if (GetHeroSpeed() > GetEnemySpeed())
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static void EnemyDamageExchange()
        {
            try
            {
                double totalDamage = EnemyTeam.team[0].Power - Team.team[0].DamageReduction;
                Team.team[0].Health -= totalDamage;
                Console.Clear();
                Console.WriteLine($"{EnemyTeam.team[0]} has dealt {totalDamage} damage to {Team.team[0]}");
                Console.WriteLine("");
                Console.WriteLine("Push enter to resume.");
                Console.ReadLine();
            }
            catch
            {
                Console.Clear();
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
            }

        }
        public static void EnemyMoveToRear()
        {
            EnemyTeam.team.Add(EnemyTeam.team[0]);
            EnemyTeam.team.Remove(EnemyTeam.team[0]);
        }
        public static void FeedGuards()
        {
            Console.Clear();
            int total = Team.CountOfTeam();
            Team.ViewTeamChoice();

            if (Team.Funds < 5)
            {
                Console.WriteLine("");
                Console.WriteLine("Come back when you can afford some grub!");
                Console.WriteLine("");
                Console.WriteLine("Press enter to continue.");
                Console.WriteLine("");
                Console.ReadLine();
            }

            else
            {
                Console.WriteLine("Which hero would you like to feed?");
                Console.WriteLine($"$5ea - {Team.FoodQuant} left tonight");
                Console.WriteLine($"Or type {total + 1} to return.");
                Console.WriteLine("");

                GetValidChoice(total + 1, out int choice);
                if (choice == total + 1)
                {
                    return;
                }
                if (Team.feedGuard(Team.team[choice - 1]))
                {
                    Team.team[choice - 1].Hunger++;
                    CheckHunger(Team.team[choice - 1]);
                    Team.Funds -= 5;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("Looks like you're out of food...");
                    Console.WriteLine("Perhaps use some of that cash to secure the roads for more food tomorrow?");
                    Console.WriteLine("");
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                }
            }
        }
        public static double GetEnemySpeed()
        {
            double total = 0;
            foreach (Unit u in EnemyTeam.team)
            {
                total += u.Speed;
            }
            double average = EnemyTeam.team.Count / total;
            return average;
        }
        public static double GetHeroSpeed()
        {
            double total = 0;
            foreach (Unit u in Team.team)
            {
                total += u.Speed;
            }
            double average = total / Team.team.Count;
            return average;
        }
        public static void GetValidChoice(int limit, out int choice)
        {
            bool isValid = int.TryParse(Console.ReadLine(), out choice);
            while (!isValid || choice <= 0 || choice > limit)
            {
                Console.WriteLine("Please enter a valid choice.");
                isValid = int.TryParse(Console.ReadLine(), out choice);
            }
        }
        public static void GenerateHeros()
        {
            for (int i = 0; i < 6; i++)
            {
                Unit unit = new Unit();
                unit.GenerateHumanUnit();
                Team.team.Add(unit);
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
        public static void GetEXP(Unit u, Unit u2)
        {
            u.Experience += u2.Experience;
        }
        public static void GuardMenu()
        {
            bool guarding = true;
            while (guarding)
            {
                Console.Clear();
                if (Team.ScoutsSent == true)
                {
                    ScoutingGUI();
                }
                else
                {
                    Team.ViewTeam();
                }
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. Work Orders");
                Console.WriteLine("2.  Add Guards");
                Console.WriteLine("3. Heal Guards");
                Console.WriteLine("4. Feed Guards");
                Console.WriteLine("5. Send Scouts");
                Console.WriteLine("6. Secure Roads");
                Console.WriteLine("7. Return");

                Console.WriteLine("");
                Console.WriteLine($"Current Funds: {Team.Funds:C2}");
                Console.WriteLine($"Average Speed: {GetHeroSpeed():F2}");

                GetValidChoice(7, out int choice);
                if (choice == 1) { SortGuards(); }
                if (choice == 2) { AddGuards(); }
                if (choice == 3) { HealGuards(); }
                if (choice == 4) { FeedGuards(); }
                if (choice == 5) { Scout(); }
                if (choice == 6) { SecureRoutes(); }
                if (choice == 7) { guarding = false; }
            }
        }
        public static void HealGuards()
        {
            int total = Team.CountOfTeam();

            Console.Clear();
            Team.ViewTeamChoice();

            if (Team.Funds >= 10)
            {
                Console.WriteLine("Which hero would you like to heal?");
                Console.WriteLine($"$10ea - {Team.HealQuant} left tonight");
                Console.WriteLine("");
                GetValidChoice(total, out int choice);

                if (Team.healGuard())
                {
                    Team.team[choice - 1].Health = Team.team[choice - 1].TotalHealth;
                    Team.team[choice - 1].Critical = false;
                    Team.Funds -= 10;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("");
                    Console.WriteLine("Looks like you're out of heals!");
                    Console.WriteLine("");
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                }

            }
        }
        public static void HealReset(int x)
        {
            Team.HealQuant += x;
            Team.FoodQuant += x;
        }
        public static void HungerToll()
        {
            foreach (Unit u in Team.team)
            {
                if (u.Hunger >= 0)
                {
                    u.Hunger--;
                    CheckHunger(u);
                }
            }
        }
        public static void InitializeGame()
        {
            GenerateHeros();
            GenerateEnemies();
        }

        public static void Ideas()
        {
            //Equipment
            //Multiple actions per turn, RNG, growth dependent
            //Special Abilities (Unlockable from wearing equipment long enough?)
            //DayClass or TimeTracker, add events on certain days (supplies, provisions, funds from main base) (bonus loot nights) (no enemy nights, low enemy nights, extra enemy nights, boss nights) 
            //Scouts are free, only usable every other day
            //Reduce the number of enters pressed
            //For loop with delay + setcursor in place to overwrite and count up in place for GP or EXP visual?
            //Look at team displays in the combatphase()
        }
        public static void MoveToRear()
        {
            Team.team.Add(Team.team[0]);
            Team.team.Remove(Team.team[0]);
        }
        public static void OrderOfEvents()
        {
            CampPhase();
            CriticalCheck();
            CountFighters(out int fighters);
            CountMages(out int mages);
            CountRogues(out int rogues);
            ApplyFighterBuffs(fighters);
            ApplyMageBuffs(mages);
            CombatPhase();
            YouWin(); //Add Loot acquired / Exp when implemented / Damage taken totals to QoL all in one informational display
            TimeMarchesOn();
            ThievesEventCheck(rogues);
            RemoveFighterBuffs(fighters);
            RemoveMageBuffs(mages);
            GenerateEnemies();
        }
        public static void RemoveFighterBuffs(int fighters)
        {
            foreach (Unit u in Team.team)
            {
                u.TotalHealth -= fighters * 3;
                u.Health -= fighters * 3;

                if(u.Health < 1)
                {
                    u.Health = 1;
                    u.Critical = true;
                }
            }
        }
        public static void RemoveMageBuffs(int mages)
        {
            foreach (Unit u in Team.team)
            {
                u.Power -= mages;
            }
        }
        public static void RoutesReset()
        {
            Team.Secure = false;
        }
        public static void Scout()
        {
            if (Team.ScoutsSent == true)
            {
                Console.Clear();
                Console.WriteLine("We're bought and paid for! Expect a report soon.");
                Console.WriteLine("");
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
                return;
            }
            Console.WriteLine("Looking for scouting information?");
            Console.WriteLine("");
            Console.WriteLine("1. Yes");
            Console.WriteLine("2. No");
            GetValidChoice(2, out int choice);
            if (choice == 1 && Team.Funds > 10)
            {
                Console.Clear();
                Team.ScoutsSent = true;
                Console.WriteLine("We're bought and paid for! Expect a report soon.");
                Console.WriteLine("");
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
            }
            if (choice == 1 && Team.Funds < 10)
            {
                Console.Clear();
                Console.WriteLine("Sorry, you can't afford us tonight.");
                Console.WriteLine("You might regret this in the morning.");
                Console.WriteLine("");
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
            }
            if (choice == 2)
            {
                return;
            }
        }
        public static void ScoutingGUI()
        {
            for (int i = 0; i < Team.team.Count; i++)
            {
                if (i >= EnemyTeam.team.Count && i < Team.team.Count) { Console.WriteLine($"{ShowUnit(i),-20}"); }
                else if (i >= Team.team.Count && i < EnemyTeam.team.Count) { Console.WriteLine($"{ShowEnemyUnit(i),7}"); }
                else { Console.WriteLine($"{ShowUnit(i),-20}{ShowEnemyUnit(i),7}"); }
            }
        }
        public static void ScoutsReset()
        {
            Team.ScoutsSent = false;
        }
        public static void SecureRoutes()
        {
            Console.Clear();
            if (Team.Funds >= 20)
            {
                Console.WriteLine("I know of a few lads willing to make sure the roads are clear...");
                Console.WriteLine("For a price he says.  Pay the man $20? (Y/N)");
                string answer = Console.ReadLine().ToLower();
                if (answer == "y")
                {
                    Team.Funds -= 20;
                    Team.Secure = true;
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("Bugger off then.");
                    Console.WriteLine("");
                    Console.WriteLine("Press enter to continue.");
                    Console.ReadLine();
                    return;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Evenin~ Best you come back with some more money, no time for just chitchat.");
                Console.WriteLine("");
                Console.WriteLine("Press enter to continue, return with $20 or more.");
                Console.ReadLine();
                return;
            }
        }
        public static void ShowCampMenu(out bool camping)
        {
            bool asking = true;
            while (asking)
            {
                Console.Clear();
                Team.ViewTeam();

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("");
                Console.WriteLine("What would you like to do?");
                Console.WriteLine("1. View Rules");
                Console.WriteLine("2. Edit Guards");
                Console.WriteLine("3. Begin Shift");

                Console.WriteLine("");
                Console.WriteLine($"Current Funds: {Team.Funds:C2}");
                Console.WriteLine($"Average Speed: {GetHeroSpeed():F2}");

                GetValidChoice(3, out int choice);
                if (choice == 1)
                {
                    //Display Rules & Advice for new players.
                    //ShowHints();
                }
                if (choice == 2)
                {

                    GuardMenu();
                    //Display list of Human Team
                    //Change order options within

                }
                if (choice == 3)
                {
                    break;
                }
            }
            camping = false;
        }
        public static Unit ShowEnemyUnit(int i)
        {
            return EnemyTeam.team[i];
        }
        public static void ShowRules()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Team Vs. Team!");
            Console.WriteLine("This is a strategy game where you need to survive monster waves each night!");
            Console.WriteLine("+ means that your unit is well fed, they will receive a damage boost from this!");
            Console.WriteLine("- means that your unit is starving, they will receive a damage nerf from this!");
            Console.WriteLine("* represents how many levels beyond the base level your unit has risen! They will see small stat boosts for each one.");
            Console.WriteLine("");
            Console.WriteLine("Tips:");
            Console.WriteLine("1.Hero placement can make all the difference.");
            Console.WriteLine("2.Utilize the scout feature to get a heads up on the enemy line up.");
            Console.WriteLine("3.Feeding guards will boost their attack power for one night.");
            Console.WriteLine("4.Securing routes will boost your heal and food recovery for that night.");
            Console.WriteLine("");
            Console.WriteLine("Passive Bonuses:");
            Console.WriteLine("Rogue: High speed, low power, useful to have some to make sure you go first. Helps to mitigate gold reduction on steal event.");
            Console.WriteLine("Mage: High power, drags speed average down, boosts party attack power by 1 for each Mage present.");
            Console.WriteLine("Fighter: Support health, boosts HP by 5 per fighter.");
            Console.WriteLine("");
            Console.WriteLine("Special Event Abilities to come!");
            Console.WriteLine("");
            Console.WriteLine("Press Enter to return.");
            Console.ReadLine();

        }
        public static Unit ShowUnit(int i)
        {
            return Team.team[i];
        }
        public static void SortGuards() 
        {
            int total = Team.CountOfTeam();
            Console.Clear();
            Team.ViewTeamChoice();
            Console.WriteLine("");
            Console.WriteLine("Which hero are you trying to move?");
            GetValidChoice(total, out int choice);
            Console.WriteLine("");
            Console.WriteLine("Where are they moving?");
            GetValidChoice(total, out int choice2);
            Unit unit = new Unit();
            unit = Team.team[choice - 1];
            Team.team.Remove(Team.team[choice - 1]);
            Team.team.Insert(choice2 - 1,unit);
            
            
        }
        public static void ThievesEventCheck(int rogues)
        {
            Random rand = new Random();
            int chance = rand.Next(1,(10 + rogues));

            double stolen = Math.Ceiling(Team.Funds * (.10 - (rogues * .10)));
            if (chance < 2)
            {
                Console.Clear();
                Console.WriteLine("Oh no! Bandits made off with some loot while we were distracted!");
                Console.WriteLine($"They took {stolen:C2}");
                Team.Funds -= stolen;
                Console.WriteLine("");
                Console.WriteLine("Press enter to continue.");
                Console.ReadLine();
            }

        }
        public static void TimeMarchesOn()
        {
            Random rand = new Random();
            int x;

            if (AreRoutesSecure()) { x = rand.Next(4, 5); } else { x = 1; }
            HealReset(x);
            RoutesReset();
            ScoutsReset();
            HungerToll();
        }
        public static void YouWin()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Congratulations for surviving the night!");
            Console.WriteLine("");
            Console.WriteLine("Press enter to continue onward;");
            Console.ReadLine();

        }
        public static void YouLose()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Your team lies dead against the waves of darkness.");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Game over.");
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
            Environment.Exit(1);
        }
    }
}
