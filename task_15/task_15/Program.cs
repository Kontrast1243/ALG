using System;
using System.IO;
using System.Linq;

class Program {
    static void Main(string[] args) {
        try {
            string[] lines = File.ReadAllLines("/Users/mihailprohorov/Desktop/учеба/C#/task_15/task_15/input.txt");
            
            if (lines.Length == 0) {
                Console.WriteLine("Файл input.txt пуст.");
                return;
            }
            
            MyArrayDeque<string> deque = new MyArrayDeque<string>();
            
            if (lines.Length > 0) {
                deque.AddFirst(lines[0]);
            }
            
            for (int i = 1; i < lines.Length; i++) {
                string currentLine = lines[i];
                
                string firstLine = deque.PeekFirst();
                
                int digitCountCurrent = CountDigits(currentLine);
                int digitCountFirst = CountDigits(firstLine);
                
                if (digitCountCurrent > digitCountFirst) {
                    deque.AddLast(currentLine);
                } else {
                    deque.AddFirst(currentLine);
                }
            }
            
            SaveDequeToFile(deque, "/Users/mihailprohorov/Desktop/учеба/C#/task_15/task_15/sorted.txt");
            Console.WriteLine("Результат сохранен в файл sorted.txt");
            
            Console.Write("Введите количество пробелов (n): ");
            if (!int.TryParse(Console.ReadLine(), out int n)) {
                Console.WriteLine("Некорректный ввод. Используется значение по умолчанию n = 3.");
                n = 3;
            }
            
            RemoveStringsWithMoreSpaces(deque, n);
            
            Console.WriteLine($"\nСтроки, содержащие не более {n} пробелов:");
            if (deque.IsEmpty()) {
                Console.WriteLine("Все строки были удалены.");
            } else {
                PrintDeque(deque);
            }
        } catch (IOException ex)
        {
            Console.WriteLine($"Ошибка ввода-вывода: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        
        Console.WriteLine("\nНажмите любую клавишу для выхода...");
        Console.ReadKey();
    }
    
    static int CountDigits(string str) {
        if (string.IsNullOrEmpty(str))
            return 0;
            
        return str.Count(c => char.IsDigit(c));
    }
    
    static int CountSpaces(string str) {
        if (string.IsNullOrEmpty(str))
            return 0;
            
        return str.Count(c => c == ' ');
    }
    
    static void SaveDequeToFile(MyArrayDeque<string> deque, string filename) {
        using (StreamWriter writer = new StreamWriter(filename)) {
            int size = deque.Size();
            for (int i = 0; i < size; i++) {
                writer.WriteLine(deque[i]);
            }
        }
    }
    
    static void RemoveStringsWithMoreSpaces(MyArrayDeque<string> deque, int n) {
        string[] stringsToRemove = new string[deque.Size()];
        int count = 0;
        
        int size = deque.Size();
        for (int i = 0; i < size; i++) {
            string line = deque[i];
            if (CountSpaces(line) > n) {
                stringsToRemove[count++] = line;
            }
        }
        
        if (count > 0) {
            string[] toRemove = new string[count];
            Array.Copy(stringsToRemove, toRemove, count);
            
            deque.RemoveAll(toRemove);
            
            Console.WriteLine($"Удалено строк: {count}");
        } else {
            Console.WriteLine("Строк для удаления не найдено.");
        }
    }
    
    static void PrintDeque(MyArrayDeque<string> deque) {
        int size = deque.Size();
        for (int i = 0; i < size; i++) {
            Console.WriteLine($"{i + 1}: {deque[i]}");
        }
    }
}