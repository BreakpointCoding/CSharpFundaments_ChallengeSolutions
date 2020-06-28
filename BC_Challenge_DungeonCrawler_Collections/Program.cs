using System;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Linq;
using System.Net.Http.Headers;

namespace CollectionsExercise_DungeonCrawler
{
    class Program
    {
        static void Main(string[] args)
        {
            DisplayGameInformation();
            bool playAgain = false;
            do
            {
                playAgain = PlayGame();

            } while (playAgain == true);

        }

        private static bool PlayGame()
        {
            // Build Enemy Collections
            Dictionary<string, int> enemyTypes = new Dictionary<string, int>();
            Stack<string> enemyDeck = new Stack<string>();
            BuildEnemyCollections(enemyTypes, enemyDeck);

            // Build Location Collections
            Dictionary<string, int> locations = new Dictionary<string, int>();
            Queue<string> dungeonMap = new Queue<string>();
            BuildLocationCollections(locations, dungeonMap);

            // Build Character Collection
            Dictionary<string, int> charactersHP = new Dictionary<string, int>();
            Dictionary<string, string> charactersNemesis = new Dictionary<string, string>();
            Dictionary<string, string> charactersWeakLocation = new Dictionary<string, string>();

            // Generate Character Information
            List<string> characterNames = new List<string>();
            NameTheCharacters(characterNames);
            GenerateCharacterHitPoints(characterNames, charactersHP);
            GenerateCharacterEnemyWeakness(characterNames, enemyTypes, charactersNemesis);
            GenerateCharacterLocationWeakness(characterNames, locations, charactersWeakLocation);

            // Crawl the Dungeon
            int locationCount = dungeonMap.Count;
            for (int i = 0; i < locationCount; i++)
            {
                Console.Clear();
                DisplayBanner("Current Character Health");
                foreach (var character in charactersHP)
                {
                    if (character.Value > 0)
                    {
                        Console.WriteLine($"{character.Key} has {character.Value} HP.");
                    }
                   
                }
                Console.WriteLine();

                string currentLocation = dungeonMap.Dequeue();
                int enemyCount = locations[currentLocation];
                // Don't make fighters with known weakness fight again
                List<string> badLocationFighters = new List<string>();

                Console.WriteLine($"The party has entered a {currentLocation}. You are attacked by {enemyCount} enemies.");
                Console.WriteLine();

                // Confront the enemies
                for (int j = 0; j < enemyCount; j++)
                {
                    bool isEnemyAlive = true;
                    string enemyType = enemyDeck.Pop();
                    int enemyDamage = enemyTypes[enemyType];

                    DisplayYellowText($"A {enemyType} attacks the party!");
                    Console.WriteLine();

                    // Don't make fighters with known weakness fight again
                    List<string> badEnemyFighters = new List<string>();

                    while (isEnemyAlive)
                    {
                        // Fight the enemy with a random character
                        Random random = new Random();
                        string fighter = characterNames[random.Next(0, characterNames.Count)];
                        if (badEnemyFighters.Contains(fighter) || badLocationFighters.Contains(fighter))
                        {
                            continue;
                        }

                        if (charactersWeakLocation[fighter] == currentLocation) // Is this the characters weak location
                        {
                            Console.WriteLine($"{fighter} can't fight while in a {currentLocation}!");
                            DisplayRedText($"The {enemyType} does {enemyDamage} damage to {fighter}.");
                            charactersHP[fighter] -= enemyDamage;
                            badLocationFighters.Add(fighter);
                        }
                        else if (charactersNemesis[fighter] == enemyType) // Is this the characters weak enemy
                        {
                            Console.WriteLine($"{fighter} is powerless against a {enemyType}!");
                            DisplayRedText($"The {enemyType} does {enemyDamage} damage to {fighter}.");
                            charactersHP[fighter] -= enemyDamage;
                            badEnemyFighters.Add(fighter);
                        }
                        else
                        {
                            DisplayWhiteText($"{fighter} has defeated the {enemyType}");
                            isEnemyAlive = false;
                        }

                        // Is the character dead
                        if (charactersHP[fighter] < 1)
                        {
                            DisplayRedText($"{fighter} has died!");
                            characterNames.Remove(fighter);
                        }
                        Console.WriteLine();
                        PauseGame();
                        Console.WriteLine();

                        // Are all characters dead?
                        if (characterNames.Count == 0)
                        {
                            goto PlayAgain;
                        }

                    } // Loop while enemy is alive


                } // Fight the next enemy

            } // Go to the next location

            // Play Again
            PlayAgain:

            Console.Clear();
            if (characterNames.Count > 0)
            {
                Console.WriteLine("At least one of your characters has survived the dungeon.");
                Console.WriteLine();
                DisplayBanner("Final Character Health");
                foreach (var character in charactersHP)
                {
                    if (character.Value > 0)
                    {
                        Console.WriteLine($"{character.Key} has {character.Value} HP.");
                    }

                }
            }
            else
            {
                Console.WriteLine("All of your characters have died!");
            }
            
            Console.WriteLine();

            Console.WriteLine("Care to see how you fare on another adventure? (y/n)");

            return GetLowerString() == "y" ? true : false;

        }

