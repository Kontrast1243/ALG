using System;
using System.Linq;
using System.Reflection;
class Program
{
    static string[] OPERATIONS =
        { "+", "-", "*", "/", "^", "sqrt", "abs", "sign", "sin", "cos", "tg",
        "ln", "lg", "min", "max", "%", "//", "exp", "round", "(", ")" };
    static MyStack<double> vals;
    static void Operate(string operation)
    {
        double a, b;
        switch (operation)
        {
            case "+":
                a = vals.Pop();
                b = vals.Pop();
                vals.Push(a + b);
                break;
            case "-":
                a = vals.Pop();
                b = vals.Pop();
                vals.Push(b - a);
                break;
            case "*":
                a = vals.Pop();
                b = vals.Pop();
                vals.Push(a * b);
                break;
            case "/":
                a = vals.Pop();
                b = vals.Pop();
                if (a == 0) throw new DivideByZeroException();
                vals.Push(b / a);
                break;
            case "^":
                a = vals.Pop();
                b = vals.Pop();
                vals.Push(Math.Pow(b, a));
                break;
            case "sqrt":
                a = vals.Pop();
                if(a < 0) throw new Exception("Корень из отрицательного числа!");
                vals.Push(Math.Sqrt(a));
                break;
            case "abs":
                a = vals.Pop();
                vals.Push(Math.Abs(a));
                break;
            case "sign":
                a = vals.Pop();
                vals.Push(Math.Sign(a));
                break;
            case "sin":
                a = vals.Pop();
                vals.Push(Math.Sin(a));
                break;
            case "cos":
                a = vals.Pop();
                vals.Push(Math.Cos(a));
                break;
            case "tg":
                a = vals.Pop();
                vals.Push(Math.Tan(a));
                break;
            case "ln":
                a = vals.Pop();
                if (a <= 0) throw new Exception("Логарифм от неположительного числа!");
                vals.Push(Math.Log(a));
                break;
            case "lg":
                a = vals.Pop();
                if (a <= 0) throw new Exception("Логарифм от неположительного числа!");
                vals.Push(Math.Log10(a));
                break;
            case "min":
                a = vals.Pop();
                b = vals.Pop();
                vals.Push(Math.Min(a, b));
                break;
            case "max":
                a = vals.Pop();
                b = vals.Pop();
                vals.Push(Math.Max(a, b));
                break;
            case "%":
                a = vals.Pop();
                b = vals.Pop();
                if (a == 0) throw new DivideByZeroException();
                vals.Push((double)((int)a % (int)b));
                break;
            case "//":
                a = vals.Pop();
                b = vals.Pop();
                if (a == 0) throw new DivideByZeroException();
                vals.Push(Math.Floor(b / a));
                break;
            case "exp":
                a = vals.Pop();
                vals.Push(Math.Exp(a));
                break;
            case "round":
                a = vals.Pop();
                vals.Push(Math.Floor(a));
                break;
        }
    }
    static int Priority(string op)
    {
        switch (op)
        {
            case "(": return 0;
            case "+":
            case "-": return 1;
            case "*":
            case "/":
            case "%":
            case "//": return 2;
            case "^":
            case "min":
            case "max": return 3;
            default: return 4;
        }
    }
    static void Main()
    {
        string[] mathExpr = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] vars = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        string[] names = new string[0]; double[] values = new double[0];
        if (vars.Length > 0)
        {
            names = new string[vars.Length];
            values = new double[vars.Length];
            double tries;
            for (int i = 0; i < vars.Length; ++i)
            {
                names[i] = vars[i].Substring(0, vars[i].IndexOf('='));
                if (double.TryParse(vars[i].Substring(vars[i].IndexOf('=') + 1), out tries))
                    values[i] = tries;
                else throw new Exception("Некорректное значение переменной!");

            }
        }
        vals = new MyStack<double>();
        MyStack<string> opers = new MyStack<string>();
        foreach (string value in mathExpr)
        {
            double doub; string opera;
            if(!OPERATIONS.Contains(value))
            {
                if (double.TryParse(value, out doub))
                    vals.Push(doub);
                else if (vars.Length > 0)
                {
                    bool found = false;
                    for (int i = 0; i < vars.Length && !found; ++i)
                        if (value == names[i])
                        {
                            vals.Push(values[i]);
                            found = true;
                        }
                    if (!found) throw new Exception("Несуществующая переменная!");
                }
                else throw new Exception("Несуществующая переменная!");
            }
            else
            {
                if (value == "(") 
                    opers.Push(value);
                else if (value == ")")
                {
                    while (!opers.Empty() && opers.Peek() != "(")
                        Operate(opers.Pop());
                    if(!opers.Empty()) opers.Pop();
                }
                else
                {
                    while(!opers.Empty() && Priority(opers.Peek()) >= Priority(value))
                        Operate(opers.Pop());
                    opers.Push(value);
                }
            }
        }
        while(!opers.Empty())
            Operate(opers.Pop());
        double ans = 0;
        if (!vals.Empty()) ans = vals.Pop();
        if (!vals.Empty()) throw new Exception("Неправильная последовательность операций");
        Console.WriteLine(ans);
    }
}