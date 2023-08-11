// See https://aka.ms/new-console-template for more information

using System.Reflection.Metadata;
using System.Runtime.CompilerServices;

namespace GameContent
{
    public class Maze
    {
        private int height { get; set; }

        private int width { get; set; }

        private Random rnd = new Random();

        private int[,] maze;
        public int[,] visited;

        public Maze(int height, int width)
        {
            this.height = height;
            this.width = width;
            maze = new int[height, width];
            visited = new int[height, width];
            mazeData();
        }

        public bool canMoveTo(int x, int y)
        {
            if (x >= height || y >= width) return true;
            return maze[x, y] == 1;
        }

        private void mazeData()
        {
            /*
             * Maze Data is created by randomly selecting from three direction; up, down, right and once it reaches end we 
             * end it and fill the filler data
             */


            int x = 0;
            int y = 0;

            // Maze Correct Path generation
            while (x < height && y < width)
            {
                maze[x, y] = 1;
                int chance = rnd.Next(0, 6);

                switch (chance)
                {
                    case 1:
                        x++;
                        break;
                    case 2:
                        y++;
                        break;
                    case 3:
                        x--;
                        break;
                    case 4:
                        x++;
                        break;
                    case 5:
                        x--;
                        break;

                    default:
                        break;
                }

                if (x < 0) { x = 0; }
                if (y < 0) {  y = 0; }
            }

            // filling rest data

            for (int i = 0; i < height-1; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (maze[i, j] != 1)
                    {
                        if (rnd.Next(0, 2) == 1)
                        {
                            maze[i, j] = 1;
                        }
                        else
                        {
                            maze[i, j] = 0;
                        }
                    }
                }
            }

        }

        public void printMaze(int y, int x)
        {
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    if (i == y && j == x)
                    {
                        Console.Write("*");
                     
                    } else if (visited[i, j] == 1)
                    {
                        if (maze[i, j] == 0)
                        {
                            Console.Write('|');
                        }

                        else
                        {
                            Console.Write('_');
                        }

                    } else
                    {
                        Console.Write("@");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}


namespace Game
{
    
    public class MainClass
    {
        public static void Main(string[] args)
        {
            int height = Console.WindowHeight - 10;
            int width = Console.WindowWidth - 10;
            Console.WriteLine("Welcome to Maze Game !");
            Console.WriteLine("w -> Move up\nd -> Move Right\ns -> Move down\na -> Move Left\n Enter -> Give Input\n");
            GameContent.Maze maze = new(height, width);
            maze.printMaze(0, 0);
            int x = 0;
            int y = 0;
            int prevY = 0, prevX = 0;
            

            Console.SetCursorPosition(x, y);

            // Game Loop

            while (true)
            {
                if (x >= width || y >= height)
                {
                    Console.WriteLine("Escaped");
                    break;
                }

                string? move = Console.ReadLine();


                if (move != "")
                {

                    if (move[move.Length-1] == 'w')
                    {
                        y--;

                    }
                    else if (move[move.Length - 1] == 's')
                    {

                        y++;

                    }
                    else if (move[move.Length - 1] == 'd')
                    {
                        x++;
                    }
                    else if (move[move.Length - 1] == 'a')
                    {

                        x--;
                    }

                    int vx = x;
                    int vy = y;

                    if (vx < 0)
                    {
                        vx = 0;
                    }
                    else if (vx >= width - 1)
                    {
                        vx = width - 1;
                    }

                    if (vy < 0)
                    {
                        vy = 0;
                    }

                    else if (vy >= height - 1)
                    {
                        vy = height - 1;
                    }


                    try
                    {
                        maze.visited[vy, vx] = 1;
                        maze.visited[vy, vx + 1] = 1;
                        maze.visited[vy + 1, vx] = 1;

                        if (maze.canMoveTo(vy, vx))
                        {

                            Console.Clear();
                            maze.printMaze(vy, vx);
                            prevX = vx;
                            prevY = vy;

                        }
                        else
                        {
                            Console.Clear();
                            maze.printMaze(prevY, prevX);
                            x = prevX; y = prevY;
                        }
                    }
                    catch (IndexOutOfRangeException e)
                    {
                        
                        Console.WriteLine("Escaped");
                        break;
                    }
                } else
                {
                    Console.Clear();
                    maze.printMaze(prevY, prevX);
                }
                
            }

            Console.ReadKey();

        }
    }
}
