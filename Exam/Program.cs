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
            Games game = new Games();
            game.StartGame();
            
        }
    }
}