class MyVector<T> {


    private T[] elementData;
    private int elementCount;
    private int capacity;
    private int capacityIncrement = 0;
    
    private static readonly EqualityComparer<T> Comparer = EqualityComparer<T>.Default;

    public int Capacity => capacity;
    
    public T this[int i] {
        get {
            if (i < 0 || i >= elementCount)
                throw new ArgumentOutOfRangeException("Индекс вне диапазона");
            return elementData[i];
        }
        set {
            if (i < 0 || i >= elementCount)
                throw new ArgumentOutOfRangeException("Индекс вне диапазона");
            elementData[i] = value;
        }
    }

    public MyVector() {
        elementCount = 0;
        capacity = 10;
        elementData = new T[capacity];
    }

    public MyVector(int capacity, int capacityIncrement) {
        if (capacity < 1) 
            throw new ArgumentOutOfRangeException("Ёмкость должна быть больше 1");
        if (capacityIncrement < 1)
            throw new ArgumentOutOfRangeException("Значение приращения ёмкости должно быть неотрицательным");
        elementCount = 0;
        this.capacity = capacity;
        this.capacityIncrement = capacityIncrement;
        elementData = new T[capacity];
    }

    public MyVector(T[] array) {
        if (array == null)
            throw new ArgumentNullException("Массива не существует");
        elementCount = array.Length;
        capacity = array.Length+11;
        elementData = new T[capacity];
        Array.Copy(array, 0, elementData, 0, elementCount);
    }

    private void Resize() {
        if (capacityIncrement == 0) capacity *= 2;
        else capacity += capacityIncrement;
        T[] newElementData = new T[capacity];
        Array.Copy(elementData, 0, newElementData, 0, elementCount);
        elementData = newElementData;
    }

    public void Add(T x) {
        if (elementCount == capacity) Resize();
        elementData[elementCount] = x; elementCount++;
    }

    public void AddAll(T[] array) {
        if (array == null)
            throw new ArgumentNullException("Массива не существует");
        foreach (T x in array) Add(x);
    }

    public void Clear() {
        T[] newElementData = new T[capacity];
        elementData = newElementData;
        elementCount = 0;
    }

    public bool Contains(T x) {
        for (int i = 0; i < elementCount; i++) {
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
        for (int i = elementCount - 1; i >= 0; i--) {
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
        T[] ans =  new T[elementCount];
        Array.Copy(elementData, 0, ans, 0, elementCount);
        return ans;
    }

    public T[] ToArray(T[]? array) {
        if (array == null) return ToArray();
        Array.Copy(elementData, 0, array, 0, elementCount);
        return array;
    }

    public void Add(int i, T x) {
        if (i < 0 || i > elementCount)
            throw new ArgumentOutOfRangeException("Выход за пределы массива");
        if (elementCount == capacity) Resize();
        for (int j = elementCount; j > i; j--) {
            elementData[j] = elementData[j - 1];
        }
        elementData[i] = x;
        elementCount++;
    }

    public void AddAll(int i, T[] array) {
        if (array == null)
            throw new ArgumentNullException("Массива не существует");
        if (i < 0 || i > elementCount)
            throw new ArgumentOutOfRangeException("Выход за пределы массива");
    
        if (elementCount + array.Length > capacity) {
            while (elementCount + array.Length > capacity) {
                Resize();
            }
        }
    
        Array.Copy(elementData, i, elementData, i + array.Length, elementCount - i);
    
        Array.Copy(array, 0, elementData, i, array.Length);
    
        elementCount += array.Length;
    }

    public T Get(int i) {
        if (i < 0 ||  i >= elementCount)
            throw new ArgumentOutOfRangeException("Выход за пределы массива");
        return elementData[i];
    }

    public int IndexOf(T x) {
        for (int i = 0; i < elementCount; i++) {
            if (Comparer.Equals(elementData[i], x)) 
                return i;
        }
        return -1;
    }

    public int LastIndexOf(T x) {
        int ans = -1;
        for (int i = 0; i < elementCount; i++) if (Comparer.Equals(elementData[i], x)) ans = i;
        return ans;
    }

    public T RemoveIndex(int i) {
        if (i < 0 ||  i >= elementCount)
            throw new ArgumentOutOfRangeException("Выход за пределы массива");
        T ans = elementData[i];
        elementCount--;
        for (int j = i; j < elementCount; j++) elementData[j] = elementData[j+1];
        return ans;
    }


    public void Set(int i, T x) {
        if (i < 0 ||  i >= elementCount)
            throw new ArgumentOutOfRangeException("Выход за пределы массива");
        elementData[i] = x;
    }

    public MyVector<T> SubList(int start, int end) {
        MyVector<T> ans = new MyVector<T>();
        for (int i = start; i < end; i++) ans.Add(elementData[i]);
        return ans;
    }

    public T FirstElement() {
        if (elementCount == 0)
            throw new Exception("Массив пуст");
        return elementData[0];
    }

    public T LastElement() {
        if (elementCount == 0)
            throw new Exception("Массив пуст");
        return elementData[elementCount - 1];
    }

    public void RemoveElementAt(int i) {
        RemoveIndex(i);
    }

    public void RemoveRange(int start, int end) {
        if (start < 0 || start >= elementCount || end < 0 || end >= elementCount || start > end)
            throw new ArgumentOutOfRangeException("Некоректные индексы");
        if (start == end)
            return;
        int countToRemove = end - start;
    
        for (int i = end; i < elementCount; i++) {
            elementData[start + (i - end)] = elementData[i];
        }
        
        elementCount -=  countToRemove;
    }

    
    public int Size() => elementCount;
    public bool IsEmpty() => elementCount == 0;

}