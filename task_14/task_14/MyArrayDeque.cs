using System;
using System.Collections.Generic;

class MyArrayDeque<T> {
    private T[] elements;
    private int head;
    private int tail;
    private int size;

    private static readonly EqualityComparer<T> Comparer = EqualityComparer<T>.Default;

    public MyArrayDeque() {
        elements = new T[16];
        head = 0;
        tail = 0;
        size = 0;
    }

    public MyArrayDeque(T[] array) {
        if (array == null) {
            throw new ArgumentNullException(nameof(array), "Массива не существует");
        }

        elements = new T[Math.Max(array.Length, 16)];
        Array.Copy(array, elements, array.Length);
        head = 0;
        tail = array.Length;
        size = array.Length;
    }

    public MyArrayDeque(int capacity) {
        if (capacity <= 0) {
            throw new ArgumentException("Ёмкость должна быть положительной", nameof(capacity));
        }

        elements = new T[capacity];
        head = 0;
        tail = 0;
        size = 0;
    }

    private void Resize() {
        T[] newElements = new T[elements.Length * 2];
        for (int i = 0; i < size; i++) {
            newElements[i] = elements[(head + i) % elements.Length];
        }

        elements = newElements;
        head = 0;
        tail = size;
    }

    public void Add(T e) => AddLast(e);

    public void AddLast(T x) {
        if (size == elements.Length) Resize();
        elements[tail] = x;
        tail = (tail + 1) % elements.Length;
        size++;
    }

    public void AddFirst(T x) {
        if (size == elements.Length) Resize();
        head = (head - 1 + elements.Length) % elements.Length;
        elements[head] = x;
        size++;
    }

    public void AddAll(T[] array) {
        if (array == null) {
            throw new ArgumentNullException(nameof(array), "Массива не существует");
        }

        foreach (T x in array) AddLast(x);
    }

    public void Clear() {
        Array.Clear(elements, 0, elements.Length);
        head = 0;
        tail = 0;
        size = 0;
    }

    public bool Contains(T x) {
        for (int i = 0; i < size; i++) {
            int index = (head + i) % elements.Length;
            if (Comparer.Equals(elements[index], x))
                return true;
        }
        return false;
    }

    public bool ContainsAll(T[] array) {
        if (array == null) {
            throw new ArgumentNullException(nameof(array), "Массива не существует");
        }

        foreach (T x in array)
            if (!Contains(x))
                return false;
        return true;
    }

    public bool IsEmpty() => size == 0;

    public bool Remove(T x) {
        for (int i = 0; i < size; i++) {
            int index = (head + i) % elements.Length;
            if (Comparer.Equals(elements[index], x)) {
                for (int j = i; j < size - 1; j++) {
                    int current = (head + j) % elements.Length;
                    int next = (head + j + 1) % elements.Length;
                    elements[current] = elements[next];
                }
                tail = (tail - 1 + elements.Length) % elements.Length;
                size--;
                return true;
            }
        }
        return false;
    }

    public bool RemoveAll(T[] array) {
        if (array == null) {
            throw new ArgumentNullException(nameof(array), "Массива не существует");
        }

        bool ok = false;
        foreach (T x in array) {
            while (Remove(x)) {
                ok = true;
            }
        }
        return ok;
    }

    public bool RetainAll(T[] array) {
        if (array == null) {
            throw new ArgumentNullException(nameof(array), "Массива не существует");
        }

        bool ok = false;
        for (int i = size - 1; i >= 0; i--) {
            int index = (head + i) % elements.Length;
            T element = elements[index];
            bool retain = false;
            
            foreach (T x in array) {
                if (Comparer.Equals(element, x)) {
                    retain = true;
                    break;
                }
            }
            
            if (!retain) {
                for (int j = i; j < size - 1; j++) {
                    int current = (head + j) % elements.Length;
                    int next = (head + j + 1) % elements.Length;
                    elements[current] = elements[next];
                }
                tail = (tail - 1 + elements.Length) % elements.Length;
                size--;
                ok = true;
            }
        }
        return ok;
    }