        private static void GenerateCharacterLocationWeakness(List<string> characterNames, Dictionary<string, int> locations, Dictionary<string, string> charactersWeakLocation)
        {
            // Every character has an location they are weak in. Generate this randomly
            Random random = new Random();

            foreach (string name in characterNames)
            {
                // Get a random index for an enemy
                int locationIndex = random.Next(0, locations.Count);

                // Since dictionaries don't have an index
                // Loop through the dictionary until counter = locationIndex
                int counter = 0;
                foreach (var location in locations)
                {
                    if (counter == locationIndex)
                    {
                        charactersWeakLocation.Add(name, location.Key);
                        // Exit the loop
                        break;
                    }
                    counter++;
                }
            }
        }

        private static void GenerateCharacterEnemyWeakness(List<string> characterNames, Dictionary<string, int> enemyTypes, Dictionary<string, string> charactersNemesis)
        {
            // Every character has an ememy they are weak against. Generate this randomly
            Random random = new Random();

            foreach (string name in characterNames)
            {
                // Get a random index for an enemy
                int enemyIndex = random.Next(0, enemyTypes.Count);

                // Since dictionaries don't have an index
                // Loop through the dictionary until counter = enemyIndex
                int counter = 0;
                foreach (var enemy in enemyTypes)
                {
                    if (counter == enemyIndex)
                    {
                        charactersNemesis.Add(name, enemy.Key);
                        // Exit the loop
                        break;
                    }
                    counter++;
                }
            }

        }

        private static void GenerateCharacterHitPoints(List<string> characterNames, Dictionary<string, int> charactersHP)
        {
            // Generate a random hitpoint value for every characture from 50 to 100 HP
            Random random = new Random();

            foreach (string name in characterNames)
            {
                int hitPoints = random.Next(50, 101);
                charactersHP.Add(name, hitPoints);
            }
        }

        private static void NameTheCharacters(List<string> characterNames)
        {
            int numberOfCharacters = 6;
            for (int i = 0; i < numberOfCharacters; i++)
            {
                Console.WriteLine($"What is the name of character #{i + 1}");
                characterNames.Add(GetTrimmedString());
            }
        }

        private static void BuildLocationCollections(Dictionary<string, int> locations, Queue<string> dungeon)
        {
            // Location Name and Number of Enemies (randomly generated)
            Random random = new Random();
            locations.Add("Dungeon Entrance", random.Next(2, 6));
            locations.Add("Dank Hallway", random.Next(2, 5));
            locations.Add("Deserted Dining Hall", random.Next(4, 7));
            locations.Add("Desecrated Crypt", random.Next(4, 7));
            locations.Add("Throne Room", random.Next(4, 8));
            locations.Add("Treasure Room", random.Next(8, 15));

            // Build Dungeon
            dungeon.Enqueue("Dungeon Entrance");
            dungeon.Enqueue("Dank Hallway");
            dungeon.Enqueue("Deserted Dining Hall");
            dungeon.Enqueue("Dank Hallway");
            dungeon.Enqueue("Desecrated Crypt");
            dungeon.Enqueue("Dank Hallway");
            dungeon.Enqueue("Throne Room");
            dungeon.Enqueue("Treasure Room");

        }

        private static void BuildEnemyCollections(Dictionary<string, int> enemyTypes, Stack<string> enemyDeck)
        {
            int numberOfEnemies = 100;

            //Enemy types and the damage they do (generated randomly)
            Random random = new Random();
            enemyTypes.Add("Warlock", random.Next(16, 27));
            enemyTypes.Add("Harpy", random.Next(20, 26));
            enemyTypes.Add("Goblin", random.Next(21,36));
            enemyTypes.Add("Ogre", random.Next(28, 48));
            enemyTypes.Add("Bandit", random.Next(32, 49));
            enemyTypes.Add("Troll", random.Next(33, 52));

            string[] enemies = enemyTypes.Keys.ToArray();

            //Stack of Enemies (generated randomly)
            for (int i = 0; i < numberOfEnemies; i++)
            {
                enemyDeck.Push(enemies[random.Next(0, enemies.Length)]);
            }

        }

        private static void DisplayGameInformation()
        {
            DisplayBanner("DUNGEON CRAWLER");
            Console.WriteLine();
            Console.WriteLine("Welcome to \"Dungeon Crawler\"!");
            DisplayYellowText("How many of your adventurers will make it out alive?");
            Console.WriteLine();
            PauseGame();

        }

        static void DisplayYellowText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void DisplayRedText(string text)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void DisplayWhiteText(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        private static void DisplayBanner(string message)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(new string('*', 50));
            int buffer = 24 - message.Length / 2;
            Console.Write("*" + new string(' ', buffer));
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write(new string(' ', buffer - 1) + "*" + Environment.NewLine);
            Console.WriteLine(new string('*', 50));
            Console.ForegroundColor = ConsoleColor.Gray;

        }

        static void PauseGame()
        {
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
        }

        static string GetLowerString()
        {
            return Console.ReadLine().Trim().ToLower();
        }

        static string GetTrimmedString()
        {
            return Console.ReadLine().Trim();
        }
    }
}
