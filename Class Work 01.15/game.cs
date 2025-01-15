using System.Drawing;

namespace Game.Game
{
    class Player
    {
        private Point pos;
        public Point Pos { get { return pos; } set { pos.X = Math.Max(value.X, 0); pos.Y = Math.Max(value.Y, 0); } }
        public ConsoleColor color = ConsoleColor.White;

        public void Move(Point pos)
        {
            this.pos.X = Math.Min(Math.Max(this.pos.X + pos.X, 1), 100);
            this.pos.Y = Math.Min(Math.Max(this.pos.Y + pos.Y, 1), 28);
        }
        public void Move(int x, int y)
        {
            this.Move(new Point(x, y));
        }

        public void Draw()
        {
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = color;
            Console.Write("██");
            Console.ResetColor();
        }
        

        public int Distanse(Point point)
        {
            return (int) Math.Sqrt(Math.Pow(this.pos.X - point.X, 2) + Math.Pow(this.pos.Y - point.Y, 2));
        }

    }
    class Coin
    {
        private Point pos;
        public Point Pos { get { return pos; } set { pos.X = Math.Max(value.X, 0); pos.Y = Math.Max(value.Y, 0); } }
        public ConsoleColor color = ConsoleColor.Yellow;
        public void Draw()
        {            
            Console.SetCursorPosition(pos.X, pos.Y);
            Console.ForegroundColor = color;
            Console.Write("🪙");
            Console.ResetColor();
        }
        public Coin(int x, int y) : this(new Point(x, y)) { }
        public Coin(Point pos) { this.pos = pos; }
        public void Move(Point pos)
        {
            this.pos.X = Math.Min(Math.Max(this.pos.X + pos.X, 1), 100);
            this.pos.Y = Math.Min(Math.Max(this.pos.Y + pos.Y, 1), 28);
        }
        public void Move(int x, int y)
        {
            this.Move(new Point(x, y));
        }

    }

    class ScoreBar 
    {
        public int score;
        public Point pos;
        private int width = 15;
        private int height = 3; 

        public void Draw()
        {
            Console.SetCursorPosition(pos.X, pos.Y);
            for (int i = 0; i < height - 1; i++)
            {
                Console.SetCursorPosition(pos.X, pos.Y + i);
                Console.Write("|");
            }
            
            Console.SetCursorPosition(pos.X + 1, pos.Y + height - 1);
            for (int i = 0; i < width - 1; i++)
                Console.Write("_");
            for (int i = 0; i < height - 1; i++)
            {
                Console.SetCursorPosition(pos.X + width, pos.Y + i);
                Console.Write("|");
            }
            Console.SetCursorPosition(pos.X + width, pos.Y + height - 1);
            Console.Write("\b/");
            Console.SetCursorPosition(pos.X + 2, pos.Y + height - 1);
            Console.Write("\b\\");
            Console.SetCursorPosition(pos.X + (width / 2) , pos.Y + (height / 2) - (height % 2));
            Console.Write(score);
        }
    }

}