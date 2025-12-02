class MyStack<T> : MyVector<T> {
    
    public MyStack() : base() {}

    public MyStack(int capacity, int capacityIncrement) : base(capacity, capacityIncrement) { }

    public MyStack(T[] array) : base(array) { }

    public void Push(T x) {
        Add(x);
    }

    public T Pop() {
        if (IsEmpty())
            throw new Exception("Empty stack");
        T top = LastElement();
        RemoveIndex(Size() - 1);
        return top;
    }

    public T Peek() {
        if (IsEmpty())
            throw new Exception("Empty stack");
        return LastElement();
    }
    
    public bool Empty() => IsEmpty();

    public int Search(T x) {
        for (int j = Size() - 1; j >= 0; j--) {
            if (Comparer.Equals(Get(j), x))
                return Size() - j;
        }
        return -1;
    }
    
    public override string ToString()
    {
        if (IsEmpty())
            return "Стек [пуст]";

        string result = "Стек [";
        for (int i = Size() - 1; i >= 0; i--) {
            result += Get(i);
            if (i > 0)
                result += ", ";
        }
        result += "]";
        return result;
    }
}
