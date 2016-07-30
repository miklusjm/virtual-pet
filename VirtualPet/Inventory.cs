using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPet
{
    class Inventory
    {
        //Stuff in your inventory
        private int potions;
        private int ethers;
        private int elixirs;
        private int money;

        //Everyone starts with 2 potions and $500
        public Inventory()
        {
            potions = 2;
            money = 500;
        }

        //Lists the contents of your inventory
        public void List()
        {
            Console.WriteLine("Inventory");
            if (potions > 0)
            {
                Console.WriteLine("Potions:   " + potions);
            }
            if (ethers > 0)
            {
                Console.WriteLine("Ethers:    " + ethers);
            }
            if (elixirs > 0)
            {
                Console.WriteLine("Elixirs:   " + elixirs);
            }
            Console.WriteLine("Money:     $" + money );
        }

        //Using an item
        //First parameter is the monster to use it on
        //Second parameter controls turn flow in combat --
        //If you use an item, it counts as your turn,
        //but if you cancel or try to use an item you don't have, it doesn't
        public void UseItem(VirtualPet Monster, bool turn)
        {
            Console.Clear();
            Console.WriteLine("What do you want to use?");
            Console.WriteLine("\n**************************\n");
            Console.WriteLine("1.) Potions:   " + potions);
            Console.WriteLine("2.) Ethers:    " + ethers);
            Console.WriteLine("3.) Elixirs:   " + elixirs);
            Console.WriteLine("4.) Nothing\n");
            Console.Write("Enter your choice: ");

            string control = Console.ReadLine();
            Console.WriteLine();
            Random rand = new Random();

            if (control == "1")
            {
                if (potions > 0)
                {
                    Monster.Heal(25 + rand.Next(0, Monster.Happiness()));
                    potions--;
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    turn = false;
                }
                else
                {
                    Console.WriteLine("You don't have any!\n");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
            else if (control == "2")
            {
                if (ethers > 0)
                {
                    Monster.Recover(10 + rand.Next(0, Convert.ToInt32(0.25 * Monster.Happiness())));
                    ethers--;
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    turn = false;
                }
                else
                {
                    Console.WriteLine("You don't have any!\n");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
            else if (control == "3")
            {
                if (elixirs > 0)
                {
                    Monster.Heal(25 + rand.Next(0, Convert.ToInt32(1.5 * Monster.Happiness())));
                    Monster.Recover(10 + rand.Next(0, Convert.ToInt32(0.5 * Monster.Happiness())));
                    elixirs--;
                    Console.WriteLine("\nPress any key to continue.");
                    Console.ReadKey();
                    turn = false;
                }
                else
                {
                    Console.WriteLine("You don't have any!\n");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
            }
            else
            {
            }
        }

        //Buying an item
        public void BuyItem()
        {
            Console.Clear();
            Console.WriteLine("What do you want to buy?\n");
            Console.WriteLine("    Item Name\tYou Have   Price");
            Console.WriteLine("**********************************");
            Console.WriteLine("1.) Potions:\t   " + potions + "\t    $ 50");
            Console.WriteLine("2.) Ethers:\t   " + ethers + "\t    $100");
            Console.WriteLine("3.) Elixirs:\t   " + elixirs + "\t    $250");
            Console.WriteLine("4.) Nothing\n");
            Console.Write("Enter your choice: ");
            string choice = Console.ReadLine();
            Console.WriteLine();

            if (choice == "1")
            {
                if (money >= 50)
                {
                    Console.WriteLine("You bought a potion.");
                    potions++;
                    money -= 50;
                }
                else
                {
                    Console.WriteLine("You don't have enough money!");
                }
            }
            else if (choice == "2")
            {
                if (money >= 100)
                {
                    Console.WriteLine("You bought an ether.");
                    ethers++;
                    money -= 100;
                }
                else
                {
                    Console.WriteLine("You don't have enough money!");
                }
            }
            else if (choice == "3")
            {
                if (money >= 250)
                {
                    Console.WriteLine("You bought an elixir.");
                    elixirs++;
                    money -= 250;
                }
                else
                {
                    Console.WriteLine("You don't have enough money!");
                }
            }
            else
            {
                //Does nothing and returns to the main menu.
            }
        }

        //Used when your Pokémon finds an item
        public void RandomFind(int rand)
        {
            if (rand < 3)
            {
                Console.WriteLine("You got a potion!\n");
                potions++;
            }
            else if (rand < 5)
            {
                Console.WriteLine("You got an ether!\n");
                ethers++;
            }
            else if (rand == 5)
            {
                Console.WriteLine("You got an elixir!\n");
                elixirs++;
            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

        //Finding money after a battle. Parameter is amount found
        public void GetMoney(int found)
        {
            money += found;
        }

        //Halves your money as payment when your Pokémon faints
        public int PokemonCenter()
        {
            money = Convert.ToInt32(money / 2);
            return money;
        }
    }
}
