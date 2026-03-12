using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1._1
{
   
    class NumberThread
    {
        public Thread Thrd;

        public NumberThread(string name)
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
            for (int i = 1; i <= 40; i++)
            {
                Console.WriteLine(Thrd.Name + ": " + i);
                Thread.Sleep(200);
            }
            Console.WriteLine(Thrd.Name + " завершив роботу");
        }
    }
}