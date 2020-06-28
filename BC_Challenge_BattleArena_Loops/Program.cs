using System;
using System.Threading;

namespace LoopsExercise_BattleArena
{
    class Program
    {
        static void Main(string[] args)
        {

            // Variable Setup
            const int startingPanic = 10;
            const int startingStamina = 10;
            const int maxPanic = 20;
            const int minStamina = 0;
            const int enemyStartingLife = 10;

            // Trapped
            while (true)
            {
                Console.Clear();
                Console.WriteLine("You wake up in a cold sweat. You don't remember your dream but can't shake the lingering feeling of dread." + Environment.NewLine);
                Thread.Sleep(1500);
                Console.WriteLine("You stumble out your bedroom door and into...." + Environment.NewLine);
                Thread.Sleep(1500);
                Console.WriteLine("A BATTLE ARENA!" + Environment.NewLine);
                Thread.Sleep(1500);
                Console.WriteLine("The door slams shut behind you!" + Environment.NewLine);
                Thread.Sleep(1500);

                int playerStamina = startingStamina;
                int playerPanic = startingPanic;
                bool isPlayerDead = false;

                do
                {
                    // Enemy Enters
                    int newEnemyLife = enemyStartingLife;
                    bool isEnemyDead = false;
                    Console.WriteLine("An enemy enters the arena!" + Environment.NewLine);
                    Console.WriteLine("You have 10 chances to destroy the enemy, assuming you can keep your Panic and Stamina under control." + Environment.NewLine);

                    // Battle
                    for (int i = 10; i > 0; i--)
                    {
                        Console.WriteLine($"Your current Stamina is {playerStamina} and your current Panic level is {playerPanic}.");
                        Console.WriteLine($"Press any key to roll the die. You have {i} remaining rolls." + Environment.NewLine);
                        Console.ReadKey();

                        int diceRoll = new Random().Next(1, 7);

                        
                        // Determine Results
                        switch (diceRoll)
                        {
                            case 1:
                                Console.WriteLine("The enemy has struck you!");
                                playerPanic += 2;
                                break;
                            case 2:
                                Console.WriteLine("You have dodged the enemy attack.");
                                playerStamina--;
                                break;
                            case 3:
                                Console.WriteLine("You deflected the enemy attack");
                                break;
                            case 4:
                                Console.WriteLine("The enemy backs away.");
                                playerStamina++;
                                break;
                            case 5:
                                Console.WriteLine("The enemy dodges but you strike a glancing blow.");
                                playerPanic--;
                                newEnemyLife -= 3;
                                break;
                            case 6:
                                Console.WriteLine("The enemy can not defend and takes major damage.");
                                playerPanic -= 3;
                                playerStamina += 2;
                                newEnemyLife -= 5;
                                break;
                        }

                        Console.WriteLine();
                        if (newEnemyLife < 1)
                        {
                            Console.WriteLine("You have vanguished the enemy!" + Environment.NewLine);
                            isEnemyDead = true;
                            break;
                        }

                        // Update Stats
                        playerStamina--;
                        playerPanic++;

                        if (playerPanic >= maxPanic)
                        {
                            Console.WriteLine("Your panic level has gotten too high." + Environment.NewLine);
                            isPlayerDead = true;
                            break;
                        }
                        if (playerStamina <= minStamina)
                        {
                            Console.WriteLine("Your stamina has gotten too low." + Environment.NewLine);
                            isPlayerDead = true;
                            break;
                        }
                    }

                    Thread.Sleep(1500);

                    // Is anyone dead enemy dead
                    if (isEnemyDead)
                    {
                        // Fight another enemy                    
                        continue;
                    }
                    else if (isPlayerDead)
                    {
                        // Break from the Do...While
                        break;
                    }
                    else
                    {
                        Console.WriteLine("You have run out of chances and you have a panic attack." + Environment.NewLine);
                        Thread.Sleep(1500);
                        playerPanic = maxPanic;
                    }

                    Environment.Exit(0);

                } while (playerPanic < maxPanic || playerStamina > minStamina);


                Console.Write("You have died");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1000);
                Console.Write(".");
                Thread.Sleep(1500);

            }

        }
    }
}
