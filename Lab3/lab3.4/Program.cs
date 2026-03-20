using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace lab3._4
{
    class Program
    {
        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int size = 10000000;
            var data = new List<double>();
            for (int i = 1; i <= size; i++) data.Add(i);

            Console.WriteLine("Parallel.ForEach() з лямбда-виразом\n");

            // Послідовне виконання
            var sw = Stopwatch.StartNew();
            foreach (var x in data)
            {
                double res = x / 10;
            }
            sw.Stop();
            Console.WriteLine($"Послiдовно: {sw.Elapsed.TotalSeconds:F3}с");

            // Паралельне з лямбда-виразом
            sw.Restart();
            Parallel.ForEach(data, x =>
            {
                double res = x / 10;
            });
            sw.Stop();
            Console.WriteLine($"Паралельно (лямбда): {sw.Elapsed.TotalSeconds:F3}с");

           

            Console.ReadKey();
        }
    }
}