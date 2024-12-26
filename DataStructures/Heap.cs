namespace DataStructures;

public class Heap
{
    private readonly List<Point> heap;

    public int Size => heap.Count;

    public Heap()
    {
        heap = new List<Point>();
    }

    private int Parent(int index) => (index - 1) / 2;
    private int LeftChild(int index) => 2 * index + 1;
    private int RightChild(int index) => 2 * index + 2;

    private void Swap(int i, int j)
    {
        (heap[j], heap[i]) = (heap[i], heap[j]);
    }

    private void HeapifyUp(int index)
    {
        while (index > 0 && heap[index].CompareTo(heap[Parent(index)]) < 0)
        {
            Swap(index, Parent(index));
            index = Parent(index);
        }
    }

    private void HeapifyDown(int index)
    {
        int minIndex = index;
        int leftChild = LeftChild(index);
        int rightChild = RightChild(index);

        if (leftChild < Size && heap[leftChild].CompareTo(heap[minIndex]) < 0)
        {
            minIndex = leftChild;
        }

        if (rightChild < Size && heap[rightChild].CompareTo(heap[minIndex]) < 0)
        {
            minIndex = rightChild;
        }

        if (index != minIndex)
        {
            Swap(index, minIndex);
            HeapifyDown(minIndex);
        }
    }

    public void Insert(Point value)
    {
        heap.Add(value);
        HeapifyUp(Size - 1);
    }

    public bool Contains(Point value)
    {
        return heap.Contains(value);
    }

    public bool Remove(Point value)
    {
        return heap.Remove(value);
    }

    public Point At(int index)
    {
        return heap[index];
    }

    public Point ExtractMin(Dictionary<Point, int> fScore)
    {
        if (Size == 0)
        {
            throw new Exception("Heap is empty. Cannot extract minimum element.");
        }

        int minValue = int.MaxValue;
        var curPoint = heap[0];
        foreach (var item in heap)
        {
            if (fScore[item].CompareTo(minValue) < 0)
            {
                minValue = fScore[item];
                curPoint = item;
            }
        }

        return curPoint;
    }

    public override string ToString()
    {
        string result = "";

        for (int i = 0; i < Size; i++)
        {
            result += heap[i] + " ";
        }

        return result;
    }
}