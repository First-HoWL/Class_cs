
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
        List<ConsoleColor> colors = new List<ConsoleColor>
        {
            ConsoleColor.Green, ConsoleColor.Red, ConsoleColor.DarkGreen, ConsoleColor.Magenta, ConsoleColor.Blue, ConsoleColor.Cyan
        };


        public bool[,] shape = new bool[3, 3] {
            { true, false, true },
            { true, true, true },
            { true, false, true }
        };
        
        Point point = new Point();
        public Point Point { get { return point; } }
        public Point SetPoint { set { point = value; } }
        public ConsoleColor color;


        public Tetromino(int a, Point Point)
        {
            Random rnd = new Random();
            color = colors[rnd.Next(0, 6)];

            this.shape = CanBeShape[Math.Min(a, 3)];
            this.point = Point;
        }
        public Tetromino(int a, Point Point, ConsoleColor color)
        {
            this.color = color;

            this.shape = CanBeShape[Math.Min(a, 3)];
            this.point = Point;
        }
        public Tetromino(Point Point, bool[,] newShape)
        {
            Random rnd = new Random();
            color = colors[rnd.Next(0, 6)];
            this.shape = newShape;
            this.point = Point;
        }

        public bool Move(Point newPoint, Games game)
        {
            
            
            Tetromino NewTet = new Tetromino(1, newPoint, this.color);
            NewTet.shape = this.shape;
            
            
            if (game.GameArea.CanSpawn(NewTet, this) == false)
            { return false; }

            game.GameArea.Clear(game);
            game.GameArea.MergeTetromino(NewTet);
            this.point = NewTet.point;


            Console.SetCursorPosition(0, 0);
            game.GameArea.Draw();

            return true;

        }
        public bool Move(int a, int b, Games game)
        {
            return Move(new Point(a, b),game);
        }
        
        public bool Rotate(Games game)
        {
            bool[,] newShape = new bool[3, 3] {
                { shape[2, 0], shape[1, 0], shape[0, 0] },
                { shape[2, 1], shape[1, 1], shape[0, 1] },
                { shape[2, 2], shape[1, 2], shape[0, 2] },
            };
            
            Tetromino newTetromino = new Tetromino(this.Point, newShape);

            if (game.GameArea.CanSpawn(newTetromino, this) == false) { return false; }
            game.GameArea.Clear(game);
            this.shape = newShape;
            game.GameArea.MergeTetromino(this);
            Console.SetCursorPosition(0, 0);
            game.GameArea.Draw();
            return true;
        }
        public List<Point> GetCoordinates()
        {
            List<Point> coordinates = new List<Point>();
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (shape[i, j] == true) {
                        coordinates.Add(new Point(point.X + i, point.Y + j));
                    }
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
        public ConsoleColor[,] fieldColor; 

        public Area(int width = 20, int height = 10)
        {
            this.width = width;
            this.height = height;

            field = new bool[width, height];
            fieldColor = new ConsoleColor[width, height];
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    field[i, j] = false;
                    fieldColor[i, j] = ConsoleColor.Black;
                }
            }
        }
        public void Clear(Games game)
        {
            
            foreach (Point p in game.CurentTetromino.GetCoordinates())
            {
                if (p.Y >= 0 && p.Y < width && p.X >= 0 && p.X < height)
                {
                    field[p.Y, p.X] = false;
                }
            }
            Console.SetCursorPosition(0, 0);
            Draw();
        }


        public bool MergeTetromino(Tetromino tetromino)
        {
            List<Point> ListCoordinates = tetromino.GetCoordinates();
            
            if (CanSpawn(tetromino) == false)
                return false;
            for (int i = 0; i < ListCoordinates.Count(); i++)
            {
                field[ListCoordinates[i].Y, ListCoordinates[i].X] = true;
                fieldColor[ListCoordinates[i].Y, ListCoordinates[i].X] = tetromino.color;
            }
            return true;
        }
        public void ClearRows(ref int score)
        {
            bool isClear = true;
            for (int i = 0; i < this.width; i++)
            {
                isClear = true;
                for (int j = 0; j < this.height; j++)
                    if (this.field[i, j] == false)
                        isClear = false;
                if (isClear == true)
                {
                    score++;
                    for (int j = i; j > 0; j--)
                    {
                        for (int k = 0; k < this.height; k++)
                        {
                            this.field[j, k] = this.field[j - 1, k];
                            this.field[j - 1, k] = false;
                        }
                    }
                    isClear = false;
                }
            }
        }
        public void Draw()
        {
            Console.SetCursorPosition(0, 0);
            for (int i = 0; i < this.width; i++)
            {
                for (int j = 0; j < this.height; j++)
                {
                    if (field[i, j] == false)
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                    }
                    else { 
                        Console.ForegroundColor = fieldColor[i, j];
                    }
                    Console.Write("█");
                    Console.ResetColor();
                }
                Console.WriteLine();

            }

            for (int i = 0; i < this.width; i++)
            {
                Console.SetCursorPosition(height, i);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write("#");
                Console.ResetColor();
            }
            for (int i = 0; i < this.height; i++)
            {

                Console.SetCursorPosition(i, width);
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
                if (tetromino.Point.X < 0 || tetromino.Point.X > this.height - 1 || tetromino.Point.Y < 0 || tetromino.Point.Y > this.width - 1 || ListCoordinates[i].Y < 0 || ListCoordinates[i].X < 0 || ListCoordinates[i].Y > this.width - 1 || ListCoordinates[i].X > this.height - 1)
                    return false;
                if (this.field[ListCoordinates[i].Y, ListCoordinates[i].X] == true && !oldtetromino.Contains(ListCoordinates[i]))
                    return false;
            }
            return true;
        }
        public bool CanSpawn(Tetromino tetromino)
        {
            List<Point> ListCoordinates = tetromino.GetCoordinates();
            for (int i = 0; i < ListCoordinates.Count(); i++)
            {
                if (tetromino.Point.X < 0 || tetromino.Point.X > this.height - 1 || tetromino.Point.Y < 0 || tetromino.Point.Y > this.width - 1 || ListCoordinates[i].Y < 0 || ListCoordinates[i].X < 0 || ListCoordinates[i].Y > this.width - 1 || ListCoordinates[i].X > this.height - 1)
                    return false;
                if (this.field[ListCoordinates[i].Y, ListCoordinates[i].X] == true)
                    return false;
            }
            return true;
        }

    }

    class Games
    {
        public int score;
        public Tetromino CurentTetromino;
        public Area GameArea;
        public bool gameStop = false;

        public void StartGame()
        {
            CurentTetromino = new Tetromino(new Random().Next(0, 3), new Point(2, 1));
            GameArea = new Area();
            GameArea.MergeTetromino(CurentTetromino);
            GameArea.Draw();
            ConsoleKey key;
            do
            {
                do
                {
                    key = Console.ReadKey(true).Key;
                } while (InputHandler(key) == false || gameStop != true);
            } while (gameStop == false);
        }
        public bool UpdateGame()
        {
            GameArea.Clear(this);
            foreach (Point poi in CurentTetromino.GetCoordinates())
            {
                for (int i = GameArea.width - 1; i > -1; i--)
                {
                    if (GameArea.field[i, poi.X] == false)
                    {
                        GameArea.field[i, poi.X] = true;
                        GameArea.fieldColor[i, poi.X] = CurentTetromino.color;
                        break;
                    }
                }
            }
            
            GameArea.ClearRows(ref this.score);
            Console.SetCursorPosition(12, 4);
            Console.WriteLine(this.score * 100);
            Console.SetCursorPosition(0, 0);

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
            else if (key == ConsoleKey.UpArrow || key == ConsoleKey.W) { CurentTetromino.Move(new Point(CurentTetromino.Point.X, CurentTetromino.Point.Y - 1), this); }
            else if (key == ConsoleKey.DownArrow || key == ConsoleKey.S) { CurentTetromino.Move(new Point(CurentTetromino.Point.X, CurentTetromino.Point.Y + 1), this); }
            else if (key == ConsoleKey.RightArrow || key == ConsoleKey.D) { CurentTetromino.Move(new Point(CurentTetromino.Point.X + 1, CurentTetromino.Point.Y), this); }
            else if (key == ConsoleKey.LeftArrow || key == ConsoleKey.A) { CurentTetromino.Move(new Point(CurentTetromino.Point.X - 1, CurentTetromino.Point.Y), this); }
            else if (key == ConsoleKey.R)
                CurentTetromino.Rotate(this);
            else
                return false;
            return true;
        }

    }

}