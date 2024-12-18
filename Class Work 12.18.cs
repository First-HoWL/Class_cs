using System.Text;
using Game;
using System.Threading;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Diagnostics;


public static class File { };

class Program
{
    class point
    {
        public int x;
        public int y;
        public point(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public point() : this(0, 0) { }

        public static point operator +(point a, point b)
        {
            return new point(a.x + b.x, a.y + b.y);
        }
        public static point operator +(point a, int v)
        {
            return new point(a.x + v, a.y + v);
        }
        public static point operator +(point a, double v)
        {
            return a + (int)v;
        }
        public static bool operator ==(point a, point b)
        {
            return (a.x == b.x) && (a.y == b.y);
        }
        public static bool operator !=(point a, point b)
        {
            return !(a == b);
        }
        public static bool operator true(point a)
        {
            return (a.x != 0) || (a.y != 0);
        }
        public static bool operator false(point a)
        {
            return (a.x == 0) && (a.y == 0);
        }
    }

    class Fraction
    {
        public int chisel;
        public int znamen;

        public Fraction(int chisel, int znamen)
        {
            this.chisel = chisel;
            this.znamen = znamen;
        }
        public void to_corect()
        {
            bool is_changet = true;
            while (is_changet)
            {
                is_changet = false;
                if (chisel % 10 == 0 && znamen % 10 == 0)
                {
                    chisel = chisel / 10;
                    znamen = znamen / 10;
                    is_changet = true;
                }
                if (chisel % 7 == 0 && znamen % 7 == 0)
                {
                    chisel = chisel / 7;
                    znamen = znamen / 7;
                    is_changet = true;
                }
                if (chisel % 5 == 0 && znamen % 5 == 0)
                {
                    chisel = chisel / 5;
                    znamen = znamen / 5;
                    is_changet = true;
                }
                if (chisel % 3 == 0 && znamen % 3 == 0)
                {
                    chisel = chisel / 3;
                    znamen = znamen / 3;
                    is_changet = true;
                }
                if (chisel % 2 == 0 && znamen % 2 == 0)
                {
                    chisel = chisel / 2;
                    znamen = znamen / 2;
                    is_changet = true;
                }
            }
        }

        public static Fraction operator -(Fraction a, Fraction b)
        {
            return new Fraction((a.chisel * b.znamen - b.chisel * a.znamen), (b.znamen * a.znamen));
        }
        public static Fraction operator +(Fraction a, Fraction b)
        {
            return new Fraction((a.chisel * b.znamen + b.chisel * a.znamen), (b.znamen * a.znamen));
        }

        public static Fraction operator *(Fraction a, Fraction b)
        {
            return new Fraction(a.chisel * b.chisel, a.znamen * b.znamen);
        }
        public static Fraction operator /(Fraction a, Fraction b)
        {
            return new Fraction(a.chisel * b.znamen, a.znamen * b.chisel);
        }

        public static bool operator ==(Fraction a, Fraction b)
        {
            return (a.chisel == b.chisel) && (a.znamen == b.znamen);
        }
        public static bool operator !=(Fraction a, Fraction b)
        {
            return !(a == b);
        }

        public static bool operator >(Fraction a, Fraction b)
        {
            return (a.chisel / a.znamen) > (b.chisel / b.znamen);
        }

        public static bool operator <(Fraction a, Fraction b)
        {
            return (a.chisel / a.znamen) < (b.chisel / b.znamen);
        }


        public static bool operator true(Fraction a)
        {
            return a.chisel < a.znamen;
        }
        public static bool operator false(Fraction a)
        {
            return a.chisel > a.znamen;
        }
        public void int_fraction(int b)
        {
            this.chisel += b * this.znamen;
        }

    }


