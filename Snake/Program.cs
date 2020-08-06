using System;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Snake
{
    class Snake
    {
    string docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
    Stopwatch StopWatch = new Stopwatch();
        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        Random rnd = new Random();
        string[] time_history = new string[50];
        bool Gamelouse = false;
        int Heigth = 20;
        int Width = 80;
        int score = 0;
        int[] X = new int[50];
        int[] Y = new int[50];
        bool wall_triger = true; // запускает вибор параметров 
        int[] x_wall = new int[6];
        int[] y_wall = new int[6];
        int[] direction = new int[2];
        int fruitX;
        int fruitY;
        int parts = 3;
        char key = 'a';
        int a = 0;
        Snake()
        {
            X[0] = 5;
            Y[0] = 5;
            Console.CursorVisible = false;
            fruitX = rnd.Next(2, (Width - 2));
            fruitY = rnd.Next(2, (Heigth - 2));
        }
        public void TheWall()
        {
            if (wall_triger == true)
            {
                TheWallDraw(" ");
                x_wall[1] = x_wall[0];
                y_wall[1] = y_wall[0];
                direction[1] = direction[0];
                wall_triger = false;
                x_wall[0] = rnd.Next(10, Width - 10);
                y_wall[0] = rnd.Next(10, Heigth - 10);
                direction[0] = rnd.Next(0, 2);
                //Console.Clear();
            }
        }
        public void TheWallDraw(string symbol)
        {
            if (x_wall[0] == x_wall[1] && y_wall[0] == y_wall[1])
            {
                x_wall[0] = x_wall[0] + 5;
                y_wall[0] = y_wall[0] + 5;
            }
            switch (direction[0])
            {
                case 0:
                    for (int i = 1; i < Heigth + 2; i++)
                    {
                        if (i > 3 && i < Heigth - 1) continue;
                        Draw(x_wall[0], i, symbol);
                    }
                    break;
                case 1:
                    for (int i = 1; i < Width + 2; i++)
                    {
                        if (i > 3 && i < Width - 1) continue;
                        Draw(i, y_wall[0], symbol);
                    }
                    break;
            }
            switch (direction[1])
            {
                case 0:
                    for (int i = 1; i < Heigth + 2; i++)
                    {
                        Draw(x_wall[1], i,symbol);
                        GameOver(1, x_wall[1], i);
                    }
                    break;
                case 1:
                    for (int i = 1; i < Width + 2; i++)
                    {
                        Draw(i, y_wall[1], symbol);
                        GameOver(1, i, y_wall[1]);
                    }
                    break;
            }
        }
        public void Timer()
        {
            TimeSpan ts = StopWatch.Elapsed;
            int score_local = score;
            time_history[score_local - 1] = String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }
        public void TimerDrawAndScore()
        {
            TimeSpan ts = StopWatch.Elapsed;
            for (int i = 0; i < a; i++)
            {
                Draw(90, 8 + i, $"{i} - {time_history[i]}");
            }
            Draw(100, 5, $"{String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10)}");
            Draw(90, 5, $"SCORE: {score}");
        }
        public void Check_TP(int x, int y) // check border touch
        {
            if (x == 0)
            {
                X[0] = Width;
            }
            else if (x == Width + 1)
            {
                X[0] = 1;
            }
            else if (y == 1)
            {
                Y[0] = Heigth;
            }
            else if (y == Heigth + 1)
            {
                Y[0] = 2;
            }
        }
        public void KeyPress()
        {
            if (key == 'w')
            {
                Y[0]--;
            }
            else if (key == 's')
            {
                Y[0]++;
            }
            else if (key == 'd')
            {
                X[0]++;
            }
            else if (key == 'a')
            {
                X[0]--;
            }
            Console.Write(key);
        }
        public void MapBorder()
        {
            for (int i = 1; i <= (Width + 2); i++)
            {
                Draw(i, 1, "-");
            }
            for (int i = 1; i <= (Width + 2); i++)
            {
                Draw(i, (Heigth + 2), "-");
            }
            for (int i = 1; i <= (Heigth + 1); i++)
            {
                Draw(1, i, "-");
            }
            for (int i = 1; i <= (Heigth + 1); i++)
            {
                Draw((Width + 2), i, "-");
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
                    a++;
                    fruitX = rnd.Next(2, (Width - 2));
                    fruitY = rnd.Next(2, (Heigth - 2));
                    TheWall();
                    wall_triger = true;
                    Timer();
                    StopWatch.Restart();
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
        public void SnakeFruitTpDraw()
        {
            for (int i = 0; i <= (parts - 1); i++)
            {
                Draw(X[i], Y[i], "#");
                Draw(X[parts-1], Y[parts-1], " ");
                Draw(fruitX, fruitY, "O");
                Check_TP(X[i], Y[i]);
            }
        }
        public void GameOver(int zerone, int x, int y)
        {
            for (int i = 3; (i < X.Length - 1 || i < Y.Length - 1); i++)
            {
                switch (zerone)
                {
                    case 0:
                        if (X[0] == X[i + 1] && Y[0] == Y[i + 1])
                        {
                            Gamelouse = true;
                        }
                        break;
                    case 1:
                        if (X[i-3] == x && Y[i-3] == y)
                        {
                            Gamelouse = true;
                        }
                        break;
                }
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
           // Console.Clear();
            StopWatch.Start();
            MapBorder();
            Fruit();
            SnakePartsAndMove();
            KeyPress();
            SnakeFruitTpDraw();
            TimerDrawAndScore();
            TheWallDraw("#");
            GameOver(0, 0, 0);
            Thread.Sleep(100);
        }
        public void Draw(int x, int y, string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
        }
        static void Main(string[] args)
        {
            Snake snake = new Snake();
            while (snake.Gamelouse == false)
            {
                snake.Input();
                snake.Logic();
            }
            Console.BackgroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("GameOver");
            Console.ReadKey();
        }
    }
}
/*
 * мерцание
 * 
 * граныцу подкоректировать
 * 
 * букву д убрать
 * 
 * максимальный рекорд в файл
 * 
 * Прочиттать про файлы
 * 
 * ооп, функцыональное, и процедурное
 * 
 * 
 */