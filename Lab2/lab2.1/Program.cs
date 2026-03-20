using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Головний потiк починає роботу.");

        // Створюємо першу задачу: виводить числа від 1 до 10 з паузою 200 мс
        Task task1 = new Task(() =>
        {
            for (int i = 1; i <= 10; i++)
            {
                Console.WriteLine($"Задача 1: число {i}");
                Thread.Sleep(200);
            }
        });

        // Створюємо другої задачі: виводить літери від A до J з паузою 200 мс
        Task task2 = new Task(() =>
        {
            for (char c = 'A'; c <= 'J'; c++)
            {
                Console.WriteLine($"Задача 2: лiтера {c}");
                Thread.Sleep(200);
            }
        });

        // Запуск обох задач
        task1.Start();
        task2.Start();

        // Очікуємо завершення обох задач
        Task.WaitAll(task1, task2);

        Console.WriteLine("Головний потiк завершив роботу.");
    }
}