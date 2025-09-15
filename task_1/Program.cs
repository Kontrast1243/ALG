using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Task_1;

class Tensor
{
    private double[,] g;
    
    public Tensor(double[,] g)
    {
        if (!isSimetryc(g))
            throw new Exception("Матрица не симметрична");
        this.g = g;
    }
    private bool isSimetryc(double[,] g)
    {
        for (int i = 0; i < g.GetLength(0); i++)
        {
            for (int j = 0; j < g.GetLength(1); j++)
            {
                if (g[i, j] != g[j, i]) return false;
            }
        }
        return true;
    }

    public double get_element(int i, int j)
    {
        return g[i, j];
    }
}

class Vector
{
    private double[] x;
    private Tensor tensor;
    public Vector(double[] coor, Tensor tensor)
    {
        this.x = coor;
        this.tensor = tensor;
    }

    public double len(Tensor tensor)
    {
        double sum = 0;
        for (int i = 0; i < x.Length; i++)
        {
            for (int j = 0; j < x.Length; j++)
            {
                sum += x[i] * tensor.get_element(i,j) * x[j];
            }
        }
        return Math.Sqrt(sum);
    }
}

class Program
{
    static void Main()
    {
        using StreamReader sr = new StreamReader("/Users/mihailprohorov/Desktop/учеба/C#/Task_1/f.txt");
        
        int N = int.Parse(sr.ReadLine());
        
        double[,] G = new double[N, N];
        for (int i = 0; i < N; i++)
        {
            string[] line = sr.ReadLine().Split(' ');
            for  (int j = 0; j < N; j++)
                G[i, j] = double.Parse(line[j]);
        }
        
        Tensor tensor = new Tensor(G);
        
        double [] x  = sr.ReadLine().Split().Select(double.Parse).ToArray();
        Vector v = new Vector(x, tensor);
        
        Console.WriteLine(v.len(tensor));
        
    }
}