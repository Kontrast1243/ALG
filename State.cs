using System;

namespace lab1;

public class State
{
    protected string name;
    protected long population;
    protected long area;

    public string Name
    {
        get { return name; }
        set
        {
            if (value != "") name = value;
        }
    }

    public long Population
    {
        get { return population; }
        set
        {
            if (value > 0) population = value;
        }
    }

    public long Area
    {
        get { return area; }
        set
        {
            if (value > 0) area = value;
        }
    }

    public State()
    {
        name = "Неизвестно";
        population = 0;
        area = 0;
    }

    public State(string name, long population, long area)
    {
        Name = name;
        Population = population;
        Area = area;
    }

    public virtual void Print()
    {
        Console.WriteLine("Государство '{0}' с населением '{1}' имеет площадь '{2}' квадратных километров.", name, population, area);
    }
    
    
}

