namespace lab2;

public class Kingdom : Monarchy, IPrintLider, IPrintPopulation, IPrintArea
{
    public bool Parlament { get; set; }

    public Kingdom(bool parlament, string name, long population, long area, string dynasty)
    :base(dynasty, name, population, area)
    {
        Parlament = parlament;
    }

    public override void PrintAll()
    {
        base.PrintAll();
        if (Parlament) System.Console.WriteLine("Но власть ограничена парламентом.");
    }
    
    public void PrintLider() => Console.WriteLine($"Монарх: {Name}");
    public void PrintPopulation() => Console.WriteLine($"Население: {Population}");
    public void PrintArea() => Console.WriteLine($"Площадь: {Area} км²");
}