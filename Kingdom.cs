namespace lab1;

public class Kingdom : Monarchy
{
    private bool parlament;

    public bool Parlament { get; set; }

    public Kingdom()
    {
        parlament = false;
    }

    public Kingdom(bool parlament, string name, long population, long area, string dynasty)
    :base(dynasty, name, population, area)
    {
        Parlament = parlament;
    }

    public override void Print()
    {
        System.Console.Write("Государство {0} с населением {1} имеет площадь {2} квадратных километров. Правязей династией является {3}. ", name, population, area, dynasty);
        if (parlament) System.Console.WriteLine("Но власть ограничена парламентом.");
    }
}