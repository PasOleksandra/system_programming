using System;
using System.Threading.Tasks;

namespace lab3._2
{
    class Program
    {
        static double[] array;
        static double targetNumber = 500;
        static double tolerance = 0.5;

        static void ElementHandler(int position, ParallelLoopState loopState)
        {
            array[position] = position * 1.8 + Math.Pow(position, 0.3);

            if (Math.Abs(array[position] - targetNumber) <= tolerance)
            {
                Console.WriteLine($"Збіг! Позицiя: {position}, Величина: {array[position]:F3}");
                loopState.Break();
            }
        }

        static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("Пошук елемента в масивi...\n");

            array = new double[10000];

            ParallelLoopResult loopResult = Parallel.For(0, array.Length, ElementHandler);

            if (!loopResult.IsCompleted)
            {
                Console.WriteLine($"\nПроцес зупинено. Номер iтерацiї: {loopResult.LowestBreakIteration}");
            }
            else
            {
                Console.WriteLine("\nЕлемент не виявлено, обробка завершена.");
            }

            Console.ReadLine();
        }
    }
}