    static void Main(string[] args)
    {
        /*
        Shape[] shapes = { new Square(4), new Rectangle(3, 10), new Circle(10), new Triangle(10, 6, 8), new Trapezoid(6, 8, 10) };

        foreach (Shape shape in shapes)
        {
            Console.WriteLine($"Area of {shape} is {shape.GetArea()}");
        }
        */
        /*
        Fraction a = new Fraction(4, 14);
        Fraction b = new Fraction(10, 14);
        a.int_fraction(3);
        Fraction c = a + b;
        c.to_corect();
        Console.WriteLine($"{c.chisel} / {c.znamen}");
        if (a > b) Console.WriteLine("a > b");
        else if (a < b) Console.WriteLine("a < b");
        else Console.WriteLine("a = b");
        */
        /*
        point point1 = new point(1, 3);
        Console.WriteLine($"point1 = ({point1.x}, {point1.y})");

        point point2 = new point(1, 3);
        Console.WriteLine($"point2 = ({point2.x}, {point2.y})");

        point point3 = point1 + point2;
        Console.WriteLine( $"point3 = ({point3.x}, {point3.y})");
        point point4 = point1 + 3;
        Console.WriteLine($"point4 = ({point4.x}, {point4.y})");

        if (point1 == point2) Console.WriteLine("True");
        else Console.WriteLine("False");
        */
        
        Berserk player1 = new Berserk("HoWL", 40, 15, 4, 15, 20, 30, (Race)1);
        player1.print();
        Console.WriteLine();

        Magical player2 = new Magical("Anton", 120, 15, 7, 10, 10, 20);
        player2.print();
        Console.WriteLine();
        Thread.Sleep(000);
        while (true)
        {

            if (player2.attack(player1))
                break;

            Thread.Sleep(000);

            if (player1.attack(player2))
                break;

            Thread.Sleep(000);
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
        protected string? name;
        protected double health;
        protected int damage;
        protected int defence;
        protected int evasion;
        protected int vampirism;
        protected int magresist;
        protected type_of_damage damageType;
        protected Race race;
        protected Race goodAgainst;
        protected Race badAgainst;

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
            set { vampirism = value; }
        }
        public int Magresist
        {
            get { return magresist; }
            set { magresist = value; }
        }
        public Race Races
        {
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
            Console.WriteLine($"Hp: {Math.Round(this.health, 2)} ");
            Console.WriteLine($"Damage: {damage} ");
            Console.WriteLine($"Defence: {defence} ");
            Console.WriteLine($"Evasion: {evasion}");
            Console.WriteLine($"Vampirism: {vampirism}");
            Console.WriteLine($"Race: {race}");
        }

        public virtual double take_damage(double damage, Character from)
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
        public virtual double takeSpelDamage(double damage, spel from)
        {
            double dmg;
            if (from.DamageType == type_of_damage.magical)
                dmg = ((damage - (damage / 100 * this.magresist)));
            else
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
                Console.WriteLine($"{target.name} evasioned from {this.name}!\n");
                return false;
            }
            Console.WriteLine($"{this.name} atacked {target.name} and caused {dmg} damage!");
            Console.WriteLine($"{target.name} has {Convert.ToInt32(target.health)} health!");
            return false;
        }

    }

    class Berserk : Character
    {
        protected bool lastChance = true;

        public bool LastChance
        {
            get { return lastChance; }
            set { this.lastChance = value; }
        }

        public Berserk(string? name, double health, int damage, int defence, int evasion, int vampirism, int magresist, Race race = Race.Human, type_of_damage damageType = type_of_damage.physical)
            : base(name, health, damage, defence, evasion, vampirism, magresist, race, damageType) { }
        public Berserk() : this("none", 100, 12, 5, 0, 0, 20) { }

        public override double take_damage(double damage, Character from)
        {
            Random? rand = new Random();
            double dmg = -10;
            if (from.DamageType == type_of_damage.physical)
            {
                int a = rand.Next(0, 2), ev = rand.Next(1, (100 / evasion) + 1);
                if (a == 0)
                    damage -= (damage / 10);
                else
                    damage += (damage / 10);
                if (ev == 1)
                    return -1;
                dmg = ((damage - (damage / 100 * defence)));
                from.Health += ((dmg / 100) * this.vampirism);

            }
            else if (from.DamageType == type_of_damage.magical)
                dmg = ((damage - (damage / 100 * magresist)));
            else if (from.DamageType == type_of_damage.clear)
                dmg = damage;
            
            this.Health = Math.Max((this.Health - dmg), 0);

            if (this.Health == 0 && this.LastChance == true)
            {
                this.Health = 1;
                this.LastChance = false;
                Console.WriteLine($"{this.Name} used his last chance!");
                return dmg;
            }

            else if (this.Health == 0)
                return 0;
            return dmg;
        }
        public override double takeSpelDamage(double damage, spel from)
        {
            double dmg;
            if (from.DamageType == type_of_damage.magical)
                dmg = ((damage - (damage / 100 * this.magresist)));
            else
                dmg = damage;

            this.health = Math.Max((this.health - dmg), 0);

            if (this.health == 0 && this.LastChance == true)
            {
                this.health = 1;
                this.LastChance = false;
                Console.WriteLine($"{this.Name} used his last chance!");
                return dmg;
            }

            else if (this.health == 0)
                return 0;
            return dmg;
        }

