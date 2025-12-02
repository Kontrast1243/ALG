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

class Program {
    static void Main() {
        
        Console.WriteLine("Создаем стек целых чисел:");
        MyStack<int> stack = new MyStack<int>();
        Console.WriteLine($"Стек пуст? {stack.Empty()}");
        
        Console.WriteLine("Добавляем элементы в стек:");
        for (int i = 1; i <= 5; i++) {
            stack.Push(i * 10);
            Console.WriteLine($"Push({i * 10}) -> {stack}");
        }
        
        Console.WriteLine("Просмотр вершины стека:");
        Console.WriteLine($"Peek() = {stack.Peek()}");
        
        Console.WriteLine("Извлекаем элементы из стека:");
        while (!stack.Empty()) {
            Console.WriteLine($"Pop() = {stack.Pop()} -> {stack}");
        }
        
        Console.WriteLine("Попытка извлечения из пустого стека:");
        try {
            stack.Pop();
        }
        catch (Exception ex) {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        
        Console.WriteLine("Поиск элементов в стеке:");
        stack.Push(100);
        stack.Push(200);
        stack.Push(300);
        stack.Push(200);
        stack.Push(400);
        Console.WriteLine($"Стек: {stack}");
        
        Console.WriteLine($"Search(100) = {stack.Search(100)}");  
        Console.WriteLine($"Search(200) = {stack.Search(200)}");  
        Console.WriteLine($"Search(300) = {stack.Search(300)}");  
        Console.WriteLine($"Search(999) = {stack.Search(999)}");  
        
    }
}