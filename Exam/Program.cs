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
using Game;

namespace SnakeGame
{
    class Program
    {

        public static void Main(string[] args)
        {
            Console.OutputEncoding = UTF8Encoding.UTF8;
            Console.InputEncoding = UTF8Encoding.UTF8;
            // https://github.com/svitlyi-itstep/CSH_P35/blob/master/CSH_P35/exam.md
            Games game = new Games();

            game.StartGame();
            ConsoleKey key;
            while (true)
            {
                do
                {
                     key = Console.ReadKey(true).Key;
                } while (game.InputHandler(key) == false || game.gameStop == true);
                if (game.gameStop == true)
                    break;
                Console.Clear();
                game.GameArea.Draw();
            }
        }
    }
}
