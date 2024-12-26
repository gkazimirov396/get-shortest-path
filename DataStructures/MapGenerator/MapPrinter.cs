namespace DataStructures
{
    public class MapPrinter
    {
        public void Print(string[,] maze, Point start, Point goal, List<Point> path)
        {
            PrintTopLine();
            for (var row = 0; row < maze.GetLength(1); row++)
            {
                Console.Write($"{row}\t");
                for (var column = 0; column < maze.GetLength(0); column++)
                {
                    var point = new Point(column, row);
                    if (point.Equals(start))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("A");
                        Console.ResetColor();
                    }
                    else if (point.Equals(goal))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkRed;
                        Console.Write("B");
                        Console.ResetColor();
                    }
                    else if (path.Contains(point))
                    {
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.Write(".");
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write(maze[column, row]);
                    }
                }

                Console.WriteLine();
            }

            void PrintTopLine()
            {
                Console.Write($" \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10 == 0 ? i / 10 : " ");
                }

                Console.Write($"\n \t");
                for (int i = 0; i < maze.GetLength(0); i++)
                {
                    Console.Write(i % 10);
                }

                Console.WriteLine("\n");
            }
        }
    }
}