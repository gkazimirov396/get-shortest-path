using DataStructures;

namespace DataStructures
{
    public class AStar
    {
        public List<Point> GetShortestPath(string[,] maze, Point start, Point goal)
        {
            var openSet = new Heap();
            var closedSet = new Heap();

            var cameFrom = new Dictionary<Point, Point>();
            var gScore = new Dictionary<Point, int>();
            var fScore = new Dictionary<Point, int>();

            foreach (var point in GetAllPoints(maze))
            {
                gScore[point] = int.MaxValue;
                fScore[point] = int.MaxValue;
            }

            gScore[start] = 0;
            fScore[start] = CalculateHeuristic(start, goal);
            openSet.Insert(start);

            while (openSet.Size > 0)
            {
                var current = GetPointWithLowestFScore(openSet, fScore);
                if (current.Equals(goal))
                {
                    return ReconstructPath(cameFrom, current);
                }

                openSet.Remove(current);
                closedSet.Insert(current);

                var neighbors = GetNeighbors(current.Column, current.Row, maze);
                foreach (var neighbor in neighbors)
                {
                    if (closedSet.Contains(neighbor))
                        continue;

                    var tentativeGScore = gScore[current] + 1;

                    if (!openSet.Contains(neighbor))
                        openSet.Insert(neighbor);
                    else if (tentativeGScore >= gScore[neighbor])
                        continue;

                    cameFrom[neighbor] = current;
                    gScore[neighbor] = tentativeGScore;
                    fScore[neighbor] = gScore[neighbor] + CalculateHeuristic(neighbor, goal);
                }
            }

            return null;
        }

        private int CalculateHeuristic(Point a, Point b)
        {
            return Math.Abs(a.Column - b.Column) + Math.Abs(a.Row - b.Row);
        }

        private Point GetPointWithLowestFScore(Heap openSet, Dictionary<Point, int> fScore)
        {
            return openSet.ExtractMin(fScore);
        }

        private List<Point> GetAllPoints(string[,] maze)
        {
            var points = new List<Point>();
            for (int row = 0; row < maze.GetLength(1); row++)
            {
                for (int col = 0; col < maze.GetLength(0); col++)
                {
                    if (maze[col, row] == " ")
                    {
                        points.Add(new Point(col, row));
                    }
                }
            }
            return points;
        }

        private List<Point> GetNeighbors(int column, int row, string[,] maze)
        {
            var result = new List<Point>();

            TryAddWithOffset(1, 0);
            TryAddWithOffset(-1, 0);
            TryAddWithOffset(0, 1);
            TryAddWithOffset(0, -1);

            return result;

            void TryAddWithOffset(int offsetX, int offsetY)
            {
                var newColumn = column + offsetX;
                var newRow = row + offsetY;
                if (newColumn >= 0 && newRow >= 0 && newColumn < maze.GetLength(0) && newRow < maze.GetLength(1) && maze[newColumn, newRow] != "?")
                {
                    result.Add(new Point(newColumn, newRow));
                }
            }
        }

        private List<Point> ReconstructPath(Dictionary<Point, Point> cameFrom, Point current)
        {
            var path = new List<Point>();
            while (cameFrom.ContainsKey(current))
            {
                path.Add(current);
                current = cameFrom[current];
            }
            path.Reverse();
            return path;
        }

        public int CalculateTotalTime(List<Point> path, string[,] maze)
        {
            int totalTime = 0;
            foreach (var point in path)
            {
                int speedLimit = GetSpeedLimit(maze[point.Column, point.Row]);
                totalTime += 1 * 60 / speedLimit;
            }
            return totalTime;
        }

        private int GetSpeedLimit(string value)
        {
            if (int.TryParse(value, out int speed))
            {
                return 60 - (speed - 1) * 6;
            }

            return 60;
        }
    }
}