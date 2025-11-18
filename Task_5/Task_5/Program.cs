using System;

namespace contest_1;

class BinHeap {
    private List<int> heap;

    public BinHeap() {
        int[] a = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
        heap = new List<int>(a);
        int statI = a.Length / 2 - 1;
        for (int i = statI; i >= 0; i--) Sift_down(i);
    }
    private void Sift_up(int i) {
        while (i > 0) {
            int parentI = (i - 1) / 2;
            if (heap[i] > heap[parentI]) {
                (heap[i], heap[parentI]) = (heap[parentI], heap[i]); i = parentI;
            } 
            else break;
        }
    }

    private void Sift_down(int i) {
        int n =  heap.Count;
        while (2 * i + 1 < n) {
            int left = 2 * i + 1;
            int right = 2 * i + 2;
            int maxСhild = left;
            if (right < n && heap[right] > heap[maxСhild]) {
                maxСhild = right;
            }
            if (heap[i] >= heap[maxСhild]) break;
            else {(heap[i], heap[maxСhild]) = (heap[maxСhild], heap[i]); i =  maxСhild;}
        }
    }

    public int Top() {
        if (heap.Count == 0) 
            throw new Exception("Heap is empty");
        return heap[0];
    }

    public void Push(int x) {
        heap.Add(x);
        Sift_up(heap.Count - 1);
    }
    
    public int Pop() {
        if (heap.Count == 0)
            throw new Exception("Heap is empty");
        int ans;
        if (heap.Count == 1) {
            ans = heap[0];
            heap.RemoveAt(0);
            return ans;
        }
        int lastElement = heap[heap.Count - 1];
        heap.RemoveAt(heap.Count - 1);
        ans = heap[0];
        heap[0] = lastElement;
        Sift_down(0);
        return ans;
    }

    public void Increasing(int i, int v) {
        if (heap[i] <= v) 
            throw new Exception("Value is not ok :(");
        heap[i] = v;
        Sift_up(i);
    }

    public void MergeHeaps(BinHeap heap2) {
        for (int i = 0; i < heap2.Count; i++) {
            heap2.Push(heap2.Pop());
        }
    }
    
    public int Count => heap.Count;

    private int Log2(int num) {
        int ans = 0;
        while (num > 0) { ans++; num /= 2;}
        return ans;
    }

    public void Print() {
        if (heap.Count == 0) 
            throw new Exception("Heap is empty");
        Console.WriteLine(heap[0]);
        for (int i = 1; i < heap.Count; i++) {
            Console.Write($"{heap[i]} ");
            if (Log2(i) != Log2(i+1)) Console.WriteLine();
        }
    }
}

public class Program
{
    static void Main() {
        
        Console.WriteLine("Input elements");
        BinHeap heap = new BinHeap();
        heap.Print();
    }


}