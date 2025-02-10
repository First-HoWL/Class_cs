
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

        public Point Point = new Point();

        public Tetromino(int a, Point Point)
        {
            this.shape = CanBeShape[Math.Min(a, 3)];
            this.Point = Point;
        }

        public bool Move(Point newPoint, Area area)
        {
            Point a = this.Point;
            Point = newPoint;
            if (!area.CanSpawn(this)) { 
                Point = a;
                return false;
            }
            area.MergeTetromino(this);
            return true;



        }
        public bool Move(int a, int b, Area area)
        {
            return Move(new Point(a, b), area);
        }
        public void Clear(Area area)
        {
            List<Point> coordinates = GetCoordinates();
            for (int i = 0; i < coordinates.Count(); i++)
                area.field[coordinates[i].Y, coordinates[i].X] = false;
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
        public bool Contains(int x, int y)
        {
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
        public bool MergeTetromino(Tetromino tetromino)
        {
            List<Point> ListCoordinates = tetromino.GetCoordinates();
            if (CanSpawn(tetromino) == false)
                return false;
            for (int i = 0; i < ListCoordinates.Count(); i++)
            {
                field[ListCoordinates[i].Y, ListCoordinates[i].X] = true;
            }
            return true;
        }
        public void ClearRows(ref int score)
        {
            bool isClear = true;
            for (int i = 0; i < this.height; i++)
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
        public void Draw()
        {
            
            for (int i = 0; i < this.height; i++) {
                
                Console.SetCursorPosition(i, width);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("#");
                Console.ResetColor();
            }
            for (int i = 0; i < this.width; i++)
            {
                Console.SetCursorPosition(height, i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("#");
                Console.ResetColor();
            }
            Console.SetCursorPosition(0, 0);

            for (int i = 0; i < this.height; i++)
            {
                for (int j = 0; j < this.width; j++)
                {
                    if (field[i, j] == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else
                        Console.ForegroundColor = ConsoleColor.White;
                    Console.Write("█");
                    Console.ResetColor();
                }

            }
        }

        public bool CanSpawn(Tetromino tetromino)
        {
            List<Point> ListCoordinates = tetromino.GetCoordinates();
            for (int i = 0; i < ListCoordinates.Count(); i++)
            {
                if (field[ListCoordinates[i].Y, ListCoordinates[i].X] == true)
                    return false;
            }
            return true;
        }

    }

    class Games
    {
        public int score;
        Tetromino CurentTetromino;
        Area GameArea;

        public void StartGame()
        {
            CurentTetromino = new Tetromino(new Random().Next(0, 3), new Point(2, 4));
            GameArea = new Area();
            GameArea.MergeTetromino(CurentTetromino);
            GameArea.Draw();
        }
        public void UpdateGame()
        {
            foreach (Point poi in CurentTetromino.GetCoordinates())
            {
                for (int i = GameArea.height; i > - 1 ; i--)
                {
                    if (GameArea.field[poi.X, i] == false) { 
                        GameArea.field[poi.X, i] = true;
                        break;
                    }
                }
            }
            CurentTetromino.Clear(GameArea);
            GameArea.ClearRows(ref this.score);

            CurentTetromino = new Tetromino(new Random().Next(0, 5), new Point(2, 4));
            GameArea.MergeTetromino(CurentTetromino);
            GameArea.Draw();
        }
        public bool InputHandler(ConsoleKey key)
        {
            if (key == ConsoleKey.Spacebar || key == ConsoleKey.Enter)
                UpdateGame();
            else if (key == ConsoleKey.UpArrow || key == ConsoleKey.W) { CurentTetromino.Move(CurentTetromino.Point.X, CurentTetromino.Point.Y - 1, GameArea); }
            else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S) { CurentTetromino.Move(CurentTetromino.Point.X, CurentTetromino.Point.Y + 1, GameArea); }
            else if (key == ConsoleKey.RightArrow || key == ConsoleKey.D) { CurentTetromino.Move(CurentTetromino.Point.X + 1, CurentTetromino.Point.Y, GameArea); }
            else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A) { CurentTetromino.Move(CurentTetromino.Point.X - 1, CurentTetromino.Point.Y, GameArea); }
            else if (key == ConsoleKey.R)
                CurentTetromino.Rotate();
            else 
                return false;
            return true;
        }

    }

}
