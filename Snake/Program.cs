using System;
using System.Threading;
using System.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Data.SqlClient;
using System.Data;
using System.Text;
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
        char key = 'a'; 
        int a = 0;

        int last_x_wall = 10;
        int last_y_wall = 10;
        int last_direction = 1;
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
                wall_triger = false;
                x_wall = rnd.Next(10, Width - 10);
                y_wall = rnd.Next(10, Heigth - 10);
                direction = rnd.Next(0, 2);
                size = rnd.Next(5, 20);
            }
            if (score%2==0)
            {
                last_x_wall = x_wall;
                last_y_wall = y_wall;
                last_direction = direction;
            }
        }
        public void TheWallDraw() {  
            switch (direction)
            {
                case 0:
                    for (int i = 1; i < Heigth+2; i++)
                    {
                        Draw(x_wall, i, "#");
                        GameOver(1, x_wall,i);

                    }
                    break;
                case 1:
                    for (int i = 1; i < Width+2; i++)
                    {
                        Draw(i, y_wall, "#");
                        GameOver(1, i, y_wall);
                    }
                    break;
            }
            switch (last_direction)
            {
                case 0:
                    for (int i = 1; i < Heigth + 2; i++)
                    {
                        Draw(last_x_wall, i, "#");
                        GameOver(1, last_x_wall, i);

                    }
                    break;
                case 1:
                    for (int i = 1; i < Width + 2; i++)
                    {
                        Draw(i, last_y_wall, "#");
                        GameOver(1, i, last_y_wall);
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
                    //if (i == 2) a = 1;
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
            else if (y == 0)
            {
                Y[0] = Heigth;
            }
            else if (y == Heigth + 1)
            {
                Y[0] = 1;
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
                Draw(fruitX, fruitY, "O");
                Check_TP(X[i], Y[i]);
            }
        }
        public void GameOver(int zerone,int x, int y)
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
                        if (X[0] == x && Y[0] == y)
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
            Console.Clear();
            StopWatch.Start();
            MapBorder();
            Fruit();
            SnakePartsAndMove();
            KeyPress();
            SnakeFruitTpDraw();
            TimerDrawAndScore();
            TheWallDraw();
            GameOver(0,0,0);
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
            Console.WriteLine("GameOver");
            Console.ReadKey();
        }
    }
}
/*
 *  убрат мерцание
 *  
 *  Добавить блокировку розворота на 180
 *  TheWall в отрисовку TimerDrawAndScore();
 *  
 *  
 *  показивать где будет новая стена что  б игрок убрал хвост змеи из под стени иначе змею розрубит(
 *  добавить монетки которые убирают стену
и даються 5шт в день

 */