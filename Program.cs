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

        static int x = 1, y = 1 ,levelnum=0;
        static char player = '*';
        static bool haveKay = false;
        static List<string> levelbody = new List<string>();
        static List<string> gameLevels =new List<string>();


        static void background()
        {
            Console.BackgroundColor = ConsoleColor.DarkYellow;
            Console.Clear();

            gameLevels.Add("level1.txt");
            gameLevels.Add("level2.txt");

            x = y = 1; //position of cursor.

            //read the levels file.
            StreamReader level = new StreamReader(gameLevels[levelnum]);
            string levelContent = level.ReadToEnd();
            levelbody = levelContent.Split('\n').ToList<string>();
            //coloring the game.
            foreach (var character in levelContent)
            {
                coloring(character);
                Console.Write(character);
            }

            Console.ForegroundColor = ConsoleColor.White;


        }
        static void defaultSitting()
        {
            Console.SetCursorPosition(x, y);
            Console.Write(player);
            Console.SetCursorPosition(x, y);
        }
        static void coloring(char character)
        {
            //give the element its color.
            if (character == '.') Console.ForegroundColor = ConsoleColor.Black;
            else if (character == '#') Console.ForegroundColor = ConsoleColor.DarkRed;
            else if (character == '$') Console.ForegroundColor = ConsoleColor.DarkBlue;
            else if (character == 'K') Console.ForegroundColor = ConsoleColor.DarkGreen;

        }

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
        static void checkMove(int x , int y ,string type)
        {
            if (levelbody[y][x] != '.' && levelbody[y][x] != 'D' && levelbody[y][x] != '#' && levelbody[y][x] != '$')
            {
                 if (levelbody[y][x] == 'K')
                                    haveKay = true;
                
                 moveplayer(type);
            }else if (levelbody[y][x] == 'D' && haveKay == true)
            {
                haveKay = false;
                moveplayer(type);
            }
            else if(levelbody[y][x] == '#')
                gameover();
            else if (levelbody[y][x] == '$')
                levelComplete();
            else
                defaultSitting();
        }

        static void levelComplete()
        {

            Console.Clear();
            StreamReader levelcompletebg = new StreamReader("levelcomplete.txt");
            Console.Write(levelcompletebg.ReadToEnd().Replace("@", levelnum+1.ToString()));
            ConsoleKeyInfo ky = Console.ReadKey();
            if (ky.Key == ConsoleKey.Enter)
            {
                levelnum++;
                background();
                defaultSitting();
                
            }

        }
        static void gameover()
        {
            haveKay = false;
            Console.Clear();
            StreamReader gameoverbg = new StreamReader("gameover.txt");
            string gameover = gameoverbg.ReadToEnd();
            Console.SetCursorPosition(0, 0);
            Console.Write(gameover);
            ConsoleKeyInfo ky;
            
                ky =Console.ReadKey();
                if (ky.Key == ConsoleKey.Enter)
                {
                    background();
                    defaultSitting();
                    return;
                }
                
        }


        static void Main(string[] args)
        {            
            background();
            defaultSitting();
            ConsoleKeyInfo ky;
            while (true)
            {
                ky = Console.ReadKey();
                switch (ky.Key)
                {
                    case ConsoleKey.DownArrow:
                        checkMove(x, y + 1, "down");
                        break;
                    case ConsoleKey.UpArrow:
                        checkMove(x, y - 1, "up");
                        break;
                    case ConsoleKey.LeftArrow:
                        checkMove(x-1, y, "left");
                        break;
                    case ConsoleKey.RightArrow:
                        checkMove(x + 1, y, "right");
                        break;
                    default:
                        defaultSitting();
                        break;
                }               
            }

                Console.ReadKey();

        }
    }
}
