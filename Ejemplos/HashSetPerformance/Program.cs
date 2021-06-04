using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HashSetPerformance
{
    class Program
    {
        const int LENGTH = 10_000_000;

        static string[] values_array;
        static List<string> values_list;
        static HashSet<string> values_set;
        
        static Random rng = new Random();

        static void Main(string[] args)
        {
            InitValues();
            var sw = new Stopwatch();
            while (true)
            {
                try
                {
                    Console.WriteLine("------------");
                    Console.WriteLine($"Choose number between 1 and {LENGTH}:");
                    var selection = Console.ReadLine();
                    Console.WriteLine();

                    {
                        Console.WriteLine("Searching array...");
                        sw.Restart();
                        var found = values_array.Contains(selection);
                        sw.Stop();
                        Console.WriteLine($"{(found ? "Found" : "Not found")}. Time searching: {sw.ElapsedMilliseconds} ms");
                        Console.WriteLine();
                    }

                    {
                        Console.WriteLine("Searching list...");
                        sw.Restart();
                        var found = values_list.Contains(selection);
                        sw.Stop();
                        Console.WriteLine($"{(found ? "Found" : "Not found")}. Time searching: {sw.ElapsedMilliseconds} ms");
                        Console.WriteLine();
                    }

                    {
                        Console.WriteLine("Searching set...");
                        sw.Restart();
                        var found = values_set.Contains(selection);
                        sw.Stop();
                        Console.WriteLine($"{(found ? "Found" : "Not found")}. Time searching: {sw.ElapsedMilliseconds} ms");
                        Console.WriteLine();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    Console.WriteLine();
                }
            }
        }

        static void InitValues()
        {
            Console.WriteLine("Initializing array...");
            values_array = new string[LENGTH];
            for (long i = 0; i < LENGTH; i++)
            {
                values_array[i] = (i + 1).ToString();
            }
            Console.WriteLine("Shuffling values...");
            Shuffle(values_array);

            Console.WriteLine("Initializing list...");
            values_list = values_array.ToList();

            Console.WriteLine("Initializing set...");
            values_set = values_array.ToHashSet();
        }

        static void Shuffle<T>(T[] array)
        {
            long currentIndex = array.Length;
            while (0 != currentIndex)
            {
                long randomIndex = (long)(rng.NextDouble() * currentIndex);
                currentIndex--;

                T temporaryValue = array[currentIndex];
                array[currentIndex] = array[randomIndex];
                array[randomIndex] = temporaryValue;
            }
        }
    }
}
