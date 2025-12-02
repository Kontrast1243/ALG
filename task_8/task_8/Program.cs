using System.ComponentModel;

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

public class Program {
    
    static void Main() {
        
        Console.WriteLine("Создание списков:");
        
        Console.WriteLine();

        MyArrayList<int> list1 = new MyArrayList<int>();
        Console.WriteLine($"Пустой список: размер = {list1.Size()}, емкость = {list1.Capacity}");
        
        Console.WriteLine();

        MyArrayList<string> list2 = new MyArrayList<string>(5);
        Console.WriteLine($"Список с начальной емкостью 5: размер = {list2.Size()}, емкость = {list2.Capacity}");
        
        Console.WriteLine();

        int[] initialArray = { 1, 2, 3, 4, 5 };
        MyArrayList<int> list3 = new MyArrayList<int>(initialArray);
        Console.WriteLine($"Список из массива [1,2,3,4,5]: размер = {list3.Size()}, емкость = {list3.Capacity}");
        
        Console.Write("Элементы: ");
        for (int i = 0; i < list3.Size(); i++) Console.Write(list3.Get(i) + " ");
        
        Console.WriteLine();
        
        Console.WriteLine();

        Console.WriteLine("Добавление элементов:");
        
        Console.WriteLine();

        MyArrayList<int> list = new MyArrayList<int>();
        for (int i = 1; i <= 10; i++) {
            list.Add(i * 10);
        }
        
        Console.WriteLine($"После добавления 10 элементов: размер = {list.Size()}");
        
        Console.Write("Элементы: ");
        for (int i = 0; i < list.Size(); i++) Console.Write(list.Get(i) + " ");
        
        Console.WriteLine();
        
        Console.WriteLine();

        Console.WriteLine("Автоматическое увеличение емкости:");
        
        Console.WriteLine();

        Console.WriteLine($"Текущая емкость: {list.Capacity}");
        
        list.Add(110); list.Add(111);
        
        Console.WriteLine($"После добавления 11-го и 12-го элемента: размер = {list.Size()}, емкость = {list.Capacity}");
        
        Console.WriteLine();

        Console.WriteLine("Добавление массива элементов:");
        
        Console.WriteLine();

        int[] newArray = { 120, 130, 40 };
        list.AddAll(newArray);
        
        Console.WriteLine($"После AddAll: размер = {list.Size()}");
        
        Console.WriteLine();

        Console.WriteLine("Работа по индексу:");
        
        Console.WriteLine();

        list.Add(2, 999);
        
        Console.WriteLine($"После вставки 999 на позицию 2: элемент на позиции 2 = {list.Get(2)}");
        
        Console.WriteLine();

        list.Set(5, 888);
        
        Console.WriteLine($"После замены элемента на позиции 5 на 888: элемент = {list.Get(5)}");
        
        Console.WriteLine();
        
        Console.Write("Элементы: ");
        for (int i = 0; i < list.Size(); i++) Console.Write(list.Get(i) + " ");
        Console.WriteLine();

        Console.WriteLine("Поиск элементов:");
        
        Console.WriteLine();
        
        Console.WriteLine();

        Console.WriteLine($"Содержит 40? {list.Contains(40)}");
        
        Console.WriteLine();

        Console.WriteLine($"Индекс 40: {list.IndexOf(40)}");
        
        Console.WriteLine();

        Console.WriteLine($"Последний индекс 40: {list.LastIndexOf(40)}");
        
        Console.WriteLine();

        int[] searchArray = { 10, 20, 30 };
        
        Console.WriteLine($"Содержит все [10,20,30]? {list.ContainsAll(searchArray)}");
        
        Console.WriteLine();

        Console.WriteLine("Удаление элементов: удалим число 999");
        
        Console.WriteLine();

        
        list.Remove(999);
        
        for (int i = 0; i < list.Size(); i++) Console.Write(list.Get(i) + " ");
        Console.WriteLine();
        
        Console.WriteLine();

        int removed = list.RemoveIndex(3);
        
        Console.WriteLine($"Удален элемент на позиции 3: {removed}");
        
        Console.WriteLine();
        
        for (int i = 0; i < list.Size(); i++) Console.Write(list.Get(i) + " ");
        Console.WriteLine();
        
        Console.WriteLine();

        Console.WriteLine("Получение подмассива:");
        
        Console.WriteLine();

        MyArrayList<int> sublist = list.SubList(2, 6);
        
        for (int i = 0; i < sublist.Size(); i++) Console.Write(sublist.Get(i) + " ");
        
        Console.Write("Элементы с 2 по 6: ");
        
        Console.WriteLine();
        
        Console.WriteLine();

        Console.WriteLine("Преобразование в массив:");
        
        Console.WriteLine();

        int[] array1 = list.ToArray();
        
        Console.Write("Массив: ");
        foreach (var i in array1) Console.Write(i + " ");
        
        Console.WriteLine();
        
        Console.WriteLine();

        Console.WriteLine("Оставить только указанные элементы:");
        
        Console.WriteLine();

        MyArrayList<int> retainList = new MyArrayList<int>(new int[] { 10, 20, 30, 40, 50 });
        
        int[] a = { 20, 40 };
        
        retainList.RetainAll(a);
        
        Console.Write("После RetainAll [20,40]: ");
        for (int i = 0; i < retainList.Size(); i++) Console.Write(retainList[i] + " ");
        
        Console.WriteLine();
        
        Console.WriteLine();

        Console.WriteLine("Очистка списка:");
        
        Console.WriteLine();

        Console.WriteLine($"Список пустой? {list.IsEmpty()}");
        
        list.Clear();
        
        Console.WriteLine($"После Clear: размер = {list.Size()}, пустой? {list.IsEmpty()}");
        
        Console.WriteLine();

        Console.WriteLine("Обработка ошибок:");
        
        Console.WriteLine();

        try {
            list.Get(-1); 
        } catch (ArgumentOutOfRangeException e)  {
            Console.WriteLine($"Поймано исключение: {e.Message}");
        }
        
        Console.WriteLine();
        
    }
}

//      /\___/\
//     |  o_O |   wtf????
//--------------------------