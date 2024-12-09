// See https://aka.ms/new-console-template for more information
using System.Text;
using Game;
using System.Threading;
using System;
using System.IO;
using System.Net.NetworkInformation;

public static class File { };

class Program
{
    static void Main(string[] args)
    {
                
        Character player1 = new Character("HoWL", 100, 15, 4, 15, 20, 30, (Race) 1);
        player1.print();
        Console.WriteLine();

        Character player2 = new Character("Anton", 90, 18, 7, 10, 10, 20);
        player2.print();
        Console.WriteLine();
        Thread.Sleep(4000);
        while (true)
        {

            if (player2.attack(player1))
                break;
            
            Thread.Sleep(2000);
            Console.Clear();

            if (player1.attack(player2))
                break;

            Thread.Sleep(2000);
            Console.Clear();
        }  
        

    }
}






using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Game
{
    enum Race
    {
        Human,
        Ork,
        Elf,
        Dwarf
    }

    enum type_of_damage 
    {
        physical,
        magical,
        clear
    }
    enum spels
    {
        fire,
        wind,
        regeneration,
        sun_strike
    }
    enum spel_type 
    {
        damage,
        regen
    }


    class Character
    {
        string? name;
        double health;
        int damage;
        int defence;
        int evasion;
        int vampirism;
        int magresist;
        type_of_damage damageType; 
        Race race;
        Race goodAgainst;
        Race badAgainst;
        spels spel;
        
        public string? Name
        {
            get { return name; }
            set { this.name = value; }
        }
        public double Health
        {
            get { return health; }
            set { health = Math.Max(value, 0); }
        }
        public int Damage
        {
            get { return damage; }
            set { this.damage = value; }
        }
        public int Defence
        {
            get { return defence; }
            set { defence = Math.Max(value, 0); }
        }
        public int Evasion
        {
            get { return evasion; }
            set { evasion = Math.Max(value, 0); }
        }
        public int Vampirism
        {
            get { return vampirism; }
            set { vampirism = value;  }
        }
        public int Magresist
        {
            get { return magresist; }
            set { magresist = value; }
        }
        public Race Races{ 
            get { return race; }
            set { race = value; Againsts(); }
        }
        public Race GoodAgainst
        {
            get { return goodAgainst; }
        }
        public Race BadAgainst
        {
            get { return badAgainst; }
        }
        public type_of_damage DamageType
        {
            get { return damageType; }
            set { damageType = value; }
        }

        void Againsts()
        {
            if (this.race == Race.Human)
            {
                this.goodAgainst = Race.Elf;
                this.badAgainst = Race.Ork;
            }
            else if (this.race == Race.Ork)
            {
                this.goodAgainst = Race.Human;
                this.badAgainst = Race.Dwarf;
            }
            else if (this.race == Race.Elf)
            {
                this.goodAgainst = Race.Dwarf;
                this.badAgainst = Race.Ork;
            }
            else if (this.race == Race.Dwarf)
            {
                this.goodAgainst = Race.Human;
                this.badAgainst = Race.Elf;
            }

        }

        public Character(string? name, double health, int damage, int defence, int evasion, int vampirism, int magresist, Race race = Race.Human, type_of_damage damageType = type_of_damage.physical)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.defence = defence;
            this.evasion = evasion;
            this.vampirism = vampirism;
            this.race = race;
            this.damageType = damageType;
            this.magresist = magresist;
            Againsts();
        }

        public void print()
        {
            Console.WriteLine($"-< {name} >- ");
            Console.WriteLine($"Hp: {Convert.ToInt32(health)} ");
            Console.WriteLine($"Damage: {damage} ");
            Console.WriteLine($"Defence: {defence} ");
            Console.WriteLine($"Evasion: {evasion}");
            Console.WriteLine($"Vampirism: {vampirism}");
            Console.WriteLine($"Race: {race}");
        }

        public double take_damage(double damage, Character from)
        {
            Random? rand = new Random();
            double dmg = -10;
            if (from.damageType == type_of_damage.physical)
            {
                int a = rand.Next(0, 2), ev = rand.Next(1, (100 / evasion) + 1);
                if (a == 0)
                    damage -= (damage / 10);
                else
                    damage += (damage / 10);
                if (ev == 1)
                    return -1;
                dmg = ((damage - (damage / 100 * defence)));
                from.health += ((dmg / 100) * this.vampirism);

            }
            else if (from.damageType == type_of_damage.magical)
                dmg = ((damage - (damage / 100 * magresist)));
            else if (from.damageType == type_of_damage.clear)
                dmg = damage;
            this.health = Math.Max((this.health - dmg), 0);
            if (this.health == 0)
                return 0;
            return dmg;
        }
        public bool attack(Character target)
        {
            double to_dmg = Convert.ToDouble(this.Damage);

            if (target.Races == this.GoodAgainst && this.DamageType == type_of_damage.physical)
                to_dmg += this.Damage / 10;
            else if (target.Races == this.BadAgainst && this.DamageType == type_of_damage.physical)
                to_dmg -= this.Damage / 10;

            int dmg = Convert.ToInt32(target.take_damage(to_dmg, this));
            if (dmg == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(target.name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" has killed by {this.name}!");
                return true;
            }
            else if (dmg == -1)
            {
                Console.Write($"{target.name} evasioned from {this.name}!");
                return false;
            }
            Console.WriteLine($"{this.name} atacked {target.name} and caused {dmg} damage!");
            Console.WriteLine($"{target.name} has {Convert.ToInt32(target.health)} health!");
            return false;
        }

    }

    class spells
    {
        spel sun_strike = new spel("sun_strike", 20, type_of_damage.magical, spel_type.damage);
    }

    class spel
    {
        string? name;
        int damage;
        type_of_damage damageType;
        spel_type typeSpel;

        public spel(string? name, int damage, type_of_damage damageType, spel_type typeSpel) 
        {
            this.name = name;
            this.damage = damage;
            this.typeSpel = typeSpel;
            this.damageType = damageType;
        }
    }
    class Web_site
    {
        string Name;
        string Url;
        string Description;
        string IP;

        public string getName()
        {
            return Name;
        }
        public string getUrl()
        {
            return Url;
        }
        public string getDescription()
        {
            return Description;
        }
        public string getIP()
        {
            return IP;
        }
        public void setName(string Name)
        {
            this.Name = Name;
        }
        public void setUrl(string Url)
        {
            this.Url = Url;
        }
        public void setDescription(string Description)
        {
            this.Description = Description;
        }
        public void setIP(string IP)
        {
            this.IP = IP;
        }

        public Web_site(string name, string url, string description, string iP)
        {
            this.Name = name;
            this.Url = url;
            this.Description = description;
            this.IP = iP;
        }
        public void print()
        {
            Console.WriteLine($"-< {Name} >- ");
            Console.WriteLine($"Url: {Url} ");
            Console.WriteLine($"Description: {Description} ");
            Console.WriteLine($"IP: {IP} ");
        }
    }

    class Bank_account
    {
        string name;
        string seccond_name;
        int number;
        double Balance;
        double InterenceRate;

        public Bank_account(string name, string seccond_name, int number, double balance, double InterenceRate)
        {
            this.name = name;
            this.seccond_name = seccond_name;
            this.number = number;
            this.Balance = balance;
            this.InterenceRate = InterenceRate;

        }
        public Bank_account() : this("Walter", "White", 12434214, 10000.0, 5.5) { }
        public string getName() => this.name;
        public string getSecName() => this.seccond_name;
        public int getNumber() => this.number;
        public double getBalance() => this.Balance;
        public double getInterenceRate() => this.InterenceRate;

        public string setName(string name) => this.name = name;
        public string setSecName(string second_name) => this.seccond_name = second_name;
        public int setNumber(int numb) => this.number = numb;
        public double setBalance(double Balance) => this.Balance = Balance;
        public double setInterenceRate(double InterenceRate) => this.InterenceRate = InterenceRate;

        public void print()
        {
            Console.WriteLine($"{name} {seccond_name}");
            Console.WriteLine($"Balance: {Balance}");
            Console.WriteLine($"InterenceRate: {InterenceRate}");
        }

        public void Deposit(double count)
        {
            this.Balance += count;
        }
        public bool Withdraw(double count)
        {
            if (this.Balance >= count)
            {
                this.Balance -= count;
                return true;
            }
            return false;
        }
        public double CalculateYearlyInterest()
        {
            return (this.Balance / 100) * this.InterenceRate;
        }
        public bool perevod(Bank_account from, double count)
        {
            if (from.Balance >= count)
            {
                from.Withdraw(count);
                this.Deposit(count - Math.Min(count * 0.01, 50));
                return true;
            }
            else
                return false;
        }
    }
}