    public int Size() => size;

    public T[] ToArray() {
        T[] ans = new T[size];
        for (int i = 0; i < size; i++) {
            ans[i] = elements[(head + i) % elements.Length];
        }
        return ans;
    }

    public T[] ToArray(T[] a) {
        if (a == null) {
            return ToArray();
        }

        if (a.Length < size) {
            a = new T[size];
        }

        for (int i = 0; i < size; i++) {
            a[i] = elements[(head + i) % elements.Length];
        }

        if (a.Length > size) {
            a[size] = default(T);
        }

        return a;
    }

    public T Element() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Двунаправленная очередь пуста");
        }
        return elements[head];
    }

    public bool Offer(T obj) {
        try {
            AddLast(obj);
            return true;
        } catch {
            return false;
        }
    }

    public T Peek() {
        if (IsEmpty()) return default(T);
        return elements[head];
    }

    public T Poll() {
        if (IsEmpty()) return default(T);
        return RemoveFirst();
    }

    public T GetFirst() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Двунаправленная очередь пуста");
        }
        return elements[head];
    }

    public T GetLast() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Двунаправленная очередь пуста");
        }
        return elements[(tail - 1 + elements.Length) % elements.Length];
    }

    public bool OfferFirst(T obj) {
        try {
            AddFirst(obj);
            return true;
        } catch {
            return false;
        }
    }

    public bool OfferLast(T obj) {
        try {
            AddLast(obj);
            return true;
        } catch {
            return false;
        }
    }

    public T Pop() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Двунаправленная очередь пуста");
        }
        return RemoveFirst();
    }

    public void Push(T obj) => AddFirst(obj);

    public T PeekFirst() {
        if (IsEmpty()) return default(T);
        return elements[head];
    }

    public T PeekLast() {
        if (IsEmpty()) return default(T);
        return elements[(tail - 1 + elements.Length) % elements.Length];
    }

    public T PollFirst() {
        if (IsEmpty()) return default(T);
        return RemoveFirst();
    }

    public T PollLast() {
        if (IsEmpty()) return default(T);
        return RemoveLast();
    }

    public T RemoveLast() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Двунаправленная очередь пуста");
        }
        
        tail = (tail - 1 + elements.Length) % elements.Length;
        T result = elements[tail];
        elements[tail] = default(T);
        size--;
        return result;
    }

    public T RemoveFirst() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Двунаправленная очередь пуста");
        }
        
        T result = elements[head];
        elements[head] = default(T);
        head = (head + 1) % elements.Length;
        size--;
        return result;
    }

    public bool RemoveLastOccurrence(T obj) {
        for (int i = size - 1; i >= 0; i--) {
            int index = (head + i) % elements.Length;
            if (Comparer.Equals(elements[index], obj)) {
                for (int j = i; j < size - 1; j++) {
                    int current = (head + j) % elements.Length;
                    int next = (head + j + 1) % elements.Length;
                    elements[current] = elements[next];
                }
                tail = (tail - 1 + elements.Length) % elements.Length;
                size--;
                return true;
            }
        }
        return false;
    }

    public bool RemoveFirstOccurrence(T obj) {
        for (int i = 0; i < size; i++) {
            int index = (head + i) % elements.Length;
            if (Comparer.Equals(elements[index], obj)) {
                for (int j = i; j < size - 1; j++) {
                    int current = (head + j) % elements.Length;
                    int next = (head + j + 1) % elements.Length;
                    elements[current] = elements[next];
                }
                tail = (tail - 1 + elements.Length) % elements.Length;
                size--;
                return true;
            }
        }
        return false;
    }

    public T this[int index] {
        get {
            if (index < 0 || index >= size) {
                throw new IndexOutOfRangeException();
            }
            return elements[(head + index) % elements.Length];
        }
    }
}

//      /\___/\
//     |  o_O |   wtf????
//--------------------------