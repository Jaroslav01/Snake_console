using System;
using System.Threading;
using System.Diagnostics;
namespace Snake
{
    class Snake
    {
        Stopwatch StopWatch = new Stopwatch();
        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        Random rnd = new Random();
        string[] time_history = new string[50];
        bool gamelouse = false;
        int heigth = 20;
        int width = 80;
        int score = 0;
        int[] X = new int[50];
        int[] Y = new int[50];
        bool wall_triger = true; // запускает вибор параметров стены
        int x_wall = 0;
        int y_wall = 0;
        int direction = 0; // Направление
        int size = 0; // Длина стены
        int fruitX;
        int fruitY;
        int parts = 3;
        char key = 'W';
        Snake()
        {
            X[0] = 5;
            Y[0] = 5;
            Console.CursorVisible = false;
            fruitX = rnd.Next(2, (width - 2));
            fruitY = rnd.Next(2, (heigth - 2));
        }
        public void TheWall()
        {
            while (wall_triger == true)
            {
                wall_triger = false;
                x_wall = rnd.Next(5, width - 1);
                y_wall = rnd.Next(5, heigth - 1);
                direction = rnd.Next(0, 4);
                size = rnd.Next(5, 20);
            }
            switch (direction)
            {
                case 0:
                    for (int i = 0; i < size && x_wall + i >= heigth; i++)
                    { 
                        Draw(x_wall + i, y_wall, "#");
                    }
                    break;
                case 1:
                    for (int i = 0; i < size && x_wall - i >= heigth; i++)
                    {
                        Draw(x_wall - i, y_wall, "#");
                    }
                    break;
                case 2:
                    for (int i = 0; i < size && x_wall + i >= width; i++)
                    {
                        Draw(x_wall, y_wall + i, "#");
                    }
                    break;
                case 3:
                    for (int i = 0; i < size && x_wall - i >= width; i++)
                    {
                        Draw(x_wall, y_wall - i, "#");
                    }
                    break;
            }
        }
        public void Timer()
        {
            TimeSpan ts = StopWatch.Elapsed;
            time_history[score-1] = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
            Console.Write(String.Join(Environment.NewLine, time_history[19]));
            StopWatch.Restart();
        }
        public void TimerDrawAndScore()
        {
            TimeSpan ts = StopWatch.Elapsed;
            for (int i = 0; i < score; i++)
            {
                Draw(90, 8 + i, $"{i + 1} - {time_history[i]}");
            }
            Draw(100, 5, $"{String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10)}");
            Draw(90, 5, $"SCORE: {score}");
        }
        public void CheckTp(int x, int y) // check border touch
        {
            if (x == 0) 
            {
                X[0] = width;
            }
            else if (x == width + 1)
            {
                X[0] = 1;
            }
            else if (y == 0)
            {
                Y[0] = heigth;
            }
            else if (y == heigth + 1)
            {
                Y[0] = 1;
            }
        }
        public void MapBorder()
        {
            for (int i = 1; i <= (width + 2); i++)
            {
                Draw(i, 1, "-");
            }
            for (int i = 1; i <= (width + 2); i++)
            {
                Draw(i, (heigth + 2), "-");
            }
            for (int i = 1; i <= (heigth + 1); i++)
            {
                Draw(1, i, "|");
            }
            for (int i = 1; i <= (heigth + 1); i++)
            {
                Draw((width + 2), i, "|");
            }
        }
        public void Fruit()
        {
            if (X[0] == fruitX)
            {
                if (Y[0] == fruitY)
                {
                    parts++;
                    score++;
                    fruitX = rnd.Next(2, (width - 2));
                    fruitY = rnd.Next(2, (width - 2));
                    wall_triger = true;
                    Timer();
                }
            }
        }
        public void SnakePartsAndMove()
        {
            for (int i = parts; i > 1; i--)
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
            }
        }
        public void KeyPress()
        {
            switch (key)
            {
                case 'w':
                    Y[0]--;
                    break;
                case 's':
                    Y[0]++;
                    break;
                case 'd':
                    X[0]++;
                    break;
                case 'a':
                    X[0]--;
                    break;
                case '+':
                    parts++;
                    break;
                case '-':
                    parts--;
                    break;
            }
        }
        public void SnakeFruitTpDraw()
        {
            for (int i = 0; i <= (parts - 1); i++)
            {
                Draw(X[i], Y[i], "#");
                Draw(fruitX, fruitY, "O");
                CheckTp(X[i], Y[i]);
            }
        }
        public void Input()
        {
            if (Console.KeyAvailable)
            {
                keyInfo = Console.ReadKey(true);
                key = keyInfo.KeyChar;
            }
        }
        public void Logic()
        {
            Console.Clear();
            StopWatch.Start();
            MapBorder();
            Fruit();
            SnakePartsAndMove();
            KeyPress();
            SnakeFruitTpDraw();
            TimerDrawAndScore();
            TheWall();
            Thread.Sleep(100);
        }
        public void Draw(int x,int y,string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
        static void Main(string[] args)
        {
            Snake snake = new Snake();
            while (snake.gamelouse == false)
            {
                snake.Input();
                snake.Logic();
            }
            Console.ReadKey();
        }
    }
}