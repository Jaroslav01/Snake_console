using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Globalization;
using System.Collections.Generic;
using System.Diagnostics;
namespace Snake
{
    class Snake
    {
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
            fruitX = rnd.Next(2, (Width - 2));
            fruitY = rnd.Next(2, (Heigth - 2));
        }
        public void TheWall()
        {
            while (wall_triger == true)
            {
                wall_triger = false;
                x_wall = rnd.Next(5, Width - 1);
                y_wall = rnd.Next(5, Heigth - 1);
                direction = rnd.Next(0, 4);
                size = rnd.Next(5, 20);
            }
            switch (direction)
            {
                case 0:
                    for (int i = 0; i < size && x_wall + i >= Heigth; i++)
                    { 
                        draw(x_wall + i, y_wall, "#");
                    }
                    break;
                case 1:
                    for (int i = 0; i < size && x_wall - i >= Heigth; i++)
                    {
                        draw(x_wall - i, y_wall, "#");
                    }
                    break;
                case 2:
                    for (int i = 0; i < size && x_wall + i >= Width; i++)
                    {
                        draw(x_wall, y_wall + i, "#");
                    }
                    break;
                case 3:
                    for (int i = 0; i < size && x_wall - i >= Width; i++)
                    {
                        draw(x_wall, y_wall - i, "#");
                    }
                    break;
            }
        }
        public void Timer()
        {
            TimeSpan ts = StopWatch.Elapsed;
            time_history[score-1] = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
            Console.Write(String.Join(Environment.NewLine, time_history[19]));
        }
        public void Timer_draw_and_score() 
        {
            TimeSpan ts = StopWatch.Elapsed;
            for (int i = 0; i < score; i++)
            {
                draw(90, 8 + i, $"{i + 1} - {time_history[i]}");
            }
            draw(100, 5, $"{String.Format("{0:00}:{1:00}:{2:00}.{3:00}", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10)}");
            draw(90, 5, $"SCORE: {score}");
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
            else if (y == 0)
            {
                Y[0] = Heigth;
            }
            else if (y == Heigth + 1)
            {
                Y[0] = 1;
            }
        }
        public void Key_press()
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
        public void Map_border()
        {
            for (int i = 1; i <= (Width + 2); i++)
            {
                draw(i, 1, "-");
            }
            for (int i = 1; i <= (Width + 2); i++)
            {
                draw(i, (Heigth + 2), "-");
            }
            for (int i = 1; i <= (Heigth + 1); i++)
            {
                draw(1, i, "|");
            }
            for (int i = 1; i <= (Heigth + 1); i++)
            {
                draw((Width + 2), i, "|");
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
                    fruitX = rnd.Next(2, (Width - 2));
                    fruitY = rnd.Next(2, (Heigth - 2));
                    wall_triger = true;
                    Timer();
                    StopWatch.Restart();
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
        public void Snake_parts_and_move()
        {
            for (int i = parts; i > 1; i--)
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
            }
        }
        public void Snake_fruit_TP_draw()
        {
            for (int i = 0; i <= (parts - 1); i++)
            {
                draw(X[i], Y[i], "#");
                draw(fruitX, fruitY, "O");
                Check_TP(X[i], Y[i]);
            }
        }
        public void Logic()
        {
            Console.Clear();
            StopWatch.Start();
            Map_border();
            Fruit();
            Snake_parts_and_move();
            Key_press();
            Snake_fruit_TP_draw();
            Timer_draw_and_score();
            Thread.Sleep(100);
        }
        public void draw(int x,int y,string symbol)
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
                snake.TheWall();
            }
            Console.ReadKey();
        }
    }
}