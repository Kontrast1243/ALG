class MyPriorityQueue<T> {
    private T[] queue;
    private IComparer<T> comparer;
    private int size;
    private int capacity;

    public MyPriorityQueue() {
        capacity = 11;
        size = 0;
        queue = new T[capacity];
        comparer = Comparer<T>.Default;
    }

    public MyPriorityQueue(T[] a) {
        if (a == null)
            throw new Exception("Input array cannot be null");
        capacity = a.Length;
        size = a.Length;
        queue = new T[capacity];
        Array.Copy(a, queue, size);
        comparer = Comparer<T>.Default;
        BuildBinHeap();
    }

    public MyPriorityQueue(int initialCapacity) {
        if (initialCapacity < 0)
            throw new Exception("Initial capacity cannot be negative");
        capacity = initialCapacity;
        size = 0;
        queue = new T[capacity];
        comparer = Comparer<T>.Default;
    }

    public MyPriorityQueue(int initialCapacity, IComparer<T> comparator) {
        if (initialCapacity < 0)
            throw new Exception("Initial capacity cannot be negative");
        
        capacity = initialCapacity;
        size = 0;
        queue = new T[capacity];
        comparer = comparator ?? Comparer<T>.Default;
    }

    public MyPriorityQueue(MyPriorityQueue<T> other) {
        if (other == null)
            throw new Exception("Other queue cannot be null");
        capacity = other.capacity;
        size = other.size;
        queue = new T[capacity];
        Array.Copy(other.queue, queue, size);
        comparer = other.comparer;
    }

    private void BuildBinHeap() {
        int startIndex = size / 2 - 1;
        for (int i = startIndex; i >= 0; i--) {
            Sift_down(i);
        }
    }

    private void Resize() {
        if (capacity < 64) capacity += 2;
        else capacity = capacity + (capacity + 1)/2;
        T[] newQueue = new T[capacity];
        Array.Copy(queue, newQueue, size);
        queue = newQueue;
    }

    private void Sift_up(int i) {
        while (i > 0) {
            int parentI = (i - 1) / 2;
            if (comparer.Compare(queue[i], queue[parentI]) > 0) {
                (queue[i], queue[parentI]) = (queue[parentI], queue[i]);
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
            if (right < size && comparer.Compare(queue[right], queue[maxChild]) > 0) {
                maxChild = right;
            }
            if (comparer.Compare(queue[i], queue[maxChild]) >= 0) break;
            else {
                (queue[i], queue[maxChild]) = (queue[maxChild], queue[i]);
                i = maxChild;
            }
        }
    }

    public void Add(T x) {
        if (size == capacity) Resize();
        queue[size] = x;
        Sift_up(size);
        size++;
    }

    public void AddAll(T[] a) {
        if (a == null)
            throw new Exception("Input array cannot be null");
        while (capacity < size + a.Length) Resize();
        Array.Copy(a, 0, queue, size, a.Length);
        size += a.Length;
        BuildBinHeap();
    }

    public void Clear() {
        queue = new T[capacity]; 
        size = 0;
    }

    public bool Contains(T x) {
        for (int i = 0; i < size; i++) 
            if (comparer.Compare(queue[i], x) == 0) 
                return true;
        return false;
    }

    public bool ContainsAll(T[] a) {
        foreach (T i in a) 
            if (!Contains(i)) 
                return false;
        return true;
    }

    public bool Remove(T x) {
        for (int i = 0; i < size; i++) {
            if (comparer.Compare(queue[i], x) == 0) {
                queue[i] = queue[size-1];
                size--;
                Sift_down(i);
                Sift_up(i);
                return true;
            }
        }
        return false;
    }

    public void RemoveAll(T[] a) {
        foreach (T i in a) Remove(i);
    }

    public void RetainAll(T[] a) {
        if  (a == null)
            throw new Exception("Input array cannot be null");
        T[] newQueue = new T[capacity];
        int i = 0;
        foreach (T x in a)
            if (Contains(x))
                newQueue[i++] = x;
        queue = newQueue; size = i;
        BuildBinHeap();
    }

    public T[] ToArray() {
        T[] a = new T[size];
        Array.Copy(queue, a, size);
        return a;
    }

    public void ToArray(T[] a) {
        Array.Copy(queue, a, size);
    }

    public T Element() {
        if (size == 0) 
            throw new Exception("Queue is empty");
        return queue[0];
    }

    public T? Peek() {
        if (size == 0) return default(T);
        return queue[0];
    }
    
    public T? Poll() {
        if (size == 0)
            return default(T);
        T ans = queue[0];
        if (size == 1) {
            size--;
            return ans;
        }
        queue[0] = queue[size - 1];
        size--;
        Sift_down(0);
        return ans;
    }

    public bool Offer(T x) {
        try {
            Add(x);
            return true;
        }
        catch {
            return false;
        };
    }

    public void Increasing(int i, T v) {
        if (size == 0) 
            throw new Exception("Queue is empty");
        if (i < 0 || i >= size)
            throw new IndexOutOfRangeException("Invalid index");
        if (comparer.Compare(v, queue[i]) <= 0) 
            throw new Exception("Value is not ok :(");
        queue[i] = v;
        Sift_up(i);
    }

    public void MergeHeaps(MyPriorityQueue<T> other) {
        if (other == null)
            throw new Exception("Queue is empty");
        int newSize = size + other.size;
        while (capacity < newSize) Resize();
        Array.Copy(other.queue, 0, queue, size, other.size);
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
                Console.Write($"{queue[i++]} ");
            }
            Console.WriteLine();
            levelSize *= 2;
        }
    }
}

public class Program {
    static void Main() {
        try {
            
            int[] initialData = { 15, 10, 20, 5, 12, 8, 25 };
            MyPriorityQueue<int> queue = new MyPriorityQueue<int>(initialData);
            
            Console.WriteLine("Исходная куча:");
            queue.Print();
            Console.WriteLine($"Вершина: {queue.Peek()}, Размер: {queue.Count}\n");

            queue.Add(30);
            queue.Add(3);
            Console.WriteLine("После добавления 30 и 3:");
            queue.Print();
            Console.WriteLine($"Вершина: {queue.Peek()}, Размер: {queue.Count}\n");

            Console.WriteLine("Извлечение элементов:");
            while (!queue.IsEmpty) {
                Console.WriteLine($"Извлечен: {queue.Poll()}, Осталось: {queue.Count}");
            }

            Console.WriteLine("\nМин-куча с кастомным компаратором:");
            var minHeap = new MyPriorityQueue<int>(10, Comparer<int>.Create((x, y) => y.CompareTo(x)));
            int[] a = { 15, 10, 20, 5, 12 };
            minHeap.AddAll(a);
            minHeap.Print();
            Console.WriteLine($"Минимум: {minHeap.Peek()}\n");

            Console.WriteLine("Тест операций поиска и удаления:");
            MyPriorityQueue<string> strQueue = new MyPriorityQueue<string>();
            strQueue.Add("apple");
            strQueue.Add("banana");
            strQueue.Add("cherry");
            strQueue.Add("date");
            
            Console.WriteLine($"Содержит 'banana': {strQueue.Contains("banana")}");
            Console.WriteLine($"Удален 'banana': {strQueue.Remove("banana")}");
            strQueue.Print();
            

        } catch (Exception ex) {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
    }
}