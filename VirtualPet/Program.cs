using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPet
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();
            Console.WriteLine("Pokémon C#");
            Console.WriteLine("\n************\n");
            Console.WriteLine("Press any key start a new game.");
            Console.ReadKey();

            //Choose your Pokémon
            string type = "";
            bool chooseLoop = true;
            while (chooseLoop == true)
            {
                Console.Clear();
                Console.WriteLine("You wake up one morning to a text message from your mom.\n");
                Console.WriteLine("\"Hey, Professor Kukui wants to see you! Go over there!\"\n");
                Console.WriteLine("You walk over to the Professor's.\n");
                Console.WriteLine("He greets you: \"Hey! I'm giving you a Pokémon! Pick one!\"\n");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1.) Rowlet, a Grass Quill Pokémon.");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("2.) Litten, a Fire Cat Pokémon.");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("3.) Popplio, a Sea Lion Pokémon.\n");
                Console.ResetColor();
                Console.Write("Which one will you pick? ");
                type = Console.ReadLine();
                if (type != "1" && type != "2" && type != "3")
                {
                    Console.WriteLine("\nInvalid selection. Enter a number from 1 to 3.");
                }
                else
                {
                    chooseLoop = false;
                }
            }

            //Creates a Pokémon based on the input above.
            VirtualPet Monster = new VirtualPet(type);
            Inventory Backpack = new Inventory();

            Console.Clear();
            Console.Write("You picked ");
            Monster.WriteName();
            Console.Write("!\n\n");
            Console.WriteLine("Press any key to start your adventure!");
            Console.ReadKey();

            bool mainLoop = true;
            while (mainLoop == true)
            {
                Console.Clear();
                Console.WriteLine("What do you want to do?");
                Console.WriteLine("\n***********************\n");
                Console.WriteLine("1.) Go out and fight!");
                Console.WriteLine("2.) Do some training");
                Console.WriteLine("3.) Take a day off");
                Console.WriteLine("4.) Use an item");
                Console.WriteLine("5.) Go to the store");
                Console.WriteLine("6.) Quit\n");
                Monster.Stats();
                Console.WriteLine();
                Backpack.List();

                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Fight(Monster, Backpack);
                }
                else if (choice == "2")
                {
                    Train();
                }
                else if (choice == "3")
                {
                    DayOff();
                }
                else if (choice == "4")
                {
                    //The boolean is passed here because it's needed
                    //when this method is called during combat
                    Backpack.UseItem(Monster, false);
                }
                else if (choice == "5")
                {
                    Backpack.BuyItem();
                }
                else if (choice == "6")
                {
                    mainLoop = false;
                }
                else
                {
                    Console.WriteLine("\nInvalid choice. Press any key to continue.");
                    Console.ReadKey();
                }
            }
        }

        static void Fight(VirtualPet Monster, Inventory Backpack)
        {
            string enemyType = "";
            Random rand = new Random();
            int selector = rand.Next(1, 3);

            if (selector == 1)
            {
                enemyType = "Rockruff";
            }
            else if (selector == 2)
            {
                enemyType = "Komala";
            }
            else if (selector == 3)
            {
                enemyType = "Grubbin";
            }

            int enemyHPMax = Monster.HPMax() + rand.Next(-20, 20);
            int enemyHP = enemyHPMax;
            bool runAway = false;

            Console.Clear();
            Console.WriteLine("A wild " + enemyType + " appeared!\n");
            Console.WriteLine("Press any key to continue!");
            Console.ReadKey();

            //Full battle loop
            while (Monster.HP() > 0 && enemyHP > 0 && runAway == false)
            {
                bool yourTurn = true;

                //Your turn
                while (yourTurn == true)
                {
                    Console.Clear();
                    Console.Write("The " + enemyType + " stares down your ");
                    Monster.WriteName();
                    Console.Write("!\n\n");
                    Console.WriteLine("What do you do?");
                    Console.WriteLine("\n*****************\n");
                    Console.WriteLine("1.) " + Monster.Attack1Name());
                    Console.Write("2.) ");
                    Monster.Color();
                    Console.Write(Monster.Attack2Name() + "\n");
                    Console.ResetColor();
                    Console.WriteLine("3.) Use item");
                    Console.WriteLine("4.) Run away\n");
                    Monster.Stats();
                    Console.WriteLine("\n" + enemyType + "'s HP: " + enemyHP + " / " + enemyHPMax);
                    Console.Write("\nEnter your choice: ");

                    string control = Console.ReadLine();

                    //Regular attack
                    if (control == "1")
                    {
                        Console.Clear();
                        enemyHP = enemyHP - Monster.Attack1(enemyType);
                        Console.ReadKey();
                        yourTurn = false;
                    }
                    //Special attack
                    else if (control == "2")
                    {
                        Console.Clear();
                        enemyHP = enemyHP - Monster.Attack2(enemyType);
                        Console.ReadKey();
                        yourTurn = false;
                    }
                    //Use item
                    else if (control == "3")
                    {
                        Console.Clear();
                        Backpack.UseItem(Monster, yourTurn);
                    }
                    //Try to run
                    else if (control == "4")
                    {
                        Console.Clear();
                        Random runChance = new Random();
                        if (runChance.Next(0, 100 + Monster.Happiness()) > 25)
                        {
                            runAway = true;
                            Console.WriteLine("You got away!\n");
                            Console.Write("...but ");
                            Monster.WriteName();
                            Console.Write(" isn't happy you had to run...\n\n");
                            Monster.Run();
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            yourTurn = false;
                        }
                        else
                        {
                            Console.WriteLine("Couldn't get away!\n");
                            Console.WriteLine("Press any key to continue.");
                            Console.ReadKey();
                            yourTurn = false;
                        }
                    }
                    //Invalid choice
                    else
                    {
                        Console.WriteLine("\nInvalid selection. Press any key to continue.");
                        Console.ReadKey();
                    }
                }

                //Enemy's turn
                if (yourTurn == false && runAway == false && enemyHP > 0)
                {
                    Console.Clear();
                    Random randDamage = new Random();
                    int damage = Convert.ToInt32(Monster.Attack() + randDamage.Next(-5, 5) - 0.5 * Monster.Happiness());
                    if (damage < 1)
                    {
                        damage = 1;
                    }
                    Monster.Damage(enemyType, damage);
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                }
                
            }

            //Post battle goes here
            if (runAway == true)
            {
                //Do nothing, just return to the main menu
            }
            else if (enemyHP <= 0)
            {
                Console.Clear();
                Console.WriteLine("The wild " + enemyType + " fainted!\n");
                Monster.LevelUp(enemyHPMax);

                Random moneyRand = new Random();
                int moneyFound = moneyRand.Next(5, 100 + Monster.Happiness());
                Backpack.GetMoney(moneyFound);

                Console.WriteLine("\nFound $" + moneyFound + "!\n");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }
            else if (Monster.HP() <= 0)
            {
                Console.Clear();
                Monster.Faint();
                Monster.WriteName();
                Console.WriteLine(" fainted!\n");
                Console.Write("You bring ");
                Monster.WriteName();
                Console.Write(" to a Pokémon Center who fix him up. They charge you $" + Backpack.PokemonCenter() + ".\n");
                Monster.WriteName();
                Console.Write(" isn't happy that he took such a beating...\n\n");
                Console.WriteLine("Press any key to continue.");
                Console.ReadKey();
            }

        }

        static void Train()
        {
            Console.Clear();
            Console.WriteLine("Training!");
            Console.ReadKey();
        }

        static void DayOff()
        {
            Console.Clear();
            Console.WriteLine("Day off!");
            Console.ReadKey();
        }

    }
}
