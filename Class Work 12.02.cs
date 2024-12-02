// See https://aka.ms/new-console-template for more information
using System.Text;
using Game;
using System.Threading;
class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.InputEncoding = UTF8Encoding.UTF8;

        Character player1 = new Character("HoWL", 100, 15, 4);
        player1.print();
        Console.WriteLine();

        Character player2 = new Character("Anton", 90, 18, 7);
        player2.print();

        while (true)
        {
            Console.WriteLine();
            player2.attack(player1);
            player1.print();

            Thread.Sleep(2000);
        }

        
    }
}






namespace Game
{
    class Character
    {
        string? name;
        int health;
        int damage;
        int defence;

        public Character(string? name, int health, int damage, int defence)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.defence = defence;
        }

        public void print()
        {
            Console.WriteLine($"-< {name} >- ");
            Console.WriteLine($" Hp: {health} ");
            Console.WriteLine($" Damage: {damage} ");
            Console.WriteLine($" Defence: {defence} ");
        }

        public int take_damage(int damage_)
        {
            double damage = Convert.ToDouble(damage_);
            Random? rand = new Random();
            int a = rand.Next(0, 2);
            if (a == 0)
                damage -= (damage / 10);
            else
                damage += (damage / 10);
            int dmg = Convert.ToInt32((damage - (damage / 100 * defence)));
            this.health -= Math.Max(dmg, 0);
            return dmg;
        }
        public void attack( Character target ) { 
            target.take_damage(this.damage);
        }

    }
}
