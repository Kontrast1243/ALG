using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;

class Program {
    static void Main(string[] args) {
        string inputFile = "/Users/mihailprohorov/Desktop/учеба/C#/task_9/task_9/Input.txt";
        if (!File.Exists(inputFile)) {
            Console.WriteLine($"Файл \"{inputFile}\" не найден.");
            return;
        }

        MyArrayList<string> allTags = new MyArrayList<string>();

        Regex regex = new Regex(@"<\/?[A-Za-z][A-Za-z0-9]*>");

        foreach (string line in File.ReadLines(inputFile)) {
            if (string.IsNullOrEmpty(line)) continue;

            MatchCollection matches = regex.Matches(line);
            foreach (Match m in matches) {
                string tag = m.Value;
                allTags.Add(tag);
            }
        }

        MyArrayList<string> uniqueTags = new MyArrayList<string>();
        MyArrayList<string> Normal = new MyArrayList<string>();

        for (int i = 0; i < allTags.Size(); i++) {
            string tag = allTags.Get(i);

            string inner = tag.Substring(1, tag.Length - 2); // без <>
            if (inner.StartsWith("/")) // / тоже убирем если есть 
                inner = inner.Substring(1);
            string normalized = inner.ToLowerInvariant(); // приведем к нижнему регистру

            if (!Normal.Contains(normalized)) {
                Normal.Add(normalized);
                uniqueTags.Add(tag); 
            }
        }

        for (int i = 0; i < uniqueTags.Size(); i++) {
            Console.WriteLine(uniqueTags.Get(i));
        }
    }
}
