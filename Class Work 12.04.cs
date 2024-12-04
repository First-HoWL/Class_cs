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
        
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          int a;

        
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
