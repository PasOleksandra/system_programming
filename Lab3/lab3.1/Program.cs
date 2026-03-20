using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace lab3._1
{
    class Program
    {
        static double[] arr;
        static int[] arrInt;

        static void Operation1(int index) => arr[index] = arr[index] / 10;
        static void Operation2(int index) => arr[index] = arr[index] / Math.PI;
        static void Operation3(int index) => arr[index] = Math.Exp(arr[index]) / Math.Pow(arr[index], Math.PI);
        static void Operation4(int index) => arr[index] = Math.Exp(Math.PI * arr[index]) / Math.Pow(arr[index], Math.PI);

        static void OpInt1(int index) => arrInt[index] = arrInt[index] / 10;

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            int[] testSizes = { 10000000, 50000000 };

            var doubleCalc = new (string name, Action<int> func)[]
            {
                ("double: /10", Operation1),
                ("double: /π", Operation2),
                ("double: e^x/x^π", Operation3),
                ("double: e^(πx)/x^π", Operation4)
            };

            Console.WriteLine("ДОСЛІДЖЕННЯ ПАРАЛЕЛЬНИХ ОБЧИСЛЕНЬ\n");

            // DOUBLE 
            foreach (int length in testSizes)
            {
                Console.WriteLine($"\nМасив double: {length} елементів");
                arr = new double[length];

                for (int i = 0; i < doubleCalc.Length; i++)
                {
                    for (int j = 0; j < length; j++) arr[j] = j + 1;

                    var timer = new Stopwatch();

                    timer.Start();
                    for (int j = 0; j < length; j++) doubleCalc[i].func(j);
                    timer.Stop();
                    double seqTime = timer.Elapsed.TotalSeconds;

                    for (int j = 0; j < length; j++) arr[j] = j + 1;

                    timer.Reset();
                    timer.Start();
                    Parallel.For(0, length, doubleCalc[i].func);
                    timer.Stop();
                    double parTime = timer.Elapsed.TotalSeconds;

                    Console.WriteLine($"{doubleCalc[i].name,-18} посл.: {seqTime,5:F3}с парал.: {parTime,5:F3}с");
                }
            }

            // INT 
            foreach (int length in testSizes)
            {
                Console.WriteLine($"\nМасив int: {length} елементів");
                arrInt = new int[length];

                for (int j = 0; j < length; j++) arrInt[j] = j + 1;

                var timer = new Stopwatch();

                timer.Start();
                for (int j = 0; j < length; j++) OpInt1(j);
                timer.Stop();
                double seqTime = timer.Elapsed.TotalSeconds;

                for (int j = 0; j < length; j++) arrInt[j] = j + 1;

                timer.Reset();
                timer.Start();
                Parallel.For(0, length, OpInt1);
                timer.Stop();
                double parTime = timer.Elapsed.TotalSeconds;

                Console.WriteLine($"int: x/10               посл.: {seqTime,5:F3}с парал.: {parTime,5:F3}с");
            }

            Console.ReadKey();
        }
    }
}