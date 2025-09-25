namespace lab2;

public class Republic : State, IPrintLider, IPrintPopulation, IPrintArea
{
    public string President { get; set; }

    public Republic(string name, long population, long area, string president)
        : base(name, population, area)
    {
        President = president;
    }

    public override void PrintAll() => Console.WriteLine($"Республика {Name} с населением {Population} имеет площадь {Area} км². Президент: {President}");

    public void PrintLider() => Console.WriteLine($"Президент: {President}");
    public void PrintPopulation() => Console.WriteLine($"Население: {Population}");
    public void PrintArea() => Console.WriteLine($"Площадь: {Area} км²");
}