        public new bool attack(Character target)
        {
            double to_dmg = Convert.ToDouble(this.Damage);

            if (target.Races == this.GoodAgainst && this.DamageType == type_of_damage.physical)
                to_dmg += this.Damage / 10;
            else if (target.Races == this.BadAgainst && this.DamageType == type_of_damage.physical)
                to_dmg -= this.Damage / 10;

            if (this.health < 50)
                to_dmg *= 1.5;

            int dmg = Convert.ToInt32(target.take_damage(to_dmg, this));
            
            if (dmg == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(target.Name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" has killed by {this.name}!");
                return true;
            }
            else if (dmg == -1)
            {
                Console.WriteLine($"{target.Name} evasioned from {this.name}!");
                return false;
            }
            Console.WriteLine($"{this.name} atacked {target.Name} and caused {dmg} damage!");
            Console.WriteLine($"{target.Name} has {Math.Round(target.Health, 2)} health!");
            return false;
        }


    }

    class Magical : Character
    {
        protected Spell[] spellList = { new Fireball() };
        spel sun_strike = new spel("sun_strike", 10, type_of_damage.clear, spel_type.damage);
        spel fire = new spel("fire", 20, type_of_damage.magical, spel_type.damage);
        spel wind = new spel("wind", 12, type_of_damage.magical, spel_type.damage);
        spel regeneration = new spel("regeneration", 15, type_of_damage.magical, spel_type.regen);
        public Magical(string? name, double health, int damage, int defence, int evasion, int vampirism, int magresist, Race race = Race.Human, type_of_damage damageType = type_of_damage.physical)
                : base(name, health, damage, defence, evasion, vampirism, magresist, race, damageType) { }
        public Magical() : this("none", 100, 12, 5, 0, 0, 20) { }

        public double CastSpell(Character target, spel Spel)
        {
            double dmg;
            if (Spel.TypeSpel == spel_type.regen) { 
                this.health += Spel.Damage;
                return -1;
                }
            dmg = target.takeSpelDamage(Spel.Damage, Spel);
            return dmg;
        }

        public new bool attack(Character target)
        {
            int dmg;
            Random? rand = new Random(Convert.ToInt32(DateTimeOffset.Now.ToUnixTimeSeconds()));
            if (rand.Next(1, 3) == 1)
            {
                int a = rand.Next(1, 5);
                spel Spel = sun_strike;
                if (a == 1) Spel = sun_strike;
                else if (a == 2) Spel = fire;
                else if (a == 3) Spel = wind;
                else if (a == 4) Spel = regeneration;
                dmg = Convert.ToInt32(CastSpell(target, Spel));

                if (dmg == 0)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.Write(target.Name);
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write($" has killed by {Spel.Name} player {this.name}!");
                    return true;
                }
                if (dmg == -1)
                {

                    Console.WriteLine($"{this.name} healling himselve by {Spel.Damage}hp!");
                    Console.WriteLine($"{this.name} have {Math.Round(this.Health, 2)}hp!");
                    Console.WriteLine($"{target.Name} has {Math.Round(target.Health, 2)}hp!");
                    return false;
                }

                Console.WriteLine($"{this.name} atacked {target.Name} by {Spel.Name} and caused {dmg} damage!");
                Console.WriteLine($"{target.Name} has {Convert.ToInt32(target.Health)} health!");
                return false;
            }

