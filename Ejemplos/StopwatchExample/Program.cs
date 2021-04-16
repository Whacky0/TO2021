using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace StopwatchExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = new Stopwatch();
            for (int i = 0; i <= 100; i++)
            {
                sw.Restart();
                int f = FibMemo(i);
                sw.Stop();
                Console.WriteLine($"{i} - {f} ({sw.ElapsedMilliseconds} ms)");
            }
        }

        static Dictionary<int, int> calculatedValues = new Dictionary<int, int>();
        static int FibMemo(int n) // Memoization
        {
            // 0 1 1 2 3 5 8 ...
            if (n < 0) throw new ArgumentException("No válido para negativos");
            if (n <= 1) return n;
            if (calculatedValues.ContainsKey(n)) return calculatedValues[n];
            calculatedValues[n] = FibMemo(n - 1) + FibMemo(n - 2);
            return calculatedValues[n];
        }

        static int FibIter(int n) // Proceso iterativo
        {
            if (n < 0) throw new ArgumentException("No válido para negativos");
            if (n <= 1) return n;
            int a1 = 0;
            int a2 = 1;
            for (int i = 1; i < n; i++)
            {
                int temp = a2 + a1;
                a1 = a2;
                a2 = temp;
            }
            return a2;
        }

        static int Fib(int n) // Proceso recursivo
        {
            // 0 1 1 2 3 5 8 ...
            if (n < 0) throw new ArgumentException("No válido para negativos");
            if (n <= 1) return n;
            return Fib(n - 1) + Fib(n - 2);
        }
    }
}
