using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1._1
{
    class LetterThread
    {
        public Thread Thrd;

        public LetterThread(string name)
        {
            Thrd = new Thread(Run);
            Thrd.Name = name;
        }

        public void Start()
        {
            Thrd.Start();
        }

        void Run()
        {
            for (char c = 'A'; c <= 'Z'; c++)
            {
                Console.WriteLine(Thrd.Name + ": " + c);
                Thread.Sleep(300);
            }
            Console.WriteLine(Thrd.Name + " завершив роботу");
        }
    }
}

