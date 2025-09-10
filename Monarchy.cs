using System;

namespace lab1;

public class Monarchy : State
{
    protected string dynasty;
    
    public string Dynasty
    {
        get { return dynasty; }
        set
        {
            if (value != "") dynasty = value;
        }
    }

    public Monarchy()
    {
        Dynasty = "Неизвестно";
    }

    public Monarchy(string dynasty, string name, long population, long area)
    :base(name, population, area)
    {
        Dynasty = dynasty;
    }

    public override void Print()
    {
        System.Console.WriteLine("Государство {0} с населением {1} имеет площадь {2} квадратных километров. Правязей династией является {3}.", name, population, area, dynasty);
    }
}