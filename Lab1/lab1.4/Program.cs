using Lab1._4;
using System;
using System.Collections.Generic;
using System.Threading;

namespace Lab1._4
{
    internal class Program
    {
        static void Main(string[] args)
        {
           

            // Введення даних від користувача
            Console.Write("Введiть цiльове число для пiдрахунку (наприклад, 10000000): ");
            long target = long.Parse(Console.ReadLine());

            Console.Write("Введiть кiлькiсть потокiв: ");
            int numThreads = int.Parse(Console.ReadLine());

            var startSignal = new ManualResetEvent(false);
            var workers = new List<ThreadWorker>();

            // Скидаємо прапорець зупинки
            ThreadWorker.ResetRace();

            // Створення потоків з вибором пріоритетів
            for (int i = 1; i <= numThreads; i++)
            {
                Console.WriteLine($"\nВиберiть прiоритет для потоку {i}:");
                Console.WriteLine("1 - Lowest (Найнижчий)");
                Console.WriteLine("2 - BelowNormal (Нижче середнього)");
                Console.WriteLine("3 - Normal (Середнiй)");
                Console.WriteLine("4 - AboveNormal (Вище середнього)");
                Console.WriteLine("5 - Highest (Найвищий)");
                Console.Write("Ваш вибiр (1-5): ");

                int choice = int.Parse(Console.ReadLine());
                ThreadPriority priority = choice switch
                {
                    1 => ThreadPriority.Lowest,
                    2 => ThreadPriority.BelowNormal,
                    3 => ThreadPriority.Normal,
                    4 => ThreadPriority.AboveNormal,
                    5 => ThreadPriority.Highest,
                    _ => ThreadPriority.Normal
                };

                workers.Add(new ThreadWorker(i, priority, target, startSignal));
            }

            Console.Clear();
            Console.WriteLine("=== ЗАПУСК ПОТОКIВ ===\n");
            Console.WriteLine($"Цiльове число: {target:N0}");
            Console.WriteLine($"Кiлькiсть потокiв: {numThreads}\n");

            // Запуск всіх потоків
            foreach (var w in workers)
            {
                w.Start();
                Console.WriteLine($"Потiк {w.Id} ({w.Priority}) створено");
            }

            Console.WriteLine("\nНатиснiть Enter для одночасного старту всiх потокiв...");
            Console.ReadLine();

            // Даємо сигнал на старт
            startSignal.Set();
            Console.WriteLine("ПОТОКИ ЗАПУЩЕНО!\n");

            // Моніторинг виконання в реальному часі
            int startRow = Console.CursorTop;
            bool allFinished = false;

            while (!allFinished)
            {
                allFinished = true;
                Console.SetCursorPosition(0, startRow);

                foreach (var w in workers)
                {
                    string status;
                    if (w.IsWinner) status = "ПЕРЕМОЖЕЦЬ";
                    else if (w.IsFinished) status = "ЗУПИНЕНО";
                    else status = "ВИКОНУЄТЬСЯ";

                    double progress = (double)w.Count / target * 100;

                    Console.WriteLine($"Потiк {w.Id} ({w.Priority,-12}) | Статус: {status,-11} | Iтерацiй: {w.Count,12:N0} | Прогрес: {progress,6:F2}%");

                    if (!w.IsFinished) allFinished = false;
                }

                Thread.Sleep(100);
            }

            // Чекаємо повного завершення всіх потоків
            foreach (var w in workers)
                w.Join();

            // Виведення фінальних результатів
            Console.WriteLine("\n\n=== РЕЗУЛЬТАТИ ВИКОНАННЯ ===");
            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"{"Потiк",-8} {"Прiоритет",-15} {"Iтерацiї",15} {"Час(мс)",10} {"CPU %",8} {"Статус",-12}");
            Console.WriteLine("----------------------------------------------------------------------------");

            long totalIter = 0;
            foreach (var w in workers)
                totalIter += w.Count;

            foreach (var w in workers)
            {
                double cpuPercent = (double)w.Count / totalIter * 100;
                string status = w.IsWinner ? "ПЕРЕМОЖЕЦЬ" : "ЗУПИНЕНО";
                Console.WriteLine($"Потiк {w.Id,-4} {w.Priority,-15} {w.Count,15:N0} {w.ElapsedMs,10} {cpuPercent,7:F2}% {status,-12}");
            }

            Console.WriteLine("----------------------------------------------------------------------------");
            Console.WriteLine($"{"ВСЬОГО",-8} {"",-15} {totalIter,15:N0} {"",10} {"100%",8} {"",-12}");
            Console.WriteLine("----------------------------------------------------------------------------");

            Console.WriteLine("\nНатиснiть Enter для виходу...");
            Console.ReadLine();
        }
    }
}