using System;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Головний потiк починає роботу.");

        // Введення числа N користувачем
        Console.Write("Введiть число N: ");
        int N = int.Parse(Console.ReadLine());

        // 1. Створюємо задачу для обчислення суми чисел від 1 до N
        Task<long> sumTask = new Task<long>(() =>
        {
            long sum = 0;
            for (int i = 1; i <= N; i++)
            {
                sum += i;
            }
            return sum;
        });

        // 2. Використовуємо ContinueWith() для запуску другої задачі
        // 3. Лямбда-вираз для реалізації продовження
        Task continueTask = sumTask.ContinueWith((previousTask) =>
        {
            long result = previousTask.Result;
            Console.WriteLine($"Сума чисел вiд 1 до {N} = {result}");
        });

        // Запускаємо першу задачу
        sumTask.Start();

        // Очікуємо завершення продовження
        continueTask.Wait();

        Console.WriteLine("Головний потiк завершив роботу.");
    }
}