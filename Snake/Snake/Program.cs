using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    class Program
    {
        const int RIGHT = 1;
        const int UP = 2;
        const int LEFT = 3;
        const int DOWN = 4;

        static int len = 0;
        static int[] snakeX = new int[200];
        static int[] snakeY = new int[200];
        static int Dir = UP;
        static int HookX, HookY;

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            InitSnake();
            CreateHook();
            while (true)
            {
                if (HookHit())
                {
                    Console.Beep();
                    IncreaseSnakeLen();
                    CreateHook();
                }
                DrawSnake();
                Thread.Sleep(100);
                ChangeDir();
                MoveSnake();
                DrawHook();
                if (GameOver())
                {
                    ConsoleKeyInfo k = GameOverPrompt();
                    if (k.Key == ConsoleKey.Y)
                    {
                        Console.Clear();
                        InitSnake();
                        CreateHook();
                    }
                    else
                    {
                        return;
                    }
                }
            }
        }

        static ConsoleKeyInfo GameOverPrompt()
        {
            Console.Beep();
            Console.SetCursorPosition(20, 10);
            Console.Write("game over");
            Console.SetCursorPosition(20, 11);
            Console.Write("do you want to play again?y/n");
            ConsoleKeyInfo k = Console.ReadKey();
            return k;
        }

        static bool GameOver()
        {
            for (int i = 1; i < len; i++)
            {
                if (snakeX[i] == snakeX[0] && snakeY[i] == snakeY[0]) return true;
            }
            return false;
        }

        static bool HookHit()
        {
            for (int i = 0; i < len; i++)
            {
                if (snakeX[i] == HookX && snakeY[i] == HookY) return true;
            }
            return false;
        }

        static void DrawHook()
        {
            Console.SetCursorPosition(HookX, HookY);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("■");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void CreateHook()
        {
            Random r = new Random();
            HookX = r.Next(0, Console.WindowWidth);
            HookY = r.Next(0, Console.WindowHeight);
        }

        static void IncreaseSnakeLen()
        {
            snakeX[len] = snakeX[len - 1];
            snakeY[len] = snakeY[len - 1];
            len++;
        }

        static void InitSnake()
        {
            snakeX[5] = 1; snakeY[5] = 0;
            snakeX[4] = 2; snakeY[4] = 0;
            snakeX[3] = 3; snakeY[3] = 0;
            snakeX[2] = 4; snakeY[2] = 0;
            snakeX[1] = 5; snakeY[1] = 0;
            snakeX[0] = 6; snakeY[0] = 0;
            len = 10; Dir = RIGHT;
        }

        static void DrawSnake()
        {
            for (int i = 0; i < len; i++)
            {
                Console.SetCursorPosition(snakeX[i], snakeY[i]);
                if (i == 0) Console.Write("▓");
                else Console.Write("█");
            }
        }

        static void ChangeDir()
        {
            if (Console.KeyAvailable == false) return;

            ConsoleKeyInfo k = Console.ReadKey();

            if (k.Key == ConsoleKey.UpArrow && Dir != DOWN)
                Dir = UP;
            if (k.Key == ConsoleKey.DownArrow && Dir != UP)
                Dir = DOWN;
            if (k.Key == ConsoleKey.LeftArrow && Dir != RIGHT)
                Dir = LEFT;
            if (k.Key == ConsoleKey.RightArrow && Dir != LEFT)
                Dir = RIGHT;
        }

        static void MoveSnake()
        {
            Console.SetCursorPosition(snakeX[len - 1], snakeY[len - 1]);

            Console.Write(" ");

            for (int i = len - 1; i > 0; i--)
            {
                snakeX[i] = snakeX[i - 1];
                snakeY[i] = snakeY[i - 1];
            }
            if (Dir == UP)
            {
                if (snakeY[0] > 0)
                    snakeY[0]--;
                else
                    snakeY[0] = Console.WindowHeight - 1;
            }

            if (Dir == DOWN)
            {
                if (snakeY[0] < Console.WindowHeight - 1)
                    snakeY[0]++;
                else
                    snakeY[0] = 0;
            }
            if (Dir == RIGHT)
            {
                if (snakeX[0] < Console.WindowWidth - 1)
                    snakeX[0]++;
                else
                    snakeX[0] = 0;
            }

            if (Dir == LEFT)
            {
                if (snakeX[0] > 0)
                    snakeX[0]--;
                else
                    snakeX[0] = Console.WindowWidth - 1;
            }
        }
    }
}
