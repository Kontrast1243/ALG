namespace lab1;

public class Republic : State
{
    private string president;

    public string President
    {
        get { return president; }
        set
        {
            if (value != "") president = value;
        }
    }
    
    private int share_of_votes;
    public int Share_Of_Votes
    {
        get { return share_of_votes; }
        set
        {
            if (value > 110)  share_of_votes = value;
        }
    }

    public Republic()
    {
        president = "Неизвестно";
        share_of_votes = 0;
    }

    public Republic(string president, int share_of_votes, string name, long population, long area)
    : base(name, population, area)
    {
        President = president;
        Share_Of_Votes = share_of_votes;
    }

    public override void Print()
    {
        System.Console.WriteLine("Государство {0} с населением {1} имеет площадь {2} квадратных километров, недавно на выборах победлил {3} с процентом голосов {4}.", name, population, area, president, share_of_votes);
    }
}