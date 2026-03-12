using System;
using System.Diagnostics;
using System.Threading;

namespace Lab1._4
{
    public class ThreadWorker
    {
        public int Id { get; }
        public ThreadPriority Priority { get; }
        public long Target { get; }
        public long Count { get; private set; }
        public long ElapsedMs { get; private set; }
        public bool IsFinished { get; private set; }
        public bool IsWinner { get; private set; }
        public Thread Thread { get; }

        private readonly ManualResetEvent _startSignal;
        private static bool _stop = false;
        private static readonly object _locker = new object();

        public ThreadWorker(int id, ThreadPriority priority, long target, ManualResetEvent startSignal)
        {
            Id = id;
            Priority = priority;
            Target = target;
            _startSignal = startSignal;

            Thread = new Thread(Run)
            {
                Name = $"Потік {id}",
                Priority = priority
            };
        }

        public void Start() => Thread.Start();
        public void Join() => Thread.Join();
        public static void ResetRace() => _stop = false;

        private void Run()
        {
            // Чекаємо сигнал для одночасного старту
            _startSignal.WaitOne();

            var sw = Stopwatch.StartNew();
            long count = 0;

            // Цикл до цільового числа або поки інший потік не завершить
            for (long i = 0; i < Target && !_stop; i++)
            {
                count++;
            }

            sw.Stop();

            Count = count;
            ElapsedMs = sw.ElapsedMilliseconds;
            IsFinished = true;

            // Якщо цей потік досяг цільового числа, зупиняємо інші
            if (count >= Target)
            {
                lock (_locker)
                {
                    _stop = true;
                    IsWinner = true;
                }
            }

            if (ElapsedMs == 0) ElapsedMs = 1;
        }
    }
}