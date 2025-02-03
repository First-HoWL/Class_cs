using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SnakeGame
{
    enum snakeMove{
        top,
        bottom,
        left,
        right,
    }
    class snake
    {
        public List<Point> position = new List<Point>();
        public ConsoleColor color = ConsoleColor.Green;
        public ConsoleColor HeadColor = ConsoleColor.Blue;
        public char headChar = '@';
        public char bodyChar = '0';
        public snake()
        {
            position.Add(new Point(2, 2));
            position.Add(new Point(1, 2));
        }
        public Point getHead()
        {
            return position[0];
        }

        public Point getLast()
        {
            return position[position.Count() - 1];
        }
        public bool isSnake(Point pos)
        {
            for (int i = 0; i < position.Count(); i++)
            {
                if (position[i] == pos)
                {
                    return true;
                }
            }
            return false;
        }
        public bool isSnake(int a, int b){
            return isSnake(new Point(a, b));
        }
        public bool Move(snakeMove toMove)
        {
            Point NEWpoint = new Point();

            if (toMove == snakeMove.top)
            {
                NEWpoint = getHead();
                NEWpoint.Y++;
            }
            else if (toMove == snakeMove.bottom)
            {
                NEWpoint = getHead();
                NEWpoint.Y--;
            }
            else if (toMove == snakeMove.left)
            {
                NEWpoint = getHead();
                NEWpoint.X--;
            }
            else if (toMove == snakeMove.right)
            {
                NEWpoint = getHead();
                NEWpoint.X++;
            }

            if (isSnake(NEWpoint)) 
            {
                if (NEWpoint == position[1])
                {
                    return true;
                }
                return false;
            }
            return true;
        }
        public void print()
        {
            for (int i = 0; i < position.Count(); i++)
            if (i == 0)
            {
                Console.SetCursorPosition(position[i].X, position[i].Y);
                Console.ForegroundColor = HeadColor;
                Console.Write(headChar);
                Console.ResetColor();
            }
            else
            {
                Console.SetCursorPosition(position[i].X, position[i].Y);
                Console.ForegroundColor = color;
                Console.Write(bodyChar);
                Console.ResetColor();
            }
        }
        
        
    }

    class Game
    {
        snake Snake = new snake();
        List<Point> fruits = new List<Point>();
        Point fieldSize = new Point(40, 20);
        public bool isGameOver = false;
        public void draw()
        {
            for (int j = 0; j < fieldSize.Y + 1; j++)
            {
                for (int i = 0; i < fieldSize.X + 1; i++)
                {
                    if (i >= fieldSize.X || j >= fieldSize.Y || i == 0 || j == 0)
                    {
                        Console.BackgroundColor = ConsoleColor.White;
                        Console.Write("█");
                    }
                    else if (Snake.isSnake(i, j))
                    {
                        if (new Point(i, j) == Snake.getHead())
                        {
                            Console.ForegroundColor = Snake.HeadColor;
                            Console.Write(Snake.headChar);
                        }
                        else
                        {
                            Console.ForegroundColor = Snake.color;
                            Console.Write(Snake.bodyChar);
                        }
                    }
                    else if (isFruit(i, j))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.Write("o");
                    }
                    else
                        Console.Write(" ");
                    Console.ResetColor();

                }
                Console.WriteLine();
            }
        }
        public Game()
        {
            Random random = new Random();
            Point a = new Point(random.Next(1, 40), random.Next(1, 20));
            fruits.Add(a);
        }
        public void Update(snakeMove toMove)
        {
            Point NEWpoint = new Point();

            if (toMove == snakeMove.top)
            {
                NEWpoint = Snake.getHead();
                NEWpoint.Y--;
            }
            else if (toMove == snakeMove.bottom)
            {
                NEWpoint = Snake.getHead();
                NEWpoint.Y++;
            }
            else if (toMove == snakeMove.left)
            {
                NEWpoint = Snake.getHead();
                NEWpoint.X--;
            }
            else if (toMove == snakeMove.right)
            {
                NEWpoint = Snake.getHead();
                NEWpoint.X++;
            }

            if (Snake.isSnake(NEWpoint))
            {
                if (NEWpoint != Snake.position[1])
                    isGameOver = true;
            }
            else if (NEWpoint.X == fieldSize.X || NEWpoint.Y == fieldSize.Y || NEWpoint.X == 0 || NEWpoint.Y == 0) {
                isGameOver = true;
            }
            else if (isFruit(NEWpoint))
            {
                Console.SetCursorPosition(fruits[0].X, fruits[0].Y);
                Console.Write(" ");
                Snake.position.Insert(0, NEWpoint);
                fruits.Clear();
                Random random = new Random();
                Point a;
                do
                {
                    a = new Point(random.Next(1, 40), random.Next(1, 20));
                } while (Snake.isSnake(a));
                fruits.Add(a);
                Console.SetCursorPosition(fruits[0].X, fruits[0].Y);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("o");
                Console.ResetColor();
                Snake.print();
            }
            else
            {
                Console.SetCursorPosition(Snake.getLast().X, Snake.getLast().Y);
                Console.Write(" ");
                Snake.position.Insert(0, NEWpoint);
                Snake.position.RemoveAt(Snake.position.Count() - 1);
                Snake.print();
            }
        }
        public bool isFruit(Point pos)
        {
            for (int i = 0; i < fruits.Count(); i++)
            {
                if (fruits[i] == pos)
                {
                    return true;
                }
            }
            return false;
        }
        public bool isFruit(int a, int b)
        {
            return isFruit(new Point(a, b));
        }

    }
    
}


