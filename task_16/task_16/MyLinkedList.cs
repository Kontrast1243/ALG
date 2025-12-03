class MyLinkedList<T> {
    private static readonly EqualityComparer<T> Comparer = EqualityComparer<T>.Default;

    public class Node {
        public T value;
        public Node next, prev;

        public Node(T value) {
            this.value = value;
        }
    }

    private Node first;
    private Node last;
    private int size;

    public MyLinkedList() {
        first = null;
        last = null;
        size = 0;
    }

    public MyLinkedList(T[] a) {
        if (a == null) {
            throw new ArgumentNullException(nameof(a), "Массив не может быть null");
        }

        foreach (T value in a) {
            AddLast(value);
        }
    }

    public void AddLast(T value) {
        Node newNode = new Node(value);
        if (last == null) {
            first = last = newNode;
        } else {
            last.next = newNode;
            newNode.prev = last;
            last = newNode;
        }
        size++;
    }

    public void AddFirst(T value) {
        Node newNode = new Node(value);
        if (first == null) { 
            first = last = newNode;
        } else {
            newNode.next = first;
            first.prev = newNode;
            first = newNode;
        }
        size++;
    }

    public void Add(T value) => AddLast(value);

    public void AddAll(T[] values) {
        if (values == null) {
            throw new ArgumentNullException(nameof(values), "Массив не может быть null");
        }

        foreach (T value in values) Add(value);
    }

    public void Clear() {
        first = null;
        last = null;
        size = 0;
    }

    public bool Contains(T value) {
        for (Node node = first; node != null; node = node.next) {
            if (Comparer.Equals(node.value, value)) return true;
        }
        return false;
    }

    public bool ContainsAll(T[] values) {
        if (values == null) {
            throw new ArgumentNullException(nameof(values), "Массив не может быть null");
        }

        foreach (T value in values) if (!Contains(value)) return false;
        return true;
    }

    public bool IsEmpty() => size == 0;

    public void RemoveLast() {
        if (last == null) return;
        if (last.prev == null) {
            first = last = null;
        } else {
            last = last.prev;
            last.next = null;
        }
        size--;
    }

    public void RemoveFirst() {
        if (first == null) return;
        if (first.next == null) { 
            first = last = null;
        } else {
            first = first.next;
            first.prev = null;
        }
        size--;
    }

    public bool Remove(T value) {
        for (Node node = first; node != null; node = node.next) {
            if (Comparer.Equals(node.value, value)) {
                if (node == first) RemoveFirst();
                else if (node == last) RemoveLast();
                else {
                    node.prev.next = node.next;
                    node.next.prev = node.prev;
                    size--;
                }
                return true;
            }
        }
        return false;
    }

    public bool RemoveAll(T[] values) {
        if (values == null) {
            throw new ArgumentNullException(nameof(values), "Массив не может быть null");
        }

        bool modified = false;
        foreach (T value in values) {
            while (Remove(value)) {
                modified = true;
            }
        }
        return modified;
    }

    public bool RetainAll(T[] values) {
        if (values == null) {
            throw new ArgumentNullException(nameof(values), "Массив не может быть null");
        }

        bool modified = false;
        Node node = first;
        while (node != null) {
            T currentValue = node.value;
            Node nextNode = node.next;
            
            bool found = false;
            foreach (T value in values) {
                if (Comparer.Equals(currentValue, value)) {
                    found = true;
                    break;
                }
            }
            
            if (!found) {
                Remove(currentValue);
                modified = true;
            }
            node = nextNode;
        }
        return modified;
    }

    public int Size() => size;

    public T[] ToArray() {
        T[] array = new T[size];
        Node node = first;
        for (int i = 0; i < size; i++) {
            array[i] = node.value;
            node = node.next;
        }
        return array;
    }

    public T[] ToArray(T[] values) {
        if (values == null) return ToArray();
        
        if (values.Length < size) {
            values = new T[size];
        }

        Node node = first;
        for (int i = 0; i < size; i++) {
            values[i] = node.value;
            node = node.next;
        }

        if (values.Length > size) {
            values[size] = default(T);
        }

        return values;
    }

    public void Add(int index, T value) {
        if (index < 0 || index > size) {
            throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
        }

        if (index == 0) {
            AddFirst(value);
        } else if (index == size) {
            AddLast(value);
        } else {
            Node newNode = new Node(value);
            Node current = GetNodeAt(index);
            
            newNode.prev = current.prev;
            newNode.next = current;
            current.prev.next = newNode;
            current.prev = newNode;
            
            size++;
        }
    }

    public void AddAll(int index, T[] values) {
        if (values == null) {
            throw new ArgumentNullException(nameof(values), "Массив не может быть null");
        }

        if (index < 0 || index > size) {
            throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
        }

        for (int i = 0; i < values.Length; i++) {
            Add(index + i, values[i]);
        }
    }

    public T Get(int index) {
        if (index < 0 || index >= size) {
            throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
        }

        return GetNodeAt(index).value;
    }

    public int IndexOf(T value) {
        int index = 0;
        for (Node node = first; node != null; node = node.next) {
            if (Comparer.Equals(node.value, value)) {
                return index;
            }
            index++;
        }
        return -1;
    }

    public int LastIndexOf(T value) {
        int index = size - 1;
        for (Node node = last; node != null; node = node.prev) {
            if (Comparer.Equals(node.value, value)) {
                return index;
            }
            index--;
        }
        return -1;
    }

    public T Remove(int index) {
        if (index < 0 || index >= size) {
            throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
        }

        Node nodeToRemove = GetNodeAt(index);
        T value = nodeToRemove.value;
        
        if (nodeToRemove == first) RemoveFirst();
        else if (nodeToRemove == last) RemoveLast();
        else {
            nodeToRemove.prev.next = nodeToRemove.next;
            nodeToRemove.next.prev = nodeToRemove.prev;
            size--;
        }
        
        return value;
    }

    public T Set(int index, T value) {
        if (index < 0 || index >= size) {
            throw new ArgumentOutOfRangeException(nameof(index), "Индекс вне диапазона");
        }

        Node node = GetNodeAt(index);
        T oldValue = node.value;
        node.value = value;
        return oldValue;
    }

    public MyLinkedList<T> SubList(int fromIndex, int toIndex) {
        if (fromIndex < 0 || fromIndex > size || toIndex < 0 || toIndex > size || fromIndex > toIndex) {
            throw new ArgumentOutOfRangeException("Некорректные индексы");
        }

        MyLinkedList<T> subList = new MyLinkedList<T>();
        Node node = GetNodeAt(fromIndex);
        
        for (int i = fromIndex; i < toIndex; i++) {
            subList.AddLast(node.value);
            node = node.next;
        }
        
        return subList;
    }

    public T Element() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Список пуст");
        }
        return first.value;
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
        return first.value;
    }

    public T Poll() {
        if (IsEmpty()) return default(T);
        T value = first.value;
        RemoveFirst();
        return value;
    }

    public T GetFirst() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Список пуст");
        }
        return first.value;
    }

    public T GetLast() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Список пуст");
        }
        return last.value;
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
            throw new InvalidOperationException("Список пуст");
        }
        return RemoveFirstReturn();
    }

    public void Push(T obj) => AddFirst(obj);

    public T PeekFirst() {
        if (IsEmpty()) return default(T);
        return first.value;
    }

    public T PeekLast() {
        if (IsEmpty()) return default(T);
        return last.value;
    }

    public T PollFirst() {
        if (IsEmpty()) return default(T);
        return RemoveFirstReturn();
    }

    public T PollLast() {
        if (IsEmpty()) return default(T);
        return RemoveLastReturn();
    }

    public T RemoveLastReturn() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Список пуст");
        }
        T value = last.value;
        RemoveLast();
        return value;
    }

    public T RemoveFirstReturn() {
        if (IsEmpty()) {
            throw new InvalidOperationException("Список пуст");
        }
        T value = first.value;
        RemoveFirst();
        return value;
    }

    public bool RemoveLastOccurrence(T obj) {
        for (Node node = last; node != null; node = node.prev) {
            if (Comparer.Equals(node.value, obj)) {
                if (node == first) RemoveFirst();
                else if (node == last) RemoveLast();
                else {
                    node.prev.next = node.next;
                    node.next.prev = node.prev;
                    size--;
                }
                return true;
            }
        }
        return false;
    }

    public bool RemoveFirstOccurrence(T obj) {
        return Remove(obj);
    }

    private Node GetNodeAt(int index) {
        if (index < 0 || index >= size) {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        Node current;
        if (index < size / 2) {
            current = first;
            for (int i = 0; i < index; i++) {
                current = current.next;
            }
        } else {
            current = last;
            for (int i = size - 1; i > index; i--) {
                current = current.prev;
            }
        }
        return current;
    }
}