            double to_dmg = Convert.ToDouble(this.Damage);

            if (target.Races == this.GoodAgainst && this.DamageType == type_of_damage.physical)
                to_dmg += this.Damage / 10;
            else if (target.Races == this.BadAgainst && this.DamageType == type_of_damage.physical)
                to_dmg -= this.Damage / 10;


            dmg = Convert.ToInt32(target.take_damage(to_dmg, this));
            
            if (dmg == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(target.Name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" has killed by {this.name}!");
                return true;
            }
            else if (dmg == -1)
            {
                Console.WriteLine($"{target.Name} evasioned from {this.name}!");
                return false;
            }
            Console.WriteLine($"{this.name} atacked {target.Name} and caused {dmg} damage!");
            Console.WriteLine($"{target.Name} has {Math.Round(target.Health, 2)} health!");
            return false;
        }
    }



    class spells
    {
        spel sun_strike = new spel("sun_strike", 10, type_of_damage.clear, spel_type.damage);
        spel fire = new spel("fire", 20, type_of_damage.magical, spel_type.damage);
        spel wind = new spel("wind", 12, type_of_damage.magical, spel_type.damage);
        spel regeneration = new spel("regeneration", 15, type_of_damage.magical, spel_type.regen);
    }

    class spel
    {
        string? name;
        int damage;
        type_of_damage damageType;
        spel_type typeSpel;

        public int Damage
        {
            get { return damage; }
            set { damage = value; }
        }
        public string? Name
        {
            get { return name; }
            set { name = value; }
        }
        public type_of_damage DamageType
        {
            get { return damageType; }
            set { damageType = value; }
        }
        public spel_type TypeSpel
        {
            get { return typeSpel; }
            set { typeSpel = value; }
        }
        public spel(string? name, int damage, type_of_damage damageType, spel_type typeSpel)
        {
            this.name = name;
            this.damage = damage;
            this.typeSpel = typeSpel;
            this.damageType = damageType;
        }
    }

    abstract class Spell
    {
        string? name;
        string? Name
        {
            get { return this.name; }
        }
        public abstract void cast(Character target);
    }
    class Fireball : Spell {
        string? name = "Fire ball";
        double damage = 13;
        public override void cast(Character target)
        {
            //target.takeSpellDamage(this.damage);
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

    abstract class Shape
    {
        public abstract double GetArea();
    }

    class Square : Shape
    {
        double side;
        public Square(double side)
        {
            this.side = side;
        }
        public override double GetArea()
        {
            return Math.Pow(side, 2);
        }
        public override string ToString()
        {
            return $"Square({this.side})";
        }
    }

    class Rectangle : Shape
    {
        double a, b;
        public Rectangle(double a, double b)
        {
            this.a = a;
            this.b = b;
        }
        public override double GetArea()
        {
            return a * b;
        }
        public override string ToString()
        {
            return $"Rectegle({this.a}, {this.b})";
        }
    }

    class Circle : Shape
    {
        double r;
        public Circle(double r)
        {
            this.r = r;
        }
        public override double GetArea()
        {
            return (Math.PI * Math.Pow(r, 2));
        }
        public override string ToString()
        {
            return $"Circle({this.r})";
        }
    }

    class Triangle : Shape
    {
        double a, b, c;
        public Triangle(double a, double b, double c)
        {
            this.a = a;
            this.b = b;
            this.c = c;
        }
        public override double GetArea()
        {
            double x = 0;
            if (a > b && a > c)
                x = 0.5 * c * b;
            else if (b > a && b > c)
                x = 0.5 * c * a;
            else if (c > b && c > a)
                x = 0.5 * a * b;
            return x;
        }
        public override string ToString()
        {
            return $"Triangle({this.a}, {this.b}, {this.c})";
        }
    }

    class Trapezoid : Shape
    {
        double a, b, h;
        public Trapezoid(double a, double b, double h)
        {
            this.a = a;
            this.b = b;
            this.h = h;
        }
        public override double GetArea()
        {
            return (0.5 * (a + b) * h);
        }
        public override string ToString()
        {
            return $"Trapezoid({this.a}, {this.b}, {this.h})";
        }
    }

}
