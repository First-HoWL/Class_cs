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
        }
    }
}





using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Game
{


    class Tetromino
    {
        List<bool[,]> CanBeShape = new List<bool[,]>
        {
            new bool[3, 3]{
            { true, false, false },
            { true, true, true },
            { false, false, false }
        },
        {
            new bool[3, 3]{
            { true, true, false },
            { true, true, false },
            { false, false, false }
        } },
        {
            new bool[3, 3]{
            { true, false, false },
            { true, false, false },
            { true, false, false }
        } },
        {
            new bool[3, 3]{
            { true, true, true },
            { false, true, false },
            { false, true, false }
        } }

        };



        public bool[,] shape = new bool[3, 3] {
            { true, false, true },
            { true, true, true },
            { true, false, true }
        };

        Point Point = new Point();

        public void Move(Point newPoint)
        {
            Point = newPoint;
        }
        public void Move(int a, int b)
        {
            Move(new Point(a, b));
        }
        public void Rotate()
        {
            bool[,] newShape = new bool[3, 3] {
                { shape[2, 0], shape[1, 0], shape[0, 0] },
                { shape[2, 1], shape[1, 1], shape[0, 1] },
                { shape[2, 2], shape[1, 2], shape[0, 2] },
            };
            this.shape = newShape;
        }
        public List<Point> GetCoordinates()
        {
            List<Point> coordinates = new List<Point>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    coordinates.Add(new Point(Point.X + j, Point.Y + i));
                }
            }


            return coordinates;
        }

        public bool Contains(Point point)
        {
            List<Point> coordinates = GetCoordinates();
            for (int i = 0; i < coordinates.Count(); i++)
            {
                if (coordinates[i] == point)
                    return true;
            }
            return false;
        }
        public bool Contains(int x, int y){
            return Contains(new Point(x, y));
        }

    }
    
    class Area
    {
        public int width; 
        public int height;

        public bool[,] field;

        public Area(int width = 10, int height = 20)
        {
            field = new bool[width, height];
        }
        public void MergeTetromino(Tetromino tetromino)
        {
            List<Point> ListCoordinates = tetromino.GetCoordinates();
            for (int i = 0; i < ListCoordinates.Count(); i++)
            {
                field[ListCoordinates[i].Y, ListCoordinates[i].X] = true;
            }
        }
        public void ClearRows(ref int score)
        {
            bool isClear = true;
            for(int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; i++)
                    if (field[i, j] == false)
                        isClear = false;
                if (isClear)
                {
                    score++;
                    for (int j = i; j > -1; j--)
                    {
                        for (int k = 0; k < width; k++) 
                        {
                            field[j, k] = field[j - 1, k];
                            field[j - 1, k] = false;
                        }
                    }
                    isClear = false;
                }
            }
        }
        public void draw()
        {
            for (int i = 0; i < this.height ;i++) 
            { 
                for (int j = 0; j < this.width; j++)
                {
                    if (field[i, j] == false)
                    {
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    else
                        Console.BackgroundColor = ConsoleColor.White;
                    Console.WriteLine("█");

                }
            
            }
        }

    }


}


