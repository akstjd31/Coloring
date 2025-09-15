using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MiniProject
{
    internal class Program
    {
        #region define values
        // 현 x, y
        public static int x = 0;
        public static int y = 0;

        // 가로, 세로 이동 제한
        const int MAX_WIDTH = 100;
        const int MAX_HEIGHT = 25;

        public static bool isGameOn = true;

        public static string currentColorName = "White";

        #endregion

        #region PrintMain func
        // 메인화면 출력
        private static void PrintMain()
        {
            Console.Write("---------------------- 재밌는 ");
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("색");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("칠");
            Console.ResetColor();
            Console.WriteLine(" 놀이 ----------------------");

            
            Console.Write("|  1. ");
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write("색칠하기 !");
            Console.ResetColor();
            Console.WriteLine("                                              |");

            Console.Write("|  2. ");
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("놀이방법");
            Console.ResetColor();
            Console.WriteLine("                                                |");

            Console.WriteLine("|  3. 종료                                                    |");
            Console.WriteLine("|                                                             |");
            Console.WriteLine("---------------------------------------------------------------");
        }
        #endregion

        #region PrintColorIndex func
        // 색 인덱스 출력
        private static void PrintColorIndex()
        {
            int i = 0;
            int j = 0;
            int num = 0;
            
            foreach (ConsoleColor color in Enum.GetValues(typeof(ConsoleColor)))
            {
                Console.ForegroundColor = color;
                Console.SetCursorPosition(j, i);
                Console.Write($"{num}. " + color);

                // 색 초기화
                Console.ResetColor();

                // 10가지 색만 쓸 수 있음.
                if (num == 10) break;
                i++; num++;

                if (i % 2 == 0)
                {
                    j+= 15; i = 0;
                }
            }

            Console.SetCursorPosition(j, i+3);
            Console.WriteLine("\n------------------------------------------------------------------------------");

            y = i + 5;
        }
        #endregion

        #region Explain func
        // 게임 방법
        private static void Explain()
        {
            
        }
        #endregion

        #region Move func

        // 1번 입력(게임시작) 시 이동할 기능
        private static void Move(ConsoleKeyInfo keyInfo, ref int x, ref int y)
        {
            switch (keyInfo.Key)
            {
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    x += 2;
                    if (x >= MAX_WIDTH) x = MAX_WIDTH;

                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    x -= 2;
                    if (x < 0) x = 0;

                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    y += 1;
                    if (y >= MAX_HEIGHT) y = MAX_HEIGHT;

                    break;
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    y -= 1;
                    if (y < 5) y = 5;
                    
                    break;
            }

            Console.SetCursorPosition(x, y);
            Console.Write("@");
        }
        #endregion

        static void Main(string[] args)
        {
            ConsoleKeyInfo keyInfo;

            ConsoleColor[] colors = (ConsoleColor[])Enum.GetValues(typeof(ConsoleColor));

            while (isGameOn)
            {
                PrintMain();

                Console.Write("입력: ");
                int input1 = int.Parse(Console.ReadLine());

                // 색칠
                if (input1 == 1)
                {
                    Console.Clear();

                    Console.SetCursorPosition(Console.WindowWidth - 1, Console.WindowHeight - 2);
                    Console.WriteLine(" Q 입력 시 종료");

                    // 색 인덱스 출력
                    PrintColorIndex();

                    // 위치 재설정
                    Console.SetCursorPosition(x, y);

                    while (isGameOn)
                    {
                        keyInfo = Console.ReadKey(true);
                        
                        // 탈출
                        if (keyInfo.Key == ConsoleKey.Q)
                        {
                            Console.ResetColor();
                            isGameOn = false;
                            break;
                        }

                        // F열에 색 매핑
                        if (ConsoleKey.F1 <= keyInfo.Key && keyInfo.Key <= ConsoleKey.F10)
                        {
                            Console.ForegroundColor = colors[keyInfo.Key - ConsoleKey.F1 + 1];
                            currentColorName = colors[keyInfo.Key - ConsoleKey.F1 + 1].ToString();
                        }
                            
                        // 이동
                        Move(keyInfo, ref x, ref y);
                    }
                }

                // 방법 설명
                else if (input1 == 2)
                {
                    Console.WriteLine("1. 게임 시작 후 W A S D나 화살표로 이동 가능.\n" +
                                      "2. 색상표를 참고하여 F1 ~ F10열을 눌러 색 변경 가능.\n" +
                                      "(q누르면 뒤로 가기)");

                    ConsoleKeyInfo inputKey = Console.ReadKey(true);

                    switch (inputKey.Key)
                    {
                        case ConsoleKey.Q:
                            Console.WriteLine();
                            continue;
                        default:
                            isGameOn = false;
                            Console.WriteLine("다른 키가 입력되었습니다.");
                            break;
                        
                    }
                }

                // 탈출
                else if (input1 == 3)
                {
                    isGameOn = false;
                    Console.WriteLine("프로그램 종료");
                    break;
                }
                else
                {
                    Console.WriteLine("잘못 입력");
                    continue;
                }
            }
        }
    }
}
