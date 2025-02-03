using System.Text;
using System.Threading;
using System;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection.Metadata;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Text.Json;
using SnakeGame;

namespace SnakeGame
{
    class Program
    {


        

        public static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = UTF8Encoding.UTF8;
            Game game = new Game();
            game.draw();
            snakeMove SnakeMove = new snakeMove();
            while (true) {
                Console.SetCursorPosition(0, 0);
                ConsoleKey? key = Console.ReadKey(true).Key;
                if (key == ConsoleKey.UpArrow || key == ConsoleKey.W) { SnakeMove = snakeMove.top; }
                if (key == ConsoleKey.DownArrow || key == ConsoleKey.S) { SnakeMove = snakeMove.bottom; }
                if (key == ConsoleKey.RightArrow || key == ConsoleKey.D) { SnakeMove = snakeMove.right; }
                if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A) { SnakeMove = snakeMove.left; }
                //Console.Clear();
                game.Update(SnakeMove);
                //game.draw();

                if (game.isGameOver)
                    break;
                

            }
            
            Console.ForegroundColor = ConsoleColor.Red;
            Console.SetCursorPosition(17, 10);
            Console.WriteLine("Not Good!");
            Console.ResetColor();
            Thread.Sleep(1000);
            Console.SetCursorPosition(0, 21);

        }
    }
}