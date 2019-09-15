using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace consolegame
{
    class Program
    {

        static int x = 1, y = 1;
        static char player = '*';
        static char [][] screen =new char[13][];

        static void moveplayer(string type)
        {
            if (type == "down")
            {
                Console.SetCursorPosition(x, ++y);
                Console.Write(player);
                Console.SetCursorPosition(x, y);
            }
            else if (type == "up")
            {
                Console.SetCursorPosition(x, --y);
                Console.Write(player);
                Console.SetCursorPosition(x, y);
            }
            else if (type == "left")
            {
                Console.SetCursorPosition(--x, y);
                Console.Write(player);
                Console.SetCursorPosition(x, y);

            }
            else
            {
                Console.SetCursorPosition(++x, y);
                Console.Write(player);
                Console.SetCursorPosition(x, y);

            }

        }

        static void makeEnemy(int start , int end ,int ye , int lenght)
        {
            Random rand = new Random();
            int startMove = rand.Next(start + lenght,end-lenght);
            int xe = startMove;
            int count =1;
            char currDirection = '>';

            while (true)
            {
                if (count == lenght || count == 0)
                {
                    currDirection = currDirection == '<' ? '>' : '<';
                }

                if (currDirection == '<')
                {
                    Console.SetCursorPosition(0, 0);
                    for (int i = 0; i < 13; i++) Console.Write(new string(screen[i]));
                    Thread.Sleep(200);
                    Console.SetCursorPosition(xe, ye);
                    Console.Write(' ');
                    Console.SetCursorPosition(xe, ye);
                    Console.Write('<');
                    Console.SetCursorPosition(xe, ye);
                    xe--;
                    count--;
                    

                }
                else
                {
                    Console.SetCursorPosition(0, 0);
                    for (int i = 0; i < 13; i++) Console.Write(new string(screen[i]));
                    Thread.Sleep(200);
                    Console.SetCursorPosition(xe, ye);
                    Console.Write(' ');
                    Console.SetCursorPosition(xe, ye);
                    Console.Write('>');
                    Console.SetCursorPosition(xe, ye);
                    xe++;
                    count++;
                    

                }
            }

        }




        static void Main(string[] args)
        {


            StreamReader level = new StreamReader("level1.txt");
            List<string> levelbody = new List<string>();
            for(int i = 0; i < 13; i++)
            {
                string levelline = level.ReadLine();
                levelbody.Add(levelline);
                screen [i] = (levelline + "\n").ToCharArray();
                Console.WriteLine(levelline);

            }
            

            Console.SetCursorPosition(x, y);
            Console.Write('*');
            Console.SetCursorPosition(x, y);

            

            Thread enemy2 = new Thread(() => makeEnemy(15, 28, 2, 3));
            //enemy2.Start();





            bool haveKay =false;





            ConsoleKeyInfo ky;
            while (true)
            {
                

                ky = Console.ReadKey();
                switch (ky.Key)
                {
                    case ConsoleKey.DownArrow:
                        if (levelbody[y + 1][x] != '.' && levelbody[y + 1][x] != 'D')
                        {
                            
                            if (levelbody[y + 1][x] == 'K') haveKay = true;
                            moveplayer("down");
                        }
                        else if (levelbody[y+1][x] == 'D' && haveKay == true)
                        {
                            
                            moveplayer("down");
                            haveKay = false;
                        }
                        else
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write("*");
                            Console.SetCursorPosition(x, y);
                        }
                        break;
                    case ConsoleKey.UpArrow:
                        if (levelbody[y - 1][x] != '.')
                        {
                            
                            if (levelbody[y - 1][x] == 'K') haveKay = true;
                            moveplayer("up");
                        }
                        else
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write("*");
                            Console.SetCursorPosition(x, y);
                        }
                        break;
                    case ConsoleKey.LeftArrow:
                        if (levelbody[y][x-1] != '.')
                        {
                            
                            if (levelbody[y][x-1] == 'K') haveKay = true;
                            moveplayer("left");
                        }
                        else
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write("*");
                            Console.SetCursorPosition(x, y);
                        }
                        break;
                    case ConsoleKey.RightArrow:
                        if (levelbody[y][x+1] != '.')
                        {
                            if (levelbody[y][x+1] == 'K') haveKay = true;
                            moveplayer("right");
                        }
                        else
                        {
                            Console.SetCursorPosition(x, y);
                            Console.Write("*");
                            Console.SetCursorPosition(x, y);

                        }
                        break;
                    default:
                        Console.SetCursorPosition(x, y);

                        break;

                }

                Thread enemy1 = new Thread(() => makeEnemy(15, 28, 1, 3));
                enemy1.Start();
            }


            
         


                Console.ReadKey();

        }
    }
}
