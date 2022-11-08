using System;
using System.Threading;
using System.Collections.Generic;

class Snake
{
    public static char dir = 'u';
    public static Random rnd = new Random();
    public static byte width = 20, height = 11;
    public static bool grow = false, game = true;
    public static List<byte[]> snake = new List<byte[]>();
    public static byte[] point = {Convert.ToByte(rnd.Next(1, width)), Convert.ToByte(rnd.Next(1, height))};
    
    static void Main(string[] args)
    {
        Console.CursorVisible = false;

        for (byte i = 0; i < 3; i++)
        {
            byte[] l = {10, Convert.ToByte(i+5)};
            snake.Add(l);
        }
        
        Draw();

        while(game)
        {
            if (Console.KeyAvailable) dir = KeyPressed();
            Move();
            if (snake[0][0] == point[0] && snake[0][1] == point[1])
            {
                Console.Beep(2000, 200);
                grow = true;
                point = Spawnpoint();
            }
            Redraw();
            Thread.Sleep(300);
        }

        Console.Beep(3000, 200);
        Console.Clear();
        Console.SetCursorPosition(0, 0);
        Console.WriteLine("Vége");
    }

    static char KeyPressed()
    {
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.UpArrow:
            case ConsoleKey.W:
                Console.Beep(500, 100);
                return 'u';
            case ConsoleKey.DownArrow:
            case ConsoleKey.S:
                Console.Beep(500, 100);
                return 'd';
            case ConsoleKey.LeftArrow:
            case ConsoleKey.A:
                Console.Beep(500, 100);
                return 'l';
            case ConsoleKey.RightArrow:
            case ConsoleKey.D:
                Console.Beep(500, 100);
                return 'r';
            case ConsoleKey.Escape:
                game = false;
                return dir;
            default:
                return dir;
        }
    }

    static void Draw()
    {
        
        for (byte i = 0; i < height; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write('|');
        }
        for (byte i = 0; i < height; i++)
        {
            Console.SetCursorPosition(width, i);
            Console.Write('|');
        }
        for (byte i = 0; i < width + 1; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write('-');
        }
        for (byte i = 0; i < width + 1; i++)
        {
            Console.SetCursorPosition(i, height);
            Console.Write('-');
        }
        foreach(byte[] s in snake)
        {
            Console.SetCursorPosition(s[0], s[1]);
            if (snake.IndexOf(s)==0) Console.ForegroundColor = ConsoleColor.Red;
            else Console.ForegroundColor = ConsoleColor.Green;
            Console.Write('*');
        }
    }
    
    static void Move()
    {
        byte[] head = new byte[] {snake[0][0], snake[0][1]};

        if (dir == 'u') head[1]--;
        else if (dir == 'd') head[1]++;
        else if (dir == 'l') head[0]--;
        else head[0]++;

        snake.Insert(0, head);

        if (grow) grow = false;
        else
        {
            Console.SetCursorPosition(snake[snake.Count-1][0], snake[snake.Count-1][1]);
            Console.Write(" ");
            snake.RemoveAt(snake.Count-1);
        }
        
        if (IsSnakeAlive()) game = false;
    }

    static void Redraw()
    {
        Console.SetCursorPosition(snake[0][0], snake[0][1]);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("*");
        Console.SetCursorPosition(snake[1][0], snake[1][1]);
        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write("*");
        Console.SetCursorPosition(point[0], point[1]);
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.Write("*");
    }

    static byte[] Spawnpoint()
    {
        byte[] p = new byte[2];
        while (true)
        {
            p = new byte[] {Convert.ToByte(rnd.Next(1,width)), Convert.ToByte(rnd.Next(1,height))};
            if (!snake.Contains(p)) return p;
        }
    }

    static bool IsSnakeAlive()
    {
    for(int i = snake.Count-1; i > 0; i--)
        if (snake[i][0] == snake[0][0] && snake[i][1] == snake[0][1] || (snake[0][0]==0 || snake[0][0] == width || snake[0][1] == 0 || snake[0][1] == height))
            return true;
        return false;
    }
}
