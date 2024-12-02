// See https://aka.ms/new-console-template for more information
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


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
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = UTF8Encoding.UTF8;
        Console.InputEncoding = UTF8Encoding.UTF8;

        Character player1 = new Character("HoWL", 100, 15, 4);
        player1.print();

    }
}
