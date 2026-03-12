using Lab1._1;
using System;
using System.Threading;

namespace Lab1._1
{
    class Program
    {
        static void Main(string[] args)
        {
       

            // Створюємо потоки
            NumberThread nt = new NumberThread("Потiк чисел");
            LetterThread lt = new LetterThread("Потiк букв");

            // Запускаємо потоки (використовуємо Start())
            nt.Start();
            lt.Start();

            // Чекаємо завершення потоків
            nt.Thrd.Join();
            lt.Thrd.Join();

            Console.WriteLine("Завдання закiнчено");
            Console.ReadLine();
        }
    }
}