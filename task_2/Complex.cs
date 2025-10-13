namespace task_2;

public class Complex {
    
    public double Real {get; set;}
    public double Imaginary {get; set;}

    public Complex(double real = 0, double imaginary = 0) {
        Real = real;
        Imaginary = imaginary;
    }

    static public Complex ReadComlexNumber() {
        Console.WriteLine("Введите действительную часть");
        double x = double.Parse(Console.ReadLine());
        Console.WriteLine("Введите мнимую часть");
        double y = double.Parse(Console.ReadLine());
        return new Complex(x, y);
    }

    public void Add() {
        Complex other = ReadComlexNumber();
        Real += other.Real;
        Imaginary += other.Imaginary;
    }
    
    public void  Subtract() {
        Complex other = ReadComlexNumber();
        Real -= other.Real;
        Imaginary -= other.Imaginary;
    }

    public void Multiply() {
        Complex other = ReadComlexNumber();
        double newReal = Real * other.Real - Imaginary * other.Imaginary;
        double newImaginary = Real * other.Imaginary + Imaginary * other.Real;
        Real = newReal;
        Imaginary = newImaginary;
    }

    public void Divide() {
        Complex other = ReadComlexNumber();
        double denom = other.Real * other.Real + Imaginary * other.Imaginary;
        if (denom == 0.0)
            throw new DivideByZeroException("Деление на ноль (комплексное число с модулем 0).");
        double newReal = (Real * other.Real + Imaginary * other.Imaginary) / denom;
        double newImaginary = (Imaginary * other.Real - Real * other.Imaginary) / denom;
        Real = newReal;
        Imaginary = newImaginary;
    }
    
    public void Modulus() {
        Console.WriteLine(Math.Sqrt(Real * Real + Imaginary * Imaginary));
    }

    public void Argument() {
        Console.WriteLine(Math.Atan2(Imaginary, Real));
    }
    
    public void RealPart() {
        Console.WriteLine(Real);
    }
    
    public void ImaginaryPart() {
        Console.WriteLine(Imaginary);
    }
    
    public void Print() 
    {
        if (Imaginary > 0)
            Console.WriteLine($"{Real} + {Imaginary}i");
        else if  (Imaginary < 0)
            Console.WriteLine($"{Real} - {-Imaginary}i");
        else 
            Console.WriteLine(Real);
    }
    
}