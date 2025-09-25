using System;

namespace lab2;

public abstract class State : IPrintLider, IPrintPopulation, IPrintArea
{
    public string Name { get; set; }
    public long Population { get; set; }
    public long Area { get; set; }

    protected State(string name, long population, long area)
    {
        Name = name;
        Population = population;
        Area = area;
    }

    public abstract void PrintAll();
    public void PrintLider() => Console.WriteLine($"Лидер: {Name}");
    public void PrintPopulation() => Console.WriteLine($"Население: {Population}");
    public void PrintArea() => Console.WriteLine($"Площадь: {Area} км²");
}


public interface IPrintLider
{
    void PrintLider();
}

public interface IPrintPopulation
{
    void PrintPopulation();
}

public interface IPrintArea
{
    void PrintArea();
}


