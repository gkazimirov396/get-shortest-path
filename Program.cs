using DataStructures;

namespace Program
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new MapGeneratorOptions
            {
                Width = 30,
                Height = 16,
                Noise = 0.1f,
                AddTraffic = true,
                TrafficSeed = 12345,
                Seed = -1,
                Type = MapType.Maze
            };

            var generator = new MapGenerator(options);
            string[,] maze = generator.Generate();

            var mapPrinter = new MapPrinter();

            var solver = new AStar();
            var start = new Point(1, 1);
            var goal = new Point(24, 14);
            var path = solver.GetShortestPath(maze, start, goal);

            if (path != null)
            {
                Console.WriteLine("\nShortest Path:");
                mapPrinter.Print(maze, start, goal, path);

                int totalTime = solver.CalculateTotalTime(path, maze);
                Console.WriteLine($"\nTotal Travel Time: {totalTime} minutes");
            }
            else
            {
                Console.WriteLine("\nNo path found!");
            }
        }
    }
}