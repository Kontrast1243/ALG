using System;

namespace lab2;

public class Monarchy : State, IPrintLider, IPrintPopulation, IPrintArea
{
    public string Dynasty { get; set; }
    public Monarchy(string dynasty, string name, long population, long area)
        : base(name, population, area)
    {
        Dynasty = dynasty;
    }

    public override void PrintAll()
    {
        Console.WriteLine($"Монархия {Name} с населением {Population} имеет площадь {Area} км². Правящей династией является {Dynasty}.");
    }

    public void PrintLider() => Console.WriteLine($"Монарх: {Name}");
    public void PrintPopulation() => Console.WriteLine($"Население: {Population}");
    public void PrintArea() => Console.WriteLine($"Площадь: {Area} км²");
}