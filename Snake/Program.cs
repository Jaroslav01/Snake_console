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
        int Heigth = 20;
        int Width = 80;
        int score = 0;
        //int HeadX = 0;
        //int HeadY = 0;
        bool Gamelouse = false;
        int[] X = new int[50];
        int[] Y = new int[50];
        int fruitX;
        int fruitY;
        int parts = 3;
        ConsoleKeyInfo keyInfo = new ConsoleKeyInfo();
        char key = 'W';
        Random rnd = new Random();
        Snake()
        {
            X[0] = 5;
            Y[0] = 5;
            Console.CursorVisible = false;
            fruitX = rnd.Next(2, (Width - 2));
            fruitY = rnd.Next(2, (Heigth - 2));
        }

        bool wall_triger = true; // запускает вибор параметров стены
        int x_wall = 0;
        int y_wall = 0;
        int direction = 0; // Направление
        int size = 0; // Длина стены
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

                        Console.SetCursorPosition(x_wall + i, y_wall);
                        Console.Write("#");
                    }
                    break;

                case 1:
                    for (int i = 0; i < size && x_wall - i >= Heigth; i++)
                    {

                        Console.SetCursorPosition(x_wall - i, y_wall);
                        Console.Write("#");
                    }
                    break;

                case 2:
                    for (int i = 0; i < size && x_wall + i >= Width; i++)
                    {

                        Console.SetCursorPosition(x_wall, y_wall + i);
                        Console.Write("#");
                    }
                    break;

                case 3:
                    for (int i = 0; i < size && x_wall - i >= Width; i++)
                    {

                        Console.SetCursorPosition(x_wall, y_wall - i);
                        Console.Write("#");
                    }
                    break;
            }
        }

        string[] time_history = new string[50];

        public void Timer()
        {
            /*
            for (int i = 3; i < parts; i++)
            {
                //Console.Write(i);
                
            } // почему?
            */
            TimeSpan ts = StopWatch.Elapsed;
            time_history[score] = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",ts.Hours, ts.Minutes, ts.Seconds,ts.Milliseconds / 10);
            Console.Write(String.Join(Environment.NewLine, time_history[19]));
        }

        public void WriteBoard()
        {
            Console.Clear();
            for (int i = 1; i <= (Width + 2); i++)
            {
                //Console.SetCursorPosition(i, 1);
                //Console.Write("-");
                draw(i, 1, "-");
            }
            for (int i = 1; i <= (Width + 2); i++)
            {
                //Console.SetCursorPosition(i, (Heigth + 2));
                //Console.Write("-");
                draw(i, (Heigth + 2), "-");
            }
            for (int i = 1; i <= (Heigth + 1); i++)
            {
                //Console.SetCursorPosition(1, i);
                //Console.Write("|");
                draw(1, i, "|");
            }
            for (int i = 1; i <= (Heigth + 1); i++)
            {
                //Console.SetCursorPosition((Width + 2), i);
                //Console.Write("|");
                draw((Width + 2), i, "|");
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

        public void Check_TP(int x, int y) // перевірка на дотик до краю
        {
            if (x == 0) // Дотик 
            {
                X[0] = Width; // Переміщення по координаті Х
            }
            else if (x == Width + 1)
            {
                X[0] = 1;
            }
            else if (y == 0)
            {
                Y[0] = Heigth; // Переміщення по координаті Y
            }
            else if (y == Heigth + 1)
            {
                Y[0] = 1;
            }
        }

        public void WritePoint_snake(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("#");
        }
        public void Logic()
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
            for (int i = parts; i > 1; i--)
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
                //X[i - 1] = HeadX;
                // Y[i - 1] = HeadY;

            }
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
            for (int i = 0; i <= (parts - 1); i++)
            {
                WritePoint_snake(X[i], Y[i]);
                draw(fruitX, fruitY,"O");
                Check_TP(X[i], Y[i]);
            }
            Thread.Sleep(100);
        }
        public void draw(int x,int y,string symbol)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(symbol);
            /*
            for (int i = 0; i < parts; i++)
            {
                Console.SetCursorPosition(90, 8 + i);
                Console.Write($"{i + 1} - {time_history[i]}");
                
            }
            */
            //Thread.Sleep(100);
        }
        static void Main(string[] args)
        {
            Snake snake = new Snake();
            while (snake.Gamelouse == false)
            {
                snake.WriteBoard();
                // в дроу
                snake.Input();
                snake.Logic();
                
                snake.draw(90, 5,$"SCORE: {snake.score}"); 
                snake.TheWall();
                //виписать елементи отрисовки в дров
            }
            Console.ReadKey();
        }
    }
}