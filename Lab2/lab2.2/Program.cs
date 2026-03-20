using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Головний потiк починає роботу.");
        Console.WriteLine($"Головний потiк ID: {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine();

        // Створюємо три задачі
        Task task1 = new Task(CountToFive);
        Task task2 = new Task(CountToFive);
        Task task3 = new Task(CountToFive);

        // Виводимо Id задач до запуску
        Console.WriteLine($"Id задачi 1 до запуску: {task1.Id}");
        Console.WriteLine($"Id задачi 2 до запуску: {task2.Id}");
        Console.WriteLine($"Id задачi 3 до запуску: {task3.Id}");
        Console.WriteLine();

        // Запускаємо задачі
        task1.Start();
        task2.Start();
        task3.Start();

        // Очікуємо завершення всіх задач
        Task.WaitAll(task1, task2, task3);

        Console.WriteLine();
        Console.WriteLine("Всi задачi завершено.");
        Console.WriteLine("Головний потiк завершив роботу.");
    }

    static void CountToFive()
    {
        // Виводимо Id задачі та поточний потік
        Console.WriteLine($"Задача {Task.CurrentId} стартувала на потоцi {Thread.CurrentThread.ManagedThreadId}");

        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"Задача {Task.CurrentId}: лiчба = {i}");
            Thread.Sleep(300); // Невелика затримка, щоб побачити паралелізм
        }

        Console.WriteLine($"Задача {Task.CurrentId} завершилась");
    }
}