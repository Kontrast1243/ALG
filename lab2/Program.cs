using System;

namespace lab2;

class Program
{
    static void Main()
    {
        State[] countries = new State[]
        {
            new Republic("Бразилия", 220051512, 8515767, "Луис Инасиу Лула да Силва"),
            new Monarchy("Ямато", "Япония", 125800000, 377975),
            new Kingdom(true, "Великобритания", 67220000, 242495, "Виндзоры")
        };

        Console.WriteLine("Выбери страну:");
        for (int i = 0; i < countries.Length; i++)
        {
            Console.WriteLine($"{i + 1}. {countries[i].Name}");
        }

        if (!int.TryParse(Console.ReadLine(), out int countryI) ||
            countryI < 1 || countryI > countries.Length)
        {
            Console.WriteLine("Неверный выбор страны.");
            return;
        }

        State curr = countries[countryI - 1];

        Console.WriteLine("\nВыбкрите один из следующих пунктов?");
        Console.WriteLine("1. Вся информация");
        Console.WriteLine("2. Лидер-ПутинВВ");
        Console.WriteLine("3. Население");
        Console.WriteLine("4. Площадь");

        if (!int.TryParse(Console.ReadLine(), out int choice) || choice < 1 || choice > 4)
        {
            Console.WriteLine("Неверный выбор информации.");
            return;
        }
        
        if (choice == 1) curr.PrintAll();
        else if (choice == 2) curr.PrintLider();
        else if (choice == 3) curr.PrintPopulation();
        else curr.PrintArea();
        yuyuyuyuyuyuyuuyyuuyyuyuyuyyuyuyuyyyuyuuyyuyuyu
    }
}