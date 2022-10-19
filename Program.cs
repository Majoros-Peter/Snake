class Snake
{
    public static Random rand = new Random();
    public static string[] snake = { "12;6", "12;7", "12;8", "12;9", "12;10", "12;11" };
    public static string point = "1;1";
    public static bool grow = false;

    public static void Main(string[] args)
    {
        string irany = "f";
        byte width = Convert.ToByte(rand.Next(10, 31)), height = Convert.ToByte(rand.Next(10, 26));
        point = spawnPoint(width, height);

        while (true)
        {
            drawMap(width, height);         // Megrajzolja a kígyót.
            irany = keyPressed(irany);      // Megvizsgálja, hogy melyik gomb van lenyomva.
            snakeMove(irany);               // Mozog.
            if (snake[0] == point)
            {
                point = spawnPoint(width, height);
                grow = true;
            }
            Thread.Sleep(100);              // 0.1 mp Késleltetés.
            Console.Clear();                // Törli, hogy majd újra tudja rajzolni.
            if (isSnakeAlive()) break;      // Megvizsgálja, hogy nem-e érintkezik a kígyónak valamelyik része.
        }

        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Vége");
    }


    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////


    public static void drawMap(byte width, byte height)
    {
        string coord;
        for (byte y = 0; y < height; y++)
            for (byte x = 0; x < width * 2; x++)
            {
                coord = Convert.ToString(x) + ";" + Convert.ToString(y);
                Console.ForegroundColor = ConsoleColor.White;
                if (y == 0 || y == height - 1) Console.Write("-");
                else if (x == 0 || x == (width * 2) - 1) Console.Write("|");
                else if (coord == point)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("*");
                }
                else if (coord == snake[0])
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write("*");
                }
                else if (snake.Contains(coord))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("*");
                }
                else Console.Write(" ");
                if (x == (width * 2) - 1) Console.WriteLine();
            }
    }


    public static string keyPressed(string direction)
    {
        switch (Console.ReadKey().Key)
        {
            case ConsoleKey.UpArrow:
            case ConsoleKey.W:
                return "f";
            case ConsoleKey.DownArrow:
            case ConsoleKey.S:
                return "l";
            case ConsoleKey.LeftArrow:
            case ConsoleKey.A:
                return "b";
            case ConsoleKey.RightArrow:
            case ConsoleKey.D:
                return "j";
            default:
                return direction;
        }
    }//todo needs fixing


    public static void snakeMove(string direction)
    {
        string[] head = snake[0].Split(';');
        byte szam;

        switch (direction)
        {
            case "f":
                szam = Convert.ToByte(head[1]);
                head[1] = Convert.ToString(szam - 1);
                break;
            case "l":
                szam = Convert.ToByte(head[1]);
                head[1] = Convert.ToString(szam + 1);
                break;
            case "j":
                szam = Convert.ToByte(head[0]);
                head[0] = Convert.ToString(szam + 1);
                break;
            case "b":
                szam = Convert.ToByte(head[0]);
                head[0] = Convert.ToString(szam - 1);
                break;
        }

        head[0] = head[0] + ";" + head[1];
        head = head.Where(coord => coord != head[1]).ToArray();

        if (grow) grow = false;
        else snake = (snake.Where(coord => coord != snake[snake.Length - 1]).ToArray());

        snake = head.Concat(snake).ToArray();
    }


    public static string spawnPoint(byte maxX, byte maxY)
    {
        string x, y;
        while (true)
        {
            x = Convert.ToString(rand.Next(1, maxX * 2 - 2));       //A legkisebb érték 1, a legnagyobb maxX*2-3 lenne (keretek miatt)
            y = Convert.ToString(rand.Next(1, maxY - 1));         //A legkisebb érték 1, a legnagyobb maxY-2 lenne (keretek miatt)
            if (!snake.Contains(x + ";" + y)) break;
        }
        return x + ";" + y;
    }


    public static bool isSnakeAlive()
    {
        foreach (string coord in snake)
        {
            if (snake.Count(s => s == coord) > 1)
                return true;
        }
        return false;
    }
}
