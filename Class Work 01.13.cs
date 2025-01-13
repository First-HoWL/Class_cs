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
    
    static void Main(string[] args)
    { 
        
        Berserk player1 = new Berserk("HoWL", 40, 15, 4, 15, 20, 30, (Race)1);
        player1.print();
        Console.WriteLine();
        List<string> a = new List<string>();
        a.Add("Bow");
        a.Add("Roten meat");
        Mob player2 = new Mob("Anton", 15, 150, 7, 10, 30, a);
       
        Console.WriteLine();
        Thread.Sleep(000);
        while (true)
        {
            Console.WriteLine();
            Thread.Sleep(000);

            if (player1.attack(player2))
                break;
            Console.WriteLine();
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
    enum spel_type
    {
        damage,
        regen
    }
    interface IDamageble {
        public double Health { get; set; }

        double take_damage(double damage, Character from);
        double takeSpelDamage(double damage, spel from, Character From);

    }
    interface ISpellCaster
    {
        double CastSpell(IDamageble target, spel Spel, Character from);
    }
    interface ILootable
    {
        List<string> Loot { get; set; }
    }
    class Mob : ILootable, IDamageble {
        protected string? name;
        protected int defence;
        protected double health;
        protected int evasion;
        protected int magresist;
        List<string> loot;

        public List<string> Loot { get { return loot; } set { loot = value; } }
        public double Health { get; set; }

        public virtual double take_damage(double damage, Character from)
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
                    dmg = -1;
                if (dmg != -1)
                {
                    dmg = ((damage - (damage / 100 * defence)));
                    from.Health += ((dmg / 100) * from.Vampirism);
                }
            }
            else if (from.DamageType == type_of_damage.magical)
                dmg = ((damage - (damage / 100 * magresist)));
            else if (from.DamageType == type_of_damage.clear)
                dmg = damage;
            this.health = Math.Max((this.health - dmg), 0);
            if (this.health == 0)
                dmg = 0;

            if (dmg == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(this.name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" has killed by {from.Name}!");
                Console.WriteLine($"\n{from.Name} loot {String.Join(", ", loot)}");
                return 0;
            }
            else if (dmg == -1)
            {
                Console.WriteLine($"{this.name} evasioned from {from.Name}!\n");
                return -1;
            }
            Console.WriteLine($"{from.Name} atacked {this.name} and caused {dmg} damage!");
            Console.WriteLine($"{this.name} has {Convert.ToInt32(this.health)} health!");
            return dmg;

        }

        public virtual double takeSpelDamage(double damage, spel from, Character From)
        {
            double dmg;
            if (from.DamageType == type_of_damage.magical)
                dmg = ((damage - (damage / 100 * this.magresist)));
            else
                dmg = damage;

            this.health = Math.Max((this.health - dmg), 0);
            if (this.health == 0)
                dmg = 0;

            if (dmg == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(From.Name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" has killed by {from.Name} player {this.name}!");
                return 0;
            }


            Console.WriteLine($"{From.Name} atacked {this.name} by {from.Name} and caused {dmg} damage!");
            Console.WriteLine($"{this.name} has {Convert.ToInt32(this.Health)} health!");
            return dmg;
        }
        public Mob(string? name, int defence, double health, int evasion,int vampirism, int magresist, List<string> loot) {
            this.name = name;
            this.defence = defence;
            this.health = health;
            this.evasion = evasion;
            this.magresist = magresist;
            this.loot = new List<string>();
            this.loot = loot;
        }

    }
    class Character : IDamageble 
    {
        protected string? name;
        protected int damage;
        protected int defence;
        protected double health;
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
                if (this.Races == from.GoodAgainst)
                    damage += damage / 10;
                else if (this.Races == from.BadAgainst)
                    damage -= damage / 10;

                int a = rand.Next(0, 2), ev = rand.Next(1, (100 / evasion) + 1);
                if (a == 0)
                    damage -= (damage / 10);
                else
                    damage += (damage / 10);
                if (ev == 1)
                    dmg = -1;
                if (dmg != -1) { 
                    dmg = ((damage - (damage / 100 * defence)));
                    from.health += ((dmg / 100) * this.vampirism);
                }
            }
            else if (from.damageType == type_of_damage.magical)
                dmg = ((damage - (damage / 100 * magresist)));
            else if (from.damageType == type_of_damage.clear)
                dmg = damage;
            this.health = Math.Max((this.health - dmg), 0);
            if (this.health == 0)
                dmg = 0;

            if (dmg == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(this.name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" has killed by {this.name}!");
                return 0;
            }
            else if (dmg == -1)
            {
                Console.WriteLine($"{this.name} evasioned from {from.name}!\n");
                return -1;
            }
            Console.WriteLine($"{from.name} atacked {this.name} and caused {dmg} damage!");
            Console.WriteLine($"{this.name} has {Convert.ToInt32(this.health)} health!");
            return dmg;

        }
        public virtual double takeSpelDamage(double damage, spel from, Character From)
        {
            double dmg;
            if (from.DamageType == type_of_damage.magical)
                dmg = ((damage - (damage / 100 * this.magresist)));
            else
                dmg = damage;

            this.health = Math.Max((this.health - dmg), 0);
            if(this.health == 0)
                dmg = 0;

            if (dmg == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(From.Name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" has killed by {from.Name} player {this.name}!");
                return 0;
            }
            

            Console.WriteLine($"{From.name} atacked {this.Name} by {from.Name} and caused {dmg} damage!");
            Console.WriteLine($"{this.Name} has {Convert.ToInt32(this.Health)} health!");
            return dmg;
        }
        public bool attack(IDamageble target)
        {
            int dmg = Convert.ToInt32(target.take_damage(Convert.ToDouble(this.Damage), this));

            if (dmg == 0)
                return true;
            else if (dmg == -1)
                return false;
            
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
                if (this.Races == from.GoodAgainst)
                    damage += damage / 10;
                else if (this.Races == from.BadAgainst)
                    damage -= damage / 10;

                int a = rand.Next(0, 2), ev = rand.Next(1, (100 / evasion) + 1);
                if (a == 0)
                    damage -= (damage / 10);
                else
                    damage += (damage / 10);
                if (ev == 1)
                    dmg = -1;
                if (dmg != -1)
                {
                    dmg = ((damage - (damage / 100 * defence)));
                    from.Health += ((dmg / 100) * this.vampirism);
                }
            }
            else if (from.DamageType == type_of_damage.magical)
                dmg = ((damage - (damage / 100 * magresist)));
            else if (from.DamageType == type_of_damage.clear)
                dmg = damage;
            this.health = Math.Max((this.health - dmg), 0);

            if (this.Health == 0 && this.LastChance == true)
            {
                this.Health = 1;
                this.LastChance = false;
                Console.WriteLine($"{this.Name} used his last chance!");
                return dmg;
            }

            if (this.health == 0)
                dmg = 0;

            if (dmg == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(this.name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" has killed by {this.name}!");
                return 0;
            }
            else if (dmg == -1)
            {
                Console.WriteLine($"{this.name} evasioned from {from.Name}!\n");
                return -1;
            }
            Console.WriteLine($"{from.Name} atacked {this.name} and caused {dmg} damage!");
            Console.WriteLine($"{this.name} has {Convert.ToInt32(this.health)} health!");
            return dmg;

        }
            

        public virtual double takeSpelDamage(double damage, spel from, Character From)
        {
            double dmg;
            if (from.DamageType == type_of_damage.magical)
                dmg = ((damage - (damage / 100 * this.magresist)));
            else
                dmg = damage;

            this.health = Math.Max((this.health - dmg), 0);
            if (this.health == 0)
                dmg = 0;

            if (this.health == 0 && this.LastChance == true)
            {
                this.health = 1;
                this.LastChance = false;
                Console.WriteLine($"{this.Name} used his last chance!");
                return dmg;
            }

            if (dmg == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(From.Name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write($" has killed by {from.Name} player {this.name}!");
                return 0;
            }


            Console.WriteLine($"{From.Name} atacked {this.Name} by {from.Name} and caused {dmg} damage!");
            Console.WriteLine($"{this.Name} has {Convert.ToInt32(this.Health)} health!");
            return dmg;
        }


        public new bool attack(IDamageble target)
        {
            int dmg = Convert.ToInt32(target.take_damage(Convert.ToDouble(this.Damage), this));

            if (dmg == 0)
                return true;
            else if (dmg == -1)
                return false;

            return false;
        }


    }
    class Magical : Character, ISpellCaster
    {
        protected Spell[] spellList = { new Fireball() };
        spel sun_strike = new spel("sun_strike", 10, type_of_damage.clear, spel_type.damage);
        spel fire = new spel("fire", 20, type_of_damage.magical, spel_type.damage);
        spel wind = new spel("wind", 12, type_of_damage.magical, spel_type.damage);
        spel regeneration = new spel("regeneration", 15, type_of_damage.magical, spel_type.regen);
        public Magical(string? name, double health, int damage, int defence, int evasion, int vampirism, int magresist, Race race = Race.Human, type_of_damage damageType = type_of_damage.physical)
                : base(name, health, damage, defence, evasion, vampirism, magresist, race, damageType) { }
        public Magical() : this("none", 100, 12, 5, 0, 0, 20) { }

        public double CastSpell(IDamageble target, spel Spel, Character from)
        {
            double dmg;
            if (Spel.TypeSpel == spel_type.regen)
            {
                this.health += Spel.Damage; 
                Console.WriteLine($"{this.name} healling himselve by {Spel.Damage}hp!");
                Console.WriteLine($"{this.name} have {Math.Round(this.Health, 2)}hp!");
                return -1;
            }
            dmg = target.takeSpelDamage(Spel.Damage, Spel, from);
            return dmg;
        }

        public new bool attack(IDamageble target)
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
                dmg = Convert.ToInt32(CastSpell(target, Spel, this));

                if (dmg == 0)
                {
                    return true;
                }
                if (dmg == -1)
                {
                    return false;
                }

                return false;
            }

            dmg = Convert.ToInt32(target.take_damage(Convert.ToDouble(this.Damage), this));

            if (dmg == 0)
                return true;
            
            else if (dmg == -1)
                return false;
            return false;
        }
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



}
