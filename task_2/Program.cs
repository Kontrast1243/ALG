using System;

namespace task_2;

class Program {

    static void Main() {
        
        Complex current = new Complex(0.0, 0.0);
        
        while (true) {
            Console.WriteLine("Текущее число: ");
            current.Print();
            Console.WriteLine("Меню:");
            Console.WriteLine("1 - Ввод нового комплексного числа");
            Console.WriteLine("+ - Сложение");
            Console.WriteLine("- - Вычитание");
            Console.WriteLine("* - Умножение");
            Console.WriteLine("/ - Деление");
            Console.WriteLine("m - Модуль");
            Console.WriteLine("a - Аргумент");
            Console.WriteLine("r - Вещественная часть");
            Console.WriteLine("i - Мнимая часть");
            Console.WriteLine("p - Вывод числа");
            Console.WriteLine("q/Q - Выход");
            Console.Write("Выберите операцию: ");
            
            string c = Console.ReadLine();
            
            switch (c) 
            {
                case "1":
                    current = Complex.ReadComlexNumber();
                    Console.Write("Новое число установлено: ");
                    current.Print();
                    break;
                    
                case "+":
                    current.Add();
                    Console.Write("Результат сложения: ");
                    current.Print();
                    break;
                    
                case "-":
                    current.Subtract();
                    Console.Write("Результат вычитания: ");
                    current.Print();
                    break;
                    
                case "*":
                    current.Multiply();
                    Console.Write("Результат умножения: ");
                    current.Print();
                    break;
                    
                case "/":
                    current.Divide();
                    Console.Write("Результат деления: ");
                    current.Print();
                    break;
                    
                case "m":
                    current.Multiply();
                    break;
                    
                case "a":
                    current.Argument();
                    break;
                    
                case "r":
                    current.RealPart();
                    break;
                    
                case "i":
                    current.ImaginaryPart();
                    break;
                    
                case "p":
                    Console.Write("Текущее число: ");
                    current.Print();
                    break;
                    
                case "q":
                case "Q":
                    Console.WriteLine("Выход из программы");
                    return;
                    
                default:
                    Console.WriteLine("Неизвестная команда");
                    break;
            }
        }
    }
}