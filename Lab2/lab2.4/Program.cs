using System;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    static void Main()
    {
        Console.WriteLine("Головний потiк починає роботу.");
        Console.WriteLine($"Id головного потоку: {Thread.CurrentThread.ManagedThreadId}\n");

        // Введення даних користувачем
        Console.Write("Введiть число для обчислення факторiала: ");
        int factNumber = int.Parse(Console.ReadLine());

        Console.Write("Введiть число N для обчислення суми вiд 1 до N: ");
        int sumNumber = int.Parse(Console.ReadLine());

        Console.WriteLine("\nЗапуск паралельного виконання методiв...\n");

        // Паралельне виконання трьох методів
        Parallel.Invoke(
            () => Method1_Factorial(factNumber),
            () => Method2_Sum(sumNumber),
            () => Method3_Message()
        );

        Console.WriteLine("\nВсi методи завершили виконання.");
        Console.WriteLine("Головний потiк завершив роботу.");
    }

    // Метод 1: обчислення факторіала числа
    static void Method1_Factorial(int n)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Метод 1 (факторiал) стартував на потоцi {Thread.CurrentThread.ManagedThreadId}");

        long factorial = 1;
        for (int i = 1; i <= n; i++)
        {
            factorial *= i;
            Thread.Sleep(100); // Невелика затримка для наочності
        }

        Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Метод 1: факторiал числа {n} = {factorial}");
    }

    // Метод 2: обчислення суми чисел від 1 до N
    static void Method2_Sum(int n)
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Метод 2 (сума) стартував на потоцi {Thread.CurrentThread.ManagedThreadId}");

        long sum = 0;
        for (int i = 1; i <= n; i++)
        {
            sum += i;
            Thread.Sleep(100); // Невелика затримка для наочності
        }

        Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Метод 2: сума вiд 1 до {n} = {sum}");
    }

    // Метод 3: виведення повідомлень з паузою 300 мс
    static void Method3_Message()
    {
        Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Метод 3 (повiдомлення) стартував на потоцi {Thread.CurrentThread.ManagedThreadId}");

        string[] messages = {
            "Перше повiдомлення",
            "Друге повiдомлення",
            "Третє повiдомлення",
            "Четверте повiдомлення",
            "П'яте повiдомлення"
        };

        foreach (string msg in messages)
        {
            Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Метод 3: {msg}");
            Thread.Sleep(300); // Пауза 300 мс
        }

        Console.WriteLine($"[{DateTime.Now:HH:mm:ss.fff}] Метод 3 завершив виведення повiдомлень");
    }
}