using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

namespace Snake
{
    class Snake
    {
        int Heigth = 20;
        int Width = 80;

        int score = 0;

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

        public void WriteBoard()
        {
            Console.Clear();
            for(int i = 1; i <= (Width + 2); i++)
            {
                Console.SetCursorPosition(i, 1);
                Console.Write("-");
            }
            for (int i = 1; i <= (Width + 2); i++)
            {
                Console.SetCursorPosition(i, (Heigth + 2));
                Console.Write("-");
            }
            for (int i = 1; i <= (Heigth + 1); i++)
            {
                Console.SetCursorPosition(1, i);
                Console.Write("|");
            }
            for (int i = 1; i <= (Heigth + 1); i++)
            {
                Console.SetCursorPosition((Width + 2), i);
                Console.Write("|");
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
        public void WritePoint(int x, int y)
        {
            if ((x == 1 || x >= Width +1) || (y == 1 || y >= Heigth +2))
            //if (x >= Width +1 || y >= Heigth +2)
            {
                Gamelouse = true;
                Console.SetCursorPosition(90,8);
                Console.WriteLine("YOU LOUSER");
                
            }
            else
            {
                Console.SetCursorPosition(x, y);
                Console.Write("#");
            }
        }
        public void WriteScore(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.Write("SCORE: "+score);
        }

        public void Logic()
        {
            if(X[0] == fruitX)
            {
                if(Y[0] == fruitY)
                {
                    parts++;
                    fruitX = rnd.Next(2, (Width - 2));
                    fruitY = rnd.Next(2, (Heigth - 2));
                    score++;
                }
            }
            for (int i = parts;i>1; i--)
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
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

            }
            for(int i = 0; i <= (parts - 1); i++)
            {
                WritePoint(X[i], Y[i]);
                WritePoint(fruitX, fruitY);
            }
            Thread.Sleep(100);
        }
        static void Main(string[] args)
        {

            Snake snake = new Snake();
            while (snake.Gamelouse == false) {
                snake.WriteBoard();
                snake.WriteScore(90,5);
                snake.Input();
                snake.Logic();
            }
            Console.ReadKey();

        }
    }
}
