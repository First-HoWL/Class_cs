// See https://aka.ms/new-console-template for more information
using System.Text;
using Game;
using System.Threading;
using System;
using System.IO;
using System.Net.NetworkInformation;

public static class File{};

class Program
{
    static void Main(string[] args)
    {
        Bank_account account = new Bank_account();
        Bank_account account2 = new Bank_account("Walter", "Black", 93271214, 20000.0, 4.3);
        account.print();
        Console.WriteLine();
        account2.print();
        Console.WriteLine();
        Console.WriteLine(account.perevod(account2, 200));
        Console.WriteLine();
        account.print();
        Console.WriteLine();
        account2.print();
        
        /* 
        Web_site site = new Web_site("Cisco", "cisco.com", "its just Cisco", "72.163.4.185");
        site.print();
        var ping = new Ping();
        PingReply response = ping.Send(site.getUrl());
        Console.WriteLine($"Ping - {response.Status}");
        */
        /*
        Character player1 = new Character("HoWL", 100, 15, 4, 15);
        player1.print();
        Console.WriteLine();

        Character player2 = new Character("Anton", 80, 18, 7, 10);
        player2.print();

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
        */

    }
}





using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System;
using System.Security.Cryptography.X509Certificates;

namespace Game
{
    class Character
    {
        string? name;
        int health;
        int damage;
        int defence;
        int evasion;


        public Character(string? name, int health, int damage, int defence, int evasion)
        {
            this.name = name;
            this.health = health;
            this.damage = damage;
            this.defence = defence;
            this.evasion = evasion;
        }

        public void print()
        {
            Console.WriteLine($"-< {name} >- ");
            Console.WriteLine($"Hp: {health} ");
            Console.WriteLine($"Damage: {damage} ");
            Console.WriteLine($"Defence: {defence} ");
            Console.WriteLine($"Evasion: {evasion}");
        }

        public int take_damage(int damage_)
        {
            double damage = Convert.ToDouble(damage_);
            Random? rand = new Random();
            int a = rand.Next(0, 2), ev = rand.Next(0, 100 / evasion);
            if (a == 0)
                damage -= (damage / 10);
            else
                damage += (damage / 10);
            if (ev == 1) {
                return -1;
            }
            int dmg = Convert.ToInt32((damage - (damage / 100 * defence)));
            this.health = Math.Max(this.health - dmg, 0);
            if (this.health == 0)
                return 0;
            return dmg;
        }
        public bool attack(Character target)
        {
            int dmg = target.take_damage(this.damage);
            if (dmg == 0)
            {
                Console.Write($"{this.name} has kiled ");
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.Write(target.name);
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.Write("!");
                return true;
            }
            else if (dmg == -1)
            {
                Console.Write($"{target.name} evasioned from {this.name}!");
                return false;
            }
            Console.WriteLine($"{this.name} atacked {target.name} and caused {dmg} damage!");
            Console.WriteLine($"{target.name} has {target.health} health!");
            return false;
        }

    }

    class Web_site {
        string Name;
        string Url;
        string Description;
        string IP;

        public string getName() {
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

        public Bank_account(string name, string seccond_name,int number, double balance, double InterenceRate)
        {
            this.name = name;
            this.seccond_name = seccond_name;
            this.number = number;
            this.Balance = balance;
            this.InterenceRate = InterenceRate;

        }
        public Bank_account(): this("Walter", "White", 12434214, 10000.0, 5.5) { }
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
            if (this.Balance >= count) {
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
            if (from.Balance >= count) {
                from.Withdraw(count);
                this.Deposit(count - Math.Min(count * 0.01, 50));
                return true;
            }
            else 
                return false;
        }
    }
}
