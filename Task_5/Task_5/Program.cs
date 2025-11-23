class HeapComparer<T> : IComparer<T> {
    public int Compare(T x, T y) {
        return Comparer<T>.Default.Compare(x, y);
    }
}

class BinHeap<T> {
    private T[] heap;
    private HeapComparer<T> comparer;
    private int size;
    private int capacity;

    public BinHeap(T[] a) {
        if (a == null)
            throw new Exception("Input array cannot be null");
        capacity = a.Length;
        size = a.Length;
        heap = new T[capacity];
        Array.Copy(a, heap, size);
        comparer = new HeapComparer<T>();
        int startIndex = size / 2 - 1;
        for (int i = startIndex; i >= 0; i--) Sift_down(i);
    }

    private void Resize() {
        if (capacity < 64) capacity += 2;
        else capacity = capacity + (capacity + 1)/2;
        T[] newHeap = new T[capacity];
        Array.Copy(heap, newHeap, size);
        heap = newHeap;
    }

    private void Sift_up(int i) {
        while (i > 0) {
            int parentI = (i - 1) / 2;
            if (comparer.Compare(heap[i], heap[parentI]) > 0) {
                (heap[i], heap[parentI]) = (heap[parentI], heap[i]);
                i = parentI;
            } 
            else break;
        }
    }

    private void Sift_down(int i) {
        while (2 * i + 1 < size) {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int maxChild = left;
            if (right < size && comparer.Compare(heap[right], heap[maxChild]) > 0) {
                maxChild = right;
            }
            if (comparer.Compare(heap[i], heap[maxChild]) >= 0) break;
            else {
                (heap[i], heap[maxChild]) = (heap[maxChild], heap[i]);
                i = maxChild;
            }
        }
    }

    public T Top() {
        if (size == 0) 
            throw new Exception("Heap is empty");
        return heap[0];
    }

    public void Push(T x) {
        if (size == capacity) Resize();
        heap[size] = x;
        Sift_up(size);
        size++;
    }
    
    public T Pop() {
        if (size == 0)
            throw new Exception("Heap is empty");
        T ans = heap[0];
        if (size == 1) {
            size--;
            return ans;
        }
        heap[0] = heap[size - 1];
        size--;
        Sift_down(0);
        return ans;
    }

    public void Increasing(int i, T v) {
        if (size == 0) 
            throw new Exception("Heap is empty");
        if (i < 0 || i >= size)
            throw new IndexOutOfRangeException("Invalid index");
        if (comparer.Compare(v, heap[i]) <= 0) 
            throw new Exception("Value is not ok :(");
        heap[i] = v;
        Sift_up(i);
    }

    public void MergeHeaps(BinHeap<T> other) {
        if (other == null)
            throw new Exception("Heap is empty");
        int newSize = size + other.size;
        while (capacity < newSize) Resize();
        Array.Copy(other.heap, 0, heap, size, other.size);
        size = newSize;
        int startIndex = size / 2 - 1;
        for (int i = startIndex; i >= 0; i--) {
            Sift_down(i);
        }
    }
    
    public int Count => size;
    public bool IsEmpty => size == 0;

    public void Print() {
        if (size == 0) {
            Console.WriteLine("Input array cannot be null");
            return;
        }
        int i = 0;
        int levelSize = 1;
        while (i < size) {
            for (int j = 0; j < levelSize && i < size; j++) {
                Console.Write($"{heap[i++]} ");
            }
            Console.WriteLine();
            levelSize *= 2;
        }
    }
}

public class Program {
    static void Main() {
        try {
            int[] initialArray = { 3, 1, 6, 5, 2, 4 };
            BinHeap<int> heap = new BinHeap<int>(initialArray);
            
            Console.WriteLine("Исходная куча:");
            heap.Print();
            Console.WriteLine($"Максимум: {heap.Top()}");
            Console.WriteLine();

            heap.Push(8);
            heap.Push(7);
            Console.WriteLine("После добавления 8 и 7:");
            heap.Print();
            Console.WriteLine();

            int max = heap.Pop();
            Console.WriteLine($"Удален максимум: {max}");
            heap.Print();
            Console.WriteLine();

            heap.Increasing(2, 10);
            Console.WriteLine("После увеличения элемента с индексом 2 до 10:");
            heap.Print();
            Console.WriteLine();

            BinHeap<int> otherHeap = new BinHeap<int>(new int[] { 20, 15, 25 });
            heap.MergeHeaps(otherHeap);
            Console.WriteLine("После слияния с кучей [20, 15, 25]:");
            heap.Print();
            Console.WriteLine();
        } catch (Exception ex) {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}