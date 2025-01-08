using System.Text;
using Game;
using System.Threading;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

public static class File { };

class Program
{
    static void DrawBoard(Point chessman, Point[] posibleMoves)
    {
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                var currentPoint = new Point(j, i);
                if (chessman == currentPoint) Console.BackgroundColor = ConsoleColor.Red;
                else if (Array.IndexOf(posibleMoves, currentPoint) != -1)
                {
                    Console.BackgroundColor = ConsoleColor.Green;
                }
                else
                {
                    Console.BackgroundColor = Convert.ToBoolean((j + i) % 2) ? ConsoleColor.DarkGray : ConsoleColor.Gray;
                }
                Console.Write("  ");
                Console.ResetColor();
            }
            Console.WriteLine();
        }
        Console.ResetColor();
    }

    static void Main(string[] args)
    {
        /*
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
            Console.WriteLine();
            Thread.Sleep(000);

            if (player1.attack(player2))
                break;
            Console.WriteLine();
            Thread.Sleep(000);
        }

        */

        /*
        int a, b;
        Console.WriteLine("Points:");
        a = Convert.ToInt32(Console.ReadLine());
        b = Convert.ToInt32(Console.ReadLine());


        Ferzin ferzin = new Ferzin(a - 1, b - 1, team.white);
        int[][] n = ferzin.whereCanMove();
        Point[] points = new Point[n.Length];
        for (int i = 0; i < n.Length; i++)
        {
            points[i] = new Point(n[i][1], n[i][0]);
        }
        Point c = new Point(a - 1, b - 1);
        DrawBoard(c, points);
        */
        /*
        student std1 = new student("aboba", new double[] { 1, 8, 10, 12, 5});
        student std2 = new student("bbb", new double[] { 4, 5, 12, 5, 9});
        student std3 = new student("none", new double[] { 7, 3, 1, 9});
        student std4 = new student("Howl", new double[] { 11, 12, 12, 10, 12, 11, 9, 10, 12, 11});
        Group group1 = new Group("p35");
        group1.Students.Add(std1);
        group1.Students.Add(std2);
        group1.Students.Add(std3);
        group1.Students.Add(std4);
        Console.WriteLine(group1);
        Console.WriteLine(group1.CountTotalAvgGrade());
        */

        

            Pizza pizza1 = new Pizza("Peperoni", size.Small, 149);
            bool isBreak;
            Console.WriteLine("Your name");
            string name = Console.ReadLine();
            Console.WriteLine("Your Phone Number");
            string Phone = Console.ReadLine();
            Console.WriteLine("Your address");
            string address = Console.ReadLine();

            Order order = new Order(new Customer(name, Phone, address));
            do {
                Console.WriteLine("What pizza do you want to choose: ");
                Console.WriteLine(" Peperoni - 149/179/209\n Margarita - 79/109/139\n 4chesse - 119/149/179\n Marinara - 129/159/189");
                Console.Write("> ");
                int choose1 = Convert.ToInt32(Console.ReadLine());
                Console.WriteLine("Small, Medium or Large?");
                Console.Write("> ");
                int choose2 = Convert.ToInt32(Console.ReadLine());
                if (choose1 == 1)
                {
                    if (choose2 == 1) {
                        pizza1 = new Pizza("Peperoni", size.Small, 149);
                    }
                    else if (choose2 == 2) {
                        pizza1 = new Pizza("Peperoni", size.Medium, 179);
                    }
                    else if (choose2 == 3) {
                        pizza1 = new Pizza("Peperoni", size.Large, 209);
                    }
                }
                else if (choose1 == 2)
                {
                    if (choose2 == 1)
                    {
                        pizza1 = new Pizza("Margarita", size.Small, 79);
                    }
                    else if (choose2 == 2)
                    {
                        pizza1 = new Pizza("Margarita", size.Medium, 109);
                    }
                    else if (choose2 == 3)
                    {
                        pizza1 = new Pizza("Margarita", size.Large, 139);
                    }
                }
                else if (choose1 == 3)
                {
                    if (choose2 == 1)
                    {
                        pizza1 = new Pizza("4chesse", size.Small, 119);
                    }
                    else if (choose2 == 2)
                    {
                        pizza1 = new Pizza("4chesse", size.Medium, 149);
                    }
                    else if (choose2 == 3)
                    {
                        pizza1 = new Pizza("4chesse", size.Large, 179);
                    }
                }
                else if (choose1 == 3)
                {
                    if (choose2 == 1)
                    {
                        pizza1 = new Pizza("Marinara", size.Small, 129);
                    }
                    else if (choose2 == 2)
                    {
                         pizza1 = new Pizza("Marinara", size.Medium, 159);
                    }
                    else if (choose2 == 3)
                    {
                        pizza1 = new Pizza("Marinara", size.Large, 189);
                    }
                }
                order.addPizza(pizza1);

                

                Console.WriteLine("do you want to add another pizzas? 1. yes, 2. no");
                int choose3 = Convert.ToInt32(Console.ReadLine());

                if (choose3 == 1)
                    isBreak = true;
                else
                    isBreak = false;
            } while (isBreak);
            Console.WriteLine(order.ToString());
            

        
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
            if (Spel.TypeSpel == spel_type.regen)
            {
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
    class Fireball : Spell
    {
        string? name = "Fire ball";
        double damage = 13;
        public override void cast(Character target)
        {
            //target.takeSpellDamage(this.damage);
        }
    }


    enum team
    {
        white,
        black
    }
    abstract class Chessman
    {
        protected int x, y;
        protected team team;

        public team Team { get { return team; } }

        public int[][] whereCanMove()
        {
            int[][] a = new int[64][];
            int b = 0;
            for (int i = 0; i < 64; ++i)
                a[i] = new int[2];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (this.isCanMove(i, j))
                    {
                        a[b][0] = i;
                        a[b][1] = j;
                        b++;
                        Console.WriteLine($"{b}, {i}, {j}");
                    }
                }
            }
            int[][] c = new int[b][];
            for (int i = 0; i < b; ++i)
                c[i] = new int[2];
            for (int i = 0; i < b; i++)
            {
                c[i][0] = a[i][0];
                c[i][1] = a[i][1];
            }
            return c;
        }

        public abstract bool isCanMove(int y, int x);


    }
    class Pawn : Chessman
    {

        public override bool isCanMove(int y, int x)
        {
            if (this.x == x)
                if (this.y == y - 1 || this.y == y - 2) return true;

            return false;
        }
        public Pawn(int x, int y, team team)
        {
            this.x = x;
            this.y = y;
            this.team = team;
        }
    }

    class Turris : Chessman
    {

        public override bool isCanMove(int y, int x)
        {
            if (y == this.y || x == this.x) return true;
            else
                return false;
        }
        public Turris(int x, int y, team team)
        {
            this.x = x;
            this.y = y;
            this.team = team;
        }
    }

    class Horse : Chessman
    {
        public override bool isCanMove(int y, int x)
        {
            if (this.y + 2 == y && this.x + 1 == x) return true;
            else if (this.y + 2 == y && this.x - 1 == x) return true;
            else if (this.y - 2 == y && this.x + 1 == x) return true;
            else if (this.y - 2 == y && this.x - 1 == x) return true;
            else if (this.y + 1 == y && this.x - 2 == x) return true;
            else if (this.y + 1 == y && this.x + 2 == x) return true;
            else if (this.y - 1 == y && this.x - 2 == x) return true;
            else if (this.y - 1 == y && this.x + 2 == x) return true;
            else
                return false;
        }
        public Horse(int x, int y, team team)
        {
            this.x = x;
            this.y = y;
            this.team = team;
        }
    }

    class Elephant : Chessman
    {
        public override bool isCanMove(int y, int x)
        {
            if (x - this.x == y - this.y)
                return true;


            return false;
        }
        public Elephant(int x, int y, team team)
        {
            this.x = x;
            this.y = y;
            this.team = team;
        }
    }

    class Ferzin : Chessman
    {
        public override bool isCanMove(int y, int x)
        {

            if (this.x == x + 1 || this.x == x - 1 || this.x == x)
                if (this.y == y + 1 || this.y == y - 1 || this.y == y)
                    return true;
            if (y == this.y || x == this.x) return true;

            if (x - this.x == y - this.y)
                return true;


            return false;
        }
        public Ferzin(int x, int y, team team)
        {
            this.x = x;
            this.y = y;
            this.team = team;
        }
    }

    class King : Chessman
    {
        public override bool isCanMove(int y, int x)
        {

            if (this.x == x + 1 || this.x == x - 1 || this.x == x)
                if (this.y == y + 1 || this.y == y - 1 || this.y == y)
                    return true;

            return false;
        }
        public King(int x, int y, team team)
        {
            this.x = x;
            this.y = y;
            this.team = team;
        }
    }

    class student
    {
        string? name;
        List<double>? grades;
        double avg;

        public string? Name { get { return name; } set { this.name = value; } }
        public List<double> Grades { get { return grades; } }
        
        public double CountAvgGrade()
        {
            if (this.grades == null)
                return this.avg = 0;
            return this.avg = grades.Sum() / grades.Count();
        }

        public student() : this("Vasya", new double[] { }) { }
        public student(string? name, double[] grades)
        {
            this.name = name;
            this.grades = new List<double>(grades);
            this.CountAvgGrade();
        }

        public void print()
        {
            Console.WriteLine($"\t\b\b< {name} >");
            Console.WriteLine($"avg: {avg}");
        }
        public override string ToString()
        {
            return $"{this.name} ({this.avg}) - {String.Join(", ", this.grades)}";
        }

    }

    class Group
    {
        string? name;
        List<student> students;

        public Group() : this("Group") { }
        public Group(string? name)
        {
            this.name = name;
            this.students = new List<student>();
        }
        public string? Name { get { return name; } set { this.name = value; } }
        public List<student> Students { get { return students; } }
        public override string ToString()
        {
            return $"{this.name}: \n  {String.Join("\n  ", this.students)}";
        }
        public double CountTotalAvgGrade()
        {
            double sum = 0;
            foreach (student student in this.students) { sum += student.CountAvgGrade(); }
            return sum / this.students.Count;
        }
    }

    enum size
    {
        Small,
        Medium,
        Large
    }
        
        
    class Pizza
    {
        string? name;
        size pizzaSize;
        double price;

        public double Price { get { return price; } }
        public string? Name { get { return name; } }
        public size PizzaSize { get { return pizzaSize; } }
        public Pizza(string? name, size pizza_size, double price)
        {
            this.name = name;
            this.price = price;
            this.pizzaSize = pizza_size;
        }
        public override string ToString()
        {
            return $"{this.name} - {this.pizzaSize}, {this.price}grn";
        }
    }
    class Customer
    {
        string? name;
        string? phoneNumber;
        string? address;
        public string? Name { get { return name; } }
        public string? PhoneNumber { get {  return phoneNumber; } }
        public string? Address { get { return address; } }

        public Customer(string? name, string? phoneNumber, string? address) {
            this.name=name;
            this.phoneNumber=phoneNumber;
            this.address=address;
        }

        public override string ToString()
        {
            return $"{this.name} - {this.phoneNumber}, {this.address}";
        }

    }
    class Order
    {
        Customer customer;
        List<Pizza> pizzas;
        double totalPrice;


        public Order(Customer customer)
        {
            this.customer = customer;
            this.pizzas = new List<Pizza>();
        }

        public void addPizza(Pizza pizza) {
            this.pizzas.Add(pizza);
            CalculateTotalPrice();
        }
        public void CalculateTotalPrice()
        {
            double sum = 0;
            for (int i = 0; i < pizzas.Count; i++)
            {
                sum += pizzas[i].Price;
            }
            this.totalPrice = sum;
        }
        public override string ToString()
        {
            return $"Order: \n {customer}\n Pizzas: \n  {String.Join("\n  ", this.pizzas)}, \n {totalPrice}grn";
        }
    }


}
