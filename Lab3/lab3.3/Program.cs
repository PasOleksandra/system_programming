using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace lab3._3
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int size = 10000000;
            var numbers = new List<double>();
            for (int i = 1; i <= size; i++) numbers.Add(i);

            Console.WriteLine("Parallel.ForEach()\n");

            var sw = Stopwatch.StartNew();
            foreach (var x in numbers) { double res = x / 10; }
            sw.Stop();
            Console.WriteLine($"Послiдовно: {sw.Elapsed.TotalSeconds:F3}с");

            sw.Restart();
            Parallel.ForEach(numbers, x => { double res = x / 10; });
            sw.Stop();
            Console.WriteLine($"Паралельно: {sw.Elapsed.TotalSeconds:F3}с");
        }
    }
}