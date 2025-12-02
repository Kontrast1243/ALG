using System;
using System.IO;

class Program {
    static bool IsValidIPv4(string ip) {
        foreach (char c in ip) {
            if (!(char.IsDigit(c) || c == '.'))
                return false;
        }

        string[] parts = ip.Split('.');
        if (parts.Length != 4) return false;

        foreach (string part in parts) {
            if (part.Length == 0 || part.Length > 3) return false;
            
            if (part.Length > 1 && part[0] == '0') return false;
            
            if (!int.TryParse(part, out int num)) return false;
            
            if (num < 0 || num > 255) return false;
        }

        return true;
    }

    static bool Intersection(string text, int start, int length) {
        if (start > 0) {
            char leftChar = text[start - 1];
            if (char.IsDigit(leftChar) || leftChar == '.')
                return true;
        }

        if (start + length < text.Length) {
            char rightChar = text[start + length];
            if (char.IsDigit(rightChar) || rightChar == '.')
                return true;
        }

        return false;
    }

    static void Main() {
        MyVector<string> linesVector = new MyVector<string>();
        
        string[] lines = File.ReadAllLines("/Users/mihailprohorov/Desktop/учеба/C#/task_11/task_11/input.txt");
        foreach (string line in lines) {
            linesVector.Add(line);
        }

        MyVector<string> ipVector = new MyVector<string>();

        for (int i = 0; i < linesVector.Size(); i++) {
            string line = linesVector.Get(i);
            for (int j = 0; j < line.Length; j++) {
                if (char.IsDigit(line[j])) {
                    int k = j;
                    while (k < line.Length && (char.IsDigit(line[k]) || line[k] == '.')) {
                        k++;
                    }
                    
                    string potentialIp = line.Substring(j, k - j);
                    
                    if (IsValidIPv4(potentialIp)) {
                        if (!Intersection(line, j, potentialIp.Length)) {
                            ipVector.Add(potentialIp);
                        }
                    }
                    
                    j = k - 1;
                }
            }
        }

        using (StreamWriter writer = new StreamWriter("/Users/mihailprohorov/Desktop/учеба/C#/task_11/task_11/output.txt")) {
            for (int i = 0; i < ipVector.Size(); i++) {
                writer.WriteLine(ipVector.Get(i));
            }
        }

        Console.WriteLine($"Найдено {ipVector.Size()} IP-адресов. Результат записан в output.txt");
    }
}

//      /\___/\
//     |  o_O |   wtf????
//--------------------------