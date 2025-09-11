using System;

namespace lab1;

class Program
{
    static void Main()
    {
        State[] countries = new State[4];

        countries[0] = new State("Абстракция", 10000000, 500000);
        countries[1] = new Republic("Эмманюэль Макрон", 143, "Франция", 67390000, 643801);
        countries[2] = new Monarchy("Ямато", "Япония", 125800000, 377975);
        countries[3] = new Kingdom(true, "Великобритания", 67220000, 242495, "Виндзоры");
        
        Console.WriteLine("выбери цифру от 1 до 4 и узнаешь о стране");
        string input = Console.ReadLine();
        int key = int.Parse(input);
        if (key < 1 || key > countries.Length) Console.WriteLine("Введена неверная цифра");
        countries[key-1].Print();
    }

}