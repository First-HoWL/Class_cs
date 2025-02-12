
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

        public Point point = new Point();

        public Tetromino(int a, Point Point)
        {
            this.shape = CanBeShape[Math.Min(a, 3)];
            this.point = Point;
        }

        public bool Move(Point newPoint, Area area)
        {
            Console.WriteLine(this.point);
            Thread.Sleep(500);
            Tetromino NewTet = this;
            NewTet.point = newPoint;
            if (area.CanSpawn(NewTet, this) == false)
                return false;
            
            this.Clear(area);
            area.MergeTetromino(NewTet);
            this.point = NewTet.point;
            Console.Clear();
            area.Draw();
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
            Console.Clear();
            area.Draw();
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
                    if (shape[i, j] == true)
                        coordinates.Add(new Point(point.X + j, point.Y + i));
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

        public Area(int width = 20, int height = 10)
        {
            this.width = width;
            this.height = height;

            field = new bool[width, height];
            for (int i = 0; i < this.width - 1; i++)
            {
                for (int j = 0; j < this.height - 1; j++)
                {
                    field[i, j] = false;
                }
            }
        }
        public bool MergeTetromino(Tetromino tetromino)
        {
            List<Point> ListCoordinates = tetromino.GetCoordinates();
            for (int i = 0;i < ListCoordinates.Count();i++)
                Console.WriteLine(ListCoordinates[i]);
            //Thread.Sleep(10000);
            if (CanSpawn(tetromino, tetromino) == false)
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
            for (int i = 0; i < this.width - 1; i++)
            {
                for (int j = 0; j < this.height - 1; j++)
                    if (field[i, j] == false)
                        isClear = false;
                if (isClear)
                {
                    score++;
                    for (int j = i; j > -1; j--)
                    {
                        for (int k = 0; k < width - 1; k++)
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
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < this.width - 1; i++)
            {
                for (int j = 0; j < this.height - 1; j++)
                {
                    if (field[i, j] == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else { 
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    Console.Write("█");
                    Console.ResetColor();
                }
                Console.WriteLine();

            }

            for (int i = 0; i < this.height; i++)
            {

                Console.SetCursorPosition(i, width - 1);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("#");
                Console.ResetColor();
            }
            for (int i = 0; i < this.width; i++)
            {
                Console.SetCursorPosition(height - 1, i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("#");
                Console.ResetColor();
            }
        }

        public bool CanSpawn(Tetromino tetromino, Tetromino oldtetromino)
        {
            List<Point> ListCoordinates = tetromino.GetCoordinates();
            for (int i = 0; i < ListCoordinates.Count(); i++)
            {
                if (tetromino.point.X < 0 || tetromino.point.X > this.height - 1 || tetromino.point.Y < 0 || tetromino.point.Y > this.width - 1 || ListCoordinates[i].Y < 0 || ListCoordinates[i].X < 0 || ListCoordinates[i].Y > this.width - 1 || ListCoordinates[i].X > this.height - 1)
                    return false;
                if (this.field[ListCoordinates[i].Y, ListCoordinates[i].X] == true && !oldtetromino.Contains(ListCoordinates[i]))
                    return false;
            }
            return true;
        }

    }

    class Games
    {
        public int score;
        Tetromino CurentTetromino;
        public Area GameArea;
        public bool gameStop = false;

        public void StartGame()
        {
            CurentTetromino = new Tetromino(new Random().Next(0, 3), new Point(2, 1));
            GameArea = new Area();
            GameArea.MergeTetromino(CurentTetromino);
            GameArea.Draw();
        }
        public bool UpdateGame()
        {
            foreach (Point poi in CurentTetromino.GetCoordinates())
            {
                for (int i = GameArea.width - 1; i > -1; i--)
                {
                    if (GameArea.field[i, poi.X] == false)
                    {
                        GameArea.field[i, poi.X] = true;
                        break;
                    }
                }
            }
            CurentTetromino.Clear(GameArea);
            GameArea.ClearRows(ref this.score);

            CurentTetromino = new Tetromino(new Random().Next(0, 3), new Point(2, 1));
            if (GameArea.MergeTetromino(CurentTetromino) == false)
                this.gameStop = true;
            GameArea.Draw();
            return true;
        }
        public bool InputHandler(ConsoleKey key)
        {
            if (key == ConsoleKey.Spacebar || key == ConsoleKey.Enter)
                UpdateGame();
            else if (key == ConsoleKey.UpArrow || key == ConsoleKey.W) { CurentTetromino.Move(new Point(CurentTetromino.point.X, CurentTetromino.point.Y - 1), GameArea); }
            else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S) { CurentTetromino.Move(new Point(CurentTetromino.point.X, CurentTetromino.point.Y + 1), GameArea); }
            else if (key == ConsoleKey.RightArrow || key == ConsoleKey.D) { CurentTetromino.Move(new Point(CurentTetromino.point.X + 1, CurentTetromino.point.Y), GameArea); }
            else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A) { CurentTetromino.Move(new Point(CurentTetromino.point.X - 1, CurentTetromino.point.Y), GameArea); }
            else if (key == ConsoleKey.R)
                CurentTetromino.Rotate();
            else
                return false;
            return true;
        }

    }

}
