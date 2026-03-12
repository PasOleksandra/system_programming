using System;
using System.Diagnostics;
using System.Threading;

namespace Lab1._3
{
    public class CountThread
    {
        // Поля
        private string _name;
        private ThreadPriority _priority;
        private ManualResetEvent _signal;
        private Thread _thread;
        private long _counter;
        private long _time;
        private Stopwatch _watch;
        private static bool _stop = false;
        private static readonly object _locker = new object();

        // Конструктор
        public CountThread(string name, ThreadPriority priority, ManualResetEvent signal)
        {
            _name = name;
            _priority = priority;
            _signal = signal;
            _counter = 0;
            _time = 0;
            _watch = new Stopwatch();

            _thread = new Thread(Execute);
            _thread.Name = name;
            _thread.Priority = priority;
        }

        // Властивості для доступу до даних
        public string Name => _name;
        public ThreadPriority Priority => _priority;
        public long Iterations => _counter;
        public long ElapsedMs => _time;
        public Thread Thread => _thread;

        // Методи
        public void Start() => _thread.Start();
        public void Join() => _thread.Join();

        // Скидання прапорця для нового запуску
        public static void ResetRace() => _stop = false;

        // Виконання
        private void Execute()
        {
            // Чекаємо сигнал для одночасного старту
            _signal.WaitOne();

            // Починаємо вимірювання
            _watch.Reset();
            _watch.Start();

            // Цикл до 100 млн або поки інший потік не завершить
            for (long i = 0; i < 100_000_000 && !_stop; i++)
            {
                _counter++;
            }

            // Зупиняємо вимірювання
            _watch.Stop();
            _time = _watch.ElapsedMilliseconds;

            // Якщо цей потік досяг 100 млн, зупиняємо інші
            if (_counter >= 100_000_000)
            {
                lock (_locker)
                {
                    _stop = true;
                }
            }

            // Запобіжник від ділення на нуль
            if (_time == 0) _time = 1;
        }
    }
}