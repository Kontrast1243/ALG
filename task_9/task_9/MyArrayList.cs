class MyArrayList<T> {


    private T[] elementData;
    private int size;
    private int capacity;
    
    private static readonly EqualityComparer<T> Comparer = EqualityComparer<T>.Default;

    public int Capacity => capacity;
    
    public T this[int i] {
        get {
            if (i < 0 || i >= size)
                throw new ArgumentOutOfRangeException("Индекс вне диапазона");
            return elementData[i];
        }
        set {
            if (i < 0 || i >= size)
                throw new ArgumentOutOfRangeException("Индекс вне диапазона");
            elementData[i] = value;
        }
    }

    public MyArrayList() {
        size = 0;
        capacity = 11;
        elementData = new T[capacity];
    }

    public MyArrayList(int capacity) {
        if (capacity < 1) 
            throw new ArgumentOutOfRangeException("Ёмкость должна быть больше 1");
        size = 0;
        this.capacity = capacity;
        elementData = new T[capacity];
    }

    public MyArrayList(T[] array) {
        if (array == null)
            throw new ArgumentNullException("Массива не существует");
        size = array.Length;
        capacity = array.Length+11;
        elementData = new T[capacity];
        Array.Copy(array, 0, elementData, 0, size);
    }

    private void Resize() {
        capacity =  capacity + (capacity+1)/2;
        T[] newElementData = new T[capacity];
        Array.Copy(elementData, 0, newElementData, 0, size);
        elementData = newElementData;
    }

    public void Add(T x) {
        if (size == capacity) Resize();
        elementData[size] = x; size++;
    }

    public void AddAll(T[] array) {
        if (array == null)
            throw new ArgumentNullException("Массива не существует");
        foreach (T x in array) Add(x);
    }

    public void Clear() {
        T[] newElementData = new T[capacity];
        elementData = newElementData;
        size = 0;
    }

    public bool Contains(T x) {
        for (int i = 0; i < size; i++) {
            if (Comparer.Equals(elementData[i], x)) 
                return true;
        }
        return false;
    }

    public bool ContainsAll(T[] array) {
        if (array == null)
            throw new ArgumentNullException("Массива не существует");
        foreach (T x in array) if (!this.Contains(x)) return false;
        return true;
    }

    public void Remove(T x) {
        int index = IndexOf(x); 
        if (index == -1) return;
        RemoveIndex(index);
    }

    public void RemoveAll(T[] array) {
        if (array == null)
            throw new ArgumentNullException("Массива не существует");
        foreach (T x in array) Remove(x);
    }

    public void RetainAll(T[] array) {
        for (int i = size - 1; i >= 0; i--) {
            bool ok = false;
            foreach (T y in array) {
                if (Comparer.Equals(elementData[i], y)) {
                    ok = true;
                    break;
                }
            }
            if (!ok) RemoveIndex(i);
        }
    }

    public T[] ToArray() {
        T[] ans =  new T[size];
        Array.Copy(elementData, 0, ans, 0, size);
        return ans;
    }

    public T[] ToArray(T[]? array) {
        if (array == null) return ToArray();
        Array.Copy(elementData, 0, array, 0, size);
        return array;
    }

    public void Add(int i, T x) {
        if (i < 0 || i > size)
            throw new ArgumentOutOfRangeException("Выход за пределы массива");
        if (size == capacity) Resize();
        for (int j = size; j > i; j--) {
            elementData[j] = elementData[j - 1];
        }
        elementData[i] = x;
        size++;
    }

    public void AddAll(int i, T[] array) {
        if (array == null)
            throw new ArgumentNullException("Массива не существует");
        if (i < 0 || i > size)
            throw new ArgumentOutOfRangeException("Выход за пределы массива");
    
        if (size + array.Length > capacity) {
            while (size + array.Length > capacity) {
                Resize();
            }
        }
    
        Array.Copy(elementData, i, elementData, i + array.Length, size - i);
    
        Array.Copy(array, 0, elementData, i, array.Length);
    
        size += array.Length;
    }

    public T Get(int i) {
        if (i < 0 ||  i >= size)
            throw new ArgumentOutOfRangeException("Выход за пределы массива");
        return elementData[i];
    }

    public int IndexOf(T x) {
        for (int i = 0; i < size; i++) {
            if (Comparer.Equals(elementData[i], x)) 
                return i;
        }
        return -1;
    }

    public int LastIndexOf(T x) {
        int ans = -1;
        for (int i = 0; i < size; i++) if (Comparer.Equals(elementData[i], x)) ans = i;
        return ans;
    }

    public T RemoveIndex(int i) {
        if (i < 0 ||  i >= size)
            throw new ArgumentOutOfRangeException("Выход за пределы массива");
        T ans = elementData[i];
        size--;
        for (int j = i; j < size; j++) elementData[j] = elementData[j+1];
        return ans;
    }


    public void Set(int i, T x) {
        if (i < 0 ||  i >= size)
            throw new ArgumentOutOfRangeException("Выход за пределы массива");
        elementData[i] = x;
    }

    public MyArrayList<T> SubList(int start, int end) {
        MyArrayList<T> ans = new MyArrayList<T>();
        for (int i = start; i < end; i++) ans.Add(elementData[i]);
        return ans;
    }

    
    public int Size() => size;
    public bool IsEmpty() => size == 0;

}