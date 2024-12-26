namespace DataStructures
{
    public struct Point : IComparable<Point>
    {
        public int Column { get; }
        public int Row { get; }

        public Point(int column, int row)
        {
            Column = column;
            Row = row;
        }

        public int CompareTo(Point other)
        {
            int columnComparison = Column.CompareTo(other.Column);

            if (columnComparison == 0)
            {
                return Row.CompareTo(other.Row);
            }

            return columnComparison;
        }
    }
}