using System;
using System.Collections.Generic;

namespace MethodsExercise_PitFighters
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                PlayGame();

                Console.WriteLine("Would you like to play again (Y/N)?");

                if (Console.ReadLine().Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    ExitGame();
                }
            }    
        }

        private static void PlayGame()
        {
            Console.Clear();
            List<string> playerOneFighters = new List<string>();
            Console.WriteLine("Player One...");
            PopulateFighterList(playerOneFighters);

            Console.WriteLine("Player Two...");
            List<string> playerTwoFighters = new List<string>();
            PopulateFighterList(playerTwoFighters);


            // Make sure the players have fighters left
            while (playerOneFighters.Count > 0 && playerTwoFighters.Count > 0)
            {
                Console.Clear();
                DisplayRemainingFighters("Player One", playerOneFighters);
                DisplayRemainingFighters("Player Two", playerTwoFighters);
                Battle(playerOneFighters,playerTwoFighters);

            }

            if (playerOneFighters.Count == 0)
            {
                Console.WriteLine("Player One: All of your fighters have died!");
            }
            else
            {
                Console.WriteLine("Player Two: All of your fighters have died!");
            }
           
        }

        private static void DisplayRemainingFighters(string playerName, List<string> fighters)
        {
            Console.WriteLine($"{playerName}: your remaining fighters are....");
            foreach (string fighter in fighters)
            {
                Console.WriteLine(fighter);
            }
            Console.WriteLine();
        }

        private static void Battle(List<string> playerOneFighters, List<string> playerTwoFighters)
        {
            int fighterOneIndex = GetRandomFighterIndex(playerOneFighters);
            int fighterTwoIndex = GetRandomFighterIndex(playerTwoFighters);
            string fighterOne = playerOneFighters[fighterOneIndex];
            string fighterTwo = playerTwoFighters[fighterTwoIndex];          

            Console.WriteLine($"The battle will be between {fighterOne} and {fighterTwo}");
            Console.WriteLine();

            GamePause();
            string weaponOne = GetWeaponChoice(fighterOne);
            GamePause();
            string weaponTwo = GetWeaponChoice(fighterTwo);
            GamePause();

            BattleResults(fighterOneIndex, fighterTwoIndex, weaponOne, weaponTwo, playerOneFighters, playerTwoFighters);
            
            GamePause();

        }

        private static void BattleResults(int fighterOneIndex, int fighterTwoIndex, string weaponOne, string weaponTwo, List<string> playerOneFighters, List<string> playerTwoFighters)
        {
            string fighterOne = playerOneFighters[fighterOneIndex];
            string fighterTwo = playerTwoFighters[fighterTwoIndex];

            Console.Clear();
            Console.WriteLine("The two fighters enter the pit!");
            Console.WriteLine($"{fighterOne} armed with a {weaponOne}. {fighterTwo} armed with a {weaponTwo}.");

            // Results are based of fighter one's weapon
            // Crossbow
            if (weaponOne == "crossbow")
            {
                switch (weaponTwo)
                {
                    case "crossbow":
                        Console.WriteLine("Both fighters fire a bolt in the heart of the other.");
                        Console.WriteLine("Both fighters have died!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "spear":
                        Console.WriteLine($"Before {fighterOne} can ready a bolt, a spear sails through air and skewers thier brain.");
                        Console.WriteLine($"{fighterOne} is dead!");
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "sword shield":
                        Console.WriteLine($"{fighterOne} fires a bolt, but {fighterTwo} blocks it with the shield.");
                        Console.WriteLine($"{fighterTwo} slashes with the sword and removes the head of {fighterOne}.");
                        Console.WriteLine($"{fighterOne} is dead!");
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "war hammer":
                    case "dagger":
                        Console.WriteLine($"{fighterTwo} charges with their {weaponTwo} held high!");
                        Console.WriteLine($"{fighterOne} fires a bolt between the eyes of {fighterTwo}!");
                        Console.WriteLine($"{fighterTwo} is dead!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        break;
                }
            }
            // Spear
            if (weaponOne == "spear")
            {
                switch (weaponTwo)
                {
                    case "crossbow":
                        Console.WriteLine($"While {fighterTwo} loads a bolt, {fighterOne} launches the spear.");
                        Console.WriteLine($" The head of {fighterTwo} explodes into a mess of gore.");
                        Console.WriteLine($"{fighterTwo} is dead!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        break;
                    case "spear":
                        Console.WriteLine("Both fighters lunge a spear into eachother's chest.");
                        Console.WriteLine("Both fighters have died!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "sword shield":
                        Console.WriteLine($"{fighterOne} throws the spear {fighterTwo} blocks it with the shield.");
                        Console.WriteLine($"{fighterTwo} slashes with the sword and removes the head of {fighterOne}.");
                        Console.WriteLine($"{fighterOne} is dead!");
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "war hammer":
                        Console.WriteLine($"Before {fighterTwo} can wield their heavy weapon, {fighterOne} hurls the spear.");
                        Console.WriteLine($"The head of {fighterTwo} explodes into a mess of gore.");
                        Console.WriteLine($"{fighterTwo} is dead!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        break;
                    case "dagger":
                        Console.WriteLine($"{fighterOne} throws the spear.");
                        Console.WriteLine($"{fighterTwo} deftly dodges the attack and slides the dagger between the ribs of {fighterOne}!");
                        Console.WriteLine($"{fighterOne} is dead!");
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                }
            }
            // Sword and Shield
            if (weaponOne == "sword shield")
            {
                switch (weaponTwo)
                {
                    case "crossbow":
                        Console.WriteLine($"{fighterTwo} fires a bolt, but {fighterOne} blocks it with the shield.");
                        Console.WriteLine($"{fighterOne} slashes with the sword and removes the head of {fighterTwo}.");
                        Console.WriteLine($"{fighterTwo} is dead!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        break;
                    case "spear":
                        Console.WriteLine($"{fighterTwo} throws the spear {fighterOne} blocks it with the shield.");
                        Console.WriteLine($"{fighterOne} slashes with the sword and removes the head of {fighterTwo}.");
                        Console.WriteLine($"{fighterTwo} is dead!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        break;

                    case "sword shield":
                        Console.WriteLine("The two fighters battle it out with sword and sheild.");
                        Console.WriteLine("After hours without vitory the match is called draw!");
                        Console.WriteLine("Both fighters are executed!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "war hammer":
                        Console.WriteLine($"{fighterTwo} attacks with the mighty warhammer, {fighterOne} defends with the shield but it shatters.");
                        Console.WriteLine($"The second blow of the warhammer caves in the skull of {fighterOne}.");
                        Console.WriteLine($"{fighterOne} is dead!");
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "dagger":
                        Console.WriteLine($"{fighterOne} slashes with the sword.");
                        Console.WriteLine($"{fighterTwo} deftly dodges the attack and slides the dagger between the ribs of {fighterOne}!");
                        Console.WriteLine($"{fighterOne} is dead!");
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                }
            }
            // Warhammer
            if (weaponOne == "war hammer")
            {
                switch (weaponTwo)
                {
                    case "crossbow":
                        Console.WriteLine($"{fighterOne} charges with their warhammer held high!");
                        Console.WriteLine($"{fighterTwo} fires a bolt between the eyes of {fighterOne}!");
                        Console.WriteLine($"{fighterOne} is dead!");
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "spear":
                        Console.WriteLine($"Before {fighterOne} can wield their heavy weapon, {fighterTwo} hurls the spear.");
                        Console.WriteLine($"The head of {fighterOne} explodes into a mess of gore.");
                        Console.WriteLine($"{fighterOne} is dead!");
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "sword shield":
                        Console.WriteLine($"{fighterOne} attacks with the mighty warhammer, {fighterTwo} defends with the shield but it shatters.");
                        Console.WriteLine($"The second blow of the warhammer caves in the skull of {fighterTwo}.");
                        Console.WriteLine($"{fighterTwo} is dead!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        break;
                    case "war hammer":
                        Console.WriteLine("Both fighters attack with thier mighty weapon.");
                        Console.WriteLine("Both fighters smash in the face of their opponent");
                        Console.WriteLine("Both fighters are executed!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "dagger":
                        Console.WriteLine($"{fighterTwo} approaches with a dagger in hand.");
                        Console.WriteLine($"{fighterOne} destroys {fighterTwo} without breaking a sweat!");
                        Console.WriteLine($"{fighterTwo} is dead!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        break;
                }
            }
            // Dagger
            if (weaponOne == "dagger")
            {
                switch (weaponTwo)
                {
                    case "crossbow":
                        Console.WriteLine($"Before {fighterOne} can approach with their dagger.");
                        Console.WriteLine($"{fighterTwo} fires a bolt between the eyes of {fighterOne}!");
                        Console.WriteLine($"{fighterOne} is dead!");
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "spear": 
                        Console.WriteLine($"{fighterTwo} throws the spear.");
                        Console.WriteLine($"{fighterOne} deftly dodges the attack and slides the dagger between the ribs of {fighterTwo}!");
                        Console.WriteLine($"{fighterTwo} is dead!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        break;
                    case "sword shield":
                        Console.WriteLine($"{fighterTwo} slashes with the sword.");
                        Console.WriteLine($"{fighterOne} deftly dodges the attack and slides the dagger between the ribs of {fighterTwo}!");
                        Console.WriteLine($"{fighterTwo} is dead!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        break;
                    case "war hammer":
                        Console.WriteLine($"{fighterOne} approaches {fighterTwo} with a dagger in hand.");
                        Console.WriteLine($"{fighterTwo} destroys {fighterOne} without breaking a sweat!");
                        Console.WriteLine($"{fighterOne} is dead!");
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                    case "dagger":
                        Console.WriteLine("Both fighters deftly strike with their daggers.");
                        Console.WriteLine($"{fighterOne} stabs the throat of {fighterTwo}.");
                        Console.WriteLine($"{fighterTwo} stabs the throat of {fighterOne}.");
                        Console.WriteLine("Both fighters are dead!");
                        playerTwoFighters.RemoveAt(fighterTwoIndex);
                        playerOneFighters.RemoveAt(fighterOneIndex);
                        break;
                }
            }

        }

        private static void GamePause()
        {
            Console.WriteLine();
            Console.WriteLine("Press any key to continue...");            
           Console.ReadKey();
        }

        private static string GetWeaponChoice(string fighter)
        {
            List<string> weapons = new List<string>()
            { "crossbow","spear","sword and shield", "war hammer", "dagger" };

            Console.Clear();
            Console.WriteLine($"{fighter} - choose your weapon.{Environment.NewLine}");
            Console.WriteLine("Your choices are the following:");

            for (int i = 0; i < weapons.Count; i++)
            {
                Console.WriteLine($"{i} - {weapons[i]} ");
            }

            bool isValidChoice = false;
            string input = "";
            string weaponChoice = "";
            while (isValidChoice == false)
            {
                Console.WriteLine("Enter the number next to the weapon of your choice.");
                input = Console.ReadLine();
                switch (input)
                {
                    case "0":
                    case "1":
                    case "2":
                    case "3":
                    case "4":
                        weaponChoice = weapons[int.Parse(input)];
                        Console.WriteLine($"{fighter} will go into battle with a {weaponChoice}.");
                        isValidChoice = true;
                        break;                        
                }
            }
            return weaponChoice;
        }

        private static int GetRandomFighterIndex(List<string> fighters)
        {
            Random random = new Random();
            return random.Next(0, fighters.Count);
        }

        private static void PopulateFighterList(List<string> fighters)
        {
            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine("What is your fighter's name?");
                fighters.Add(Console.ReadLine());
            }
        }

        private static void ExitGame()
        {
            Console.WriteLine("Press any key to exit the game.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
