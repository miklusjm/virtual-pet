using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualPet
{
    class VirtualPet
    {
        //Your Pokémon's stats
        private string name;
        private int hp;
        private int hpMax;
        private int attack;
        private int happiness;
        private string basic;
        private string special;
        private int ap;
        private int apMax;

        //Constructor. Determines Pokémon's type and starting stats
        //Parameter is passed from menu where player chooses their Pokémon
        public VirtualPet(string typeCode)
        {
            if (typeCode == "1")
            {
                name = "Rowlet";
                hp = 50;
                hpMax = 50;
                attack = 10;
                happiness = 10;
                basic = "Peck";
                special = "Leafage";
                ap = 25;
                apMax = 25;

            }
            else if (typeCode == "2")
            {
                name = "Litten";
                hp = 40;
                hpMax = 40;
                attack = 12;
                happiness = 10;
                basic = "Scratch";
                special = "Ember";
                ap = 25;
                apMax = 25;
            }
            else
            {
                name = "Popplio";
                hp = 60;
                hpMax = 60;
                attack = 8;
                happiness = 10;
                basic = "Pound";
                special = "Water Gun";
                ap = 25;
                apMax = 25;
            }
        }

        //Returns the name of your Pokémon's main attack
        public string Attack1Name()
        {
            return basic;
        }

        //Returns the name of your Pokémon's special attack in its own color
        public string Attack2Name()
        {
            return special;
        }

        //Your Pokémon's main attack
        public int Attack1(string type)
        {
            Random rand = new Random();
            int damage = (attack + rand.Next(0, Convert.ToInt32(0.5 * happiness)));
            WriteName();
            Console.Write(" used " + basic + "!\n\n");
            Console.WriteLine("Dealt " + damage + " damage to " + type + "!");
            return damage;
        }

        //Your Pokémon's special attack
        public int Attack2(string type)
        {
            Random rand = new Random();
            bool superEffective = false;
            int damage = Convert.ToInt32(1.5 * attack + rand.Next(0, happiness));
            if (type == "Rockruff" && (special == "Leafage" || special == "Water Gun"))
            {
                damage = Convert.ToInt32(1.5 * damage);
                superEffective = true;
            }
            if (type == "Grubbin" && special == "Ember")
            {
                damage = Convert.ToInt32(1.5 * damage);
                superEffective = true;
            }

            WriteName();
            Console.Write(" used ");
            Color();
            Console.Write(Attack2Name());
            Console.ResetColor();
            Console.Write("!\n\n");

            if (superEffective == true)
            {
                Console.WriteLine("It's super effective!\n");
            }

            Console.WriteLine("Dealt " + damage + " damage to " + type + "!");
            ap--;
            return damage;
        }

        //Returns Pokémon's current HP
        public int HP()
        {
            return hp;
        }

        //Returns Pokemon's maximum HP
        public int HPMax()
        {
            return hpMax;
        }

        //Returns Pokémon's current AP
        public int AP()
        {
            return ap;
        }

        //Returns Pokémon's maximum AP
        public int APMax()
        {
            return apMax;
        }

        //Returns Pokémon's Happiness stat
        public int Happiness()
        {
            return happiness;
        }

        //Returns Pokémon's Attack stat
        public int Attack()
        {
            return attack;
        }

        //Run when your Pokémon takes damage
        //Parameter is the enemy name (for display) and the amount of damage
        public void Damage(string enemy, int amount)
        {
            hp -= amount;
            Console.Write(enemy + " hit ");
            WriteName();
            Console.Write(" for " + amount + " damage!\n");
        }

        //Stat gains after winning a battle
        //Parameter is max HP of the defeated enemy -- tougher enemy, more gains
        public void LevelUp(int enemyHP)
        {
            Random randStats = new Random();
            int happinessUp = randStats.Next(1, 5);
            int attackUp = Convert.ToInt32(0.03 * (enemyHP + randStats.Next(0, 2 * happiness)));
            int hpUp = Convert.ToInt32(0.05 * (enemyHP + randStats.Next(0, 3 * happiness)));
            int apUp = randStats.Next(0, 1);

            happiness += happinessUp;
            attack += attackUp;
            hpMax += hpUp;
            hp += hpUp;
            apMax += apUp;
            ap += apUp;

            WriteName();
            Console.Write(" got stronger!\n");
            WriteName();
            Console.Write("'s Happiness increased by " + happinessUp + "!\n");
            WriteName();
            Console.Write("'s Attack increased by " + attackUp + "!\n");
            WriteName();
            Console.Write("'s HP increased by " + hpUp + "!\n");
            if (apUp > 0)
            {
                WriteName();
                Console.Write("'s AP increased by " + apUp + "!\n");
            }
        }

        //HP restoration from potions and elixirs
        //Parameter is the amount healed
        public void Heal(int amount)
        {
            hp += amount;
            if (hp > hpMax)
            {
                hp = hpMax;
                WriteName();
                Console.Write(" recovered to full health!\n");
            }
            else
            {
                WriteName();
                Console.Write(" recovered " + amount + " HP.\n");
            }
        }

        //AP restoration from ethers and elixirs
        //Parameter is the amount restored
        public void Recover(int amount)
        {
            ap += amount;
            if (ap > apMax)
            {
                ap = apMax;
                WriteName();
                Console.Write(" recovered to full AP!\n");
            }
            else
            {
                WriteName();
                Console.Write(" recovered " + amount + " AP.\n"); ;
            }
        }

        //Negatively adjusts the Pokémon's happiness upon running from a battle
        public void Run()
        {
            Random run = new Random();
            happiness -= run.Next(1, 5);
        }

        //Run when the Pokémon faints. Halves happiness, then restores health
        //(Health restoration comes at cost of half money in Inventory.PokemonCenter())
        public void Faint()
        {
            happiness = Convert.ToInt32(happiness / 2);
            hp = hpMax;
            ap = apMax;
        }

        //Writes the Pokémon's name in their color, then resets color
        public void WriteName()
        {
            if (name == "Rowlet")
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write("Rowlet");
                Console.ResetColor();
            }
            else if (name == "Litten")
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("Litten");
                Console.ResetColor();
            }
            else if (name == "Popplio")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.Write("Popplio");
                Console.ResetColor();
            }
        }

        //Displays the Pokémon's stats
        public void Stats()
        {
            WriteName();
            Console.Write("'s Stats\n");
            Console.WriteLine("HP:        " + hp + " / " + hpMax);
            Console.WriteLine("AP:        " + ap + " / " + apMax);
            Console.WriteLine("Attack:    " + attack);
            Console.WriteLine("Happiness: " + happiness);
        }

        //Sets the console color to match the Pokémon type
        public void Color()
        {
            if (name == "Rowlet")
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            else if (name == "Litten")
            {
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else if (name == "Popplio")
            {
                Console.ForegroundColor = ConsoleColor.Blue;
            }
        }
    }
}
