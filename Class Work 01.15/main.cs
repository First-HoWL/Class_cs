using System.Text;
using Game.Game;
using System.Threading;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace Game
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = UTF8Encoding.UTF8;
            Random rnd = new Random(DateTime.Now.Second);

            Player player = new Player();
            player.Pos = new Point(1, 3);


            /*Player player2 = new Player();
            player2.Pos = new Point(4, 3);
            player2.color = ConsoleColor.Green;*/
            
            List<Coin> coinList = new List<Coin>();
            for (int i = 0; i < 5; i++)
                coinList.Add(new Coin(rnd.Next(1, 100), rnd.Next(1, 28)));

            ScoreBar scoreBar = new ScoreBar();
            scoreBar.pos = new Point(20, 0);
            while (true) {
                scoreBar.Draw();
                foreach (Coin coin in coinList.ToList()) 
                { 
                    if (player.Distanse(coin.Pos) <= 1)
                    {
                        
                        coinList.Remove(coin);
                        scoreBar.score++;
                        coinList.Add(new Coin(rnd.Next(1, 100), rnd.Next(1, 28)));
                    }
                    else
                    {
                        coin.Move(rnd.Next(-2, 2) >= 0 ? 1 : -1, rnd.Next(-2, 2) >= 0 ? 1 : -1) ;
                        coin.Draw();
                    }

                }

                player.Draw();
                //player2.Draw();
                

                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.Delete) { Console.Clear();   break; }
                if (key == ConsoleKey.W)
                    player.Move(0, -1);
                else if (key == ConsoleKey.A)
                    player.Move(-1, 0);
                else if (key == ConsoleKey.S)
                    player.Move(0, 1);
                else if (key == ConsoleKey.D)
                    player.Move(1, 0);
               /* else if (key == ConsoleKey.UpArrow)
                    player2.Move(0, -1);
                else if (key == ConsoleKey.LeftArrow)
                    player2.Move(-1, 0);
                else if (key == ConsoleKey.DownArrow)
                    player2.Move(0, 1);
                else if (key == ConsoleKey.RightArrow)
                    player2.Move(1, 0);*/
                Console.Clear();
            }
        }
    }
}