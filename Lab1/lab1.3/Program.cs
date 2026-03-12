using Lab1._3;
using System;
using System.Threading;

namespace Lab1._3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Дослiдження прiоритетiв потокiв");
        

            var startSignal = new ManualResetEvent(false);

            // Скидаємо прапорець зупинки
            CountThread.ResetRace();

            // Створюємо 4 потоки згідно з варіантом 3
            var threads = new[]
            {
                new CountThread("Низький", ThreadPriority.Lowest, startSignal),
                new CountThread("Вище сер.", ThreadPriority.AboveNormal, startSignal),
                new CountThread("Нижче сер.", ThreadPriority.BelowNormal, startSignal),
                new CountThread("Високий", ThreadPriority.Highest, startSignal)
            };

            Console.WriteLine("Запуск потокiв...\n");

            // Запускаємо всі потоки
            foreach (var t in threads)
                t.Start();

            // Даємо сигнал на одночасний старт
            startSignal.Set();

            // Чекаємо завершення всіх потоків
            foreach (var t in threads)
                t.Join();

            // Знаходимо потік-переможець (той, що досяг 100 млн)
            string winner = "";
            foreach (var t in threads)
            {
                if (t.Iterations >= 100_000_000)
                {
                    winner = t.Name;
                    break;
                }
            }

            Console.WriteLine($"\nПотiк-переможець: {winner} (першим досяг 100 млн)");
            Console.WriteLine();

            // Розрахунок загальної кількості ітерацій
            long totalIter = 0;
            foreach (var t in threads)
                totalIter += t.Iterations;

            // Виведення результатів
            Console.WriteLine("РЕЗУЛЬТАТИ ВИКОНАННЯ:");
            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine($"| {"Потiк",-10} | {"Прiоритет",-12} | {"Iтерацiї",12} | {"Час(мс)",8} | {"CPU %",6} |");
            Console.WriteLine("-------------------------------------------------------------------------");

            foreach (var t in threads)
            {
                double percent = (double)t.Iterations / totalIter * 100;
                Console.WriteLine($"| {t.Name,-10} | {t.Priority,-12} | {t.Iterations,12:N0} | {t.ElapsedMs,8} | {percent,5:F2}% |");
            }

            Console.WriteLine("-------------------------------------------------------------------------");
            Console.WriteLine($"| {"ВСЬОГО",-10} | {"",-12} | {totalIter,12:N0} | {"",8} | {"100%",6} |");
            Console.WriteLine("-------------------------------------------------------------------------");

            Console.ReadLine();
        }
    }
}