using System;
using System.Threading;

namespace Lab1._2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Головний потiк стартував");

            // Створюємо два пріоритетні потоки
            Thread t1 = new Thread(PriorityWork);
            t1.Name = "Прiоритетний 1";

            Thread t2 = new Thread(PriorityWork);
            t2.Name = "Прiоритетний 2";

            // Створюємо фоновий потік
            Thread t3 = new Thread(BackgroundWork);
            t3.Name = "Фоновий";
            t3.IsBackground = true;

            // Запускаємо всі потоки
            t1.Start();
            t2.Start();
            t3.Start();

            // Чекаємо завершення тільки пріоритетних потоків
            t1.Join();
            t2.Join();

            Console.WriteLine("Прiоритетнi потоки завершились");
            Console.WriteLine("Головний потiк завершується");
            Console.WriteLine("Фоновий потiк зупиниться автоматично");

            
        }

        static void PriorityWork()
        {
            for (int i = 1; i <= 5; i++)
            {
                Console.WriteLine(Thread.CurrentThread.Name + ": " + i);
                Thread.Sleep(500);
            }
            Console.WriteLine(Thread.CurrentThread.Name + " завершився");
        }

        static void BackgroundWork()
        {
            int count = 0;
            while (true)
            {
                count++;
                Console.WriteLine(Thread.CurrentThread.Name + ": " + count);
                Thread.Sleep(700);
            }
        }
    }
}