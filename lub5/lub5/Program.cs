using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection; 

namespace lab5
{
    public interface ILoadSave { void Load(); void Save(); }
    public interface IManage { void ListAll(); void AddStudent(); void EditStudent(); void DeleteStudent(); }

    public class GradeRecord
    {
        public string Subject { get; set; }
        public int Grade { get; set; }
        public GradeRecord(string subj, int grade) { Subject = subj; Grade = grade; }
    }

    public class Student
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public int Course { get; set; }
        public int Group { get; set; }
        public List<GradeRecord> Performance { get; set; } = new();
    }

    public class ExceptionEventArgs : EventArgs
    {
        public Exception Exception { get; }
        public string MethodName { get; }
        public DateTime Time { get; }

        public ExceptionEventArgs(Exception ex, string methodName)
        {
            Exception = ex;
            MethodName = methodName;
            Time = DateTime.Now;
        }
    }

    public class DataManager : IManage, ILoadSave
    {
        public List<Student> Students { get; private set; } = new();
        private string path = "/Users/mihailprohorov/Desktop/учеба/C#/lab3/text.txt";

        public void Load()
        {
            Students.Clear();
            foreach (var line in File.ReadAllLines(path))
            {
                var p = line.Split();
                string name = p[0];
                string last = p[1];
                int course = int.Parse(p[2]);
                int group = int.Parse(p[3]);
                string subj = p[4];
                int grade = int.Parse(p[5]);

                var st = Students.FirstOrDefault(s =>
                    s.Name == name && s.LastName == last &&
                    s.Course == course && s.Group == group);

                if (st == null)
                {
                    st = new Student { Name = name, LastName = last, Course = course, Group = group };
                    Students.Add(st);
                }
                st.Performance.Add(new GradeRecord(subj, grade));
            }
        }

        public void Save()
        {
            var lines = new List<string>();
            foreach (var s in Students)
                foreach (var g in s.Performance)
                    lines.Add($"{s.Name} {s.LastName} {s.Course} {s.Group} {g.Subject} {g.Grade}");
            File.WriteAllLines(path, lines);
        }

        public void ListAll()
        {
            if (Students.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            for (int i = 0; i < Students.Count; i++)
            {
                var s = Students[i];
                Console.Write($"{i + 1}. {s.Name} {s.LastName} | Курс {s.Course} | Группа {s.Group} | Оценки: ");
                foreach (var g in s.Performance)
                {
                    Console.Write($"{g.Subject}:{g.Grade} ");
                }
                Console.WriteLine();
            }
        }

        public void AddStudent()
        {
            Console.Write("Имя: "); string name = Console.ReadLine();
            Console.Write("Фамилия: "); string last = Console.ReadLine();
            Console.Write("Курс: "); int course = int.Parse(Console.ReadLine());
            Console.Write("Группа: "); int group = int.Parse(Console.ReadLine());

            var st = new Student { Name = name, LastName = last, Course = course, Group = group };

            while (true)
            {
                Console.Write("Предмет (пусто = конец): ");
                var subj = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(subj)) break;
                Console.Write("Оценка: "); int gr = int.Parse(Console.ReadLine());
                st.Performance.Add(new GradeRecord(subj, gr));
            }
            Students.Add(st);
        }

        public void EditStudent()
        {
            Console.Write("Имя и фамилия: ");
            var p = Console.ReadLine().Split();
            var st = Students.FirstOrDefault(s => s.Name == p[0] && s.LastName == p[1]);
            if (st == null) return;

            Console.Write("Новое имя (Enter = без изм): ");
            var n = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(n)) st.Name = n;
            Console.Write("Новая фамилия (Enter = без изм): ");
            var l = Console.ReadLine(); if (!string.IsNullOrWhiteSpace(l)) st.LastName = l;
            Console.Write("Новый курс (Enter = без изм): ");
            var c = Console.ReadLine(); if (int.TryParse(c, out int nc)) st.Course = nc;
            Console.Write("Новая группа (Enter = без изм): ");
            var g = Console.ReadLine(); if (int.TryParse(g, out int ng)) st.Group = ng;
        }

        public void DeleteStudent()
        {
            Console.Write("Имя и фамилия: ");
            var p = Console.ReadLine().Split();
            var st = Students.FirstOrDefault(s => s.Name == p[0] && s.LastName == p[1]);
            if (st != null) Students.Remove(st);
        }

        public void Variant13()
        {
            Students.RemoveAll(s => s.Course == 1 && s.Performance.Count(g => g.Grade == 2) == 3);
        }

        public delegate void CheckMethodDelegate();
        public CheckMethodDelegate CheckMethods;

        public DataManager()
        {
            CheckMethods = ListAll;
            CheckMethods += AddStudent;
            CheckMethods += EditStudent;
            CheckMethods += DeleteStudent;
            CheckMethods += Save;
            CheckMethods += Variant13;
        }

        public void TestAllMethods()
        {
            Console.WriteLine("Проверка всех методов через многоадресный делегат");
            CheckMethods();
            Console.WriteLine("Проверка завершена");
        }
    }

    public class SafeDataManager : DataManager
    {
        public event EventHandler<ExceptionEventArgs> OnError;

        protected virtual void RaiseError(Exception ex, string methodName)
        {
            OnError.Invoke(this, new ExceptionEventArgs(ex, methodName));
        }

        public new void Load()
        {
            try { base.Load(); }
            catch (Exception ex) when (
                ex is IndexOutOfRangeException ||
                ex is InvalidCastException ||
                ex is OverflowException ||
                ex is OutOfMemoryException ||
                ex is StackOverflowException ||
                ex is ArrayTypeMismatchException ||
                ex is DivideByZeroException)
            {
                RaiseError(ex, "Load");
            }
            catch (FormatException ex)
            {
                RaiseError(new InvalidCastException("Неверный формат числа", ex), "Load");
            }
        }

        public new void AddStudent()
        {
            try { base.AddStudent(); }
            catch (Exception ex) when (ex is OverflowException || ex is OutOfMemoryException || ex is StackOverflowException)
            {
                RaiseError(ex, "AddStudent");
            }
            catch (FormatException ex)
            {
                RaiseError(new InvalidCastException("Оценка или курс — не число!", ex), "AddStudent");
            }
        }

        public new void EditStudent()
        {
            try { base.EditStudent(); }
            catch (Exception ex) when (ex is InvalidCastException || ex is OverflowException)
            {
                RaiseError(ex, "EditStudent");
            }
            catch (FormatException ex)
            {
                RaiseError(new InvalidCastException("Неверный ввод числа", ex), "EditStudent");
            }
        }

        public new void DeleteStudent()
        {
            try { base.DeleteStudent(); }
            catch (Exception ex) when (ex is IndexOutOfRangeException)
            {
                RaiseError(ex, "DeleteStudent");
            }
        }

        public new void Save()
        {
            try { base.Save(); }
            catch (Exception ex) when (ex is OutOfMemoryException || ex is StackOverflowException)
            {
                RaiseError(ex, "Save");
            }
        }

        public new void TestAllMethods()
        {
            Console.WriteLine("Проверка всех методов через многоадресный делегат");
            try
            {
                CheckMethods(); 
            }
            catch (TargetInvocationException ex) 
            {
                if (ex.InnerException != null)
                    RaiseError(ex.InnerException, "TestAllMethods (через делегат)");
            }
            catch (Exception ex) when (
                ex is StackOverflowException ||
                ex is OutOfMemoryException ||
                ex is IndexOutOfRangeException ||
                ex is DivideByZeroException ||
                ex is InvalidCastException ||
                ex is OverflowException ||
                ex is ArrayTypeMismatchException)
            {
                RaiseError(ex, "TestAllMethods");
            }
            Console.WriteLine("Проверка завершена");
        }
    }

    class Program
    {
        static void Main()
        {
            SafeDataManager dm = new SafeDataManager();

            dm.OnError += (sender, e) =>
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nОшибка!");
                Console.WriteLine($"Время: {e.Time}");
                Console.WriteLine($"Метод: {e.MethodName}");
                Console.WriteLine($"Тип ошибки: {e.Exception.GetType().Name}");
                Console.WriteLine($"Сообщение: {e.Exception.Message}");
                Console.ResetColor();
                Console.WriteLine("Нажмите любую клавишу для продолжения...");
                Console.ReadKey();
            };

            dm.Load();

            while (true)
            {
                Console.WriteLine("\n1 - список, 2 - добавить, 3 - редактировать, 4 - удалить, 5 - сохранить, 6 - отчислить, 7 - проверить все методы, 0 - выход");
                var k = Console.ReadLine();

                if (k == "1") dm.ListAll();
                else if (k == "2") dm.AddStudent();
                else if (k == "3") dm.EditStudent();
                else if (k == "4") dm.DeleteStudent();
                else if (k == "5") dm.Save();
                else if (k == "6") dm.Variant13();
                else if (k == "7") dm.TestAllMethods();
                else if (k == "0") break;
            }
        }
    }
}