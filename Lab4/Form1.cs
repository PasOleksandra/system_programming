using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace lab4
{
    public partial class Form1 : Form
    {
        // ЗАВДАННЯ 4: Джерело токена для скасування операції
        private CancellationTokenSource cts;

        // Прапорець для відстеження скасування
        private bool isCancelled = false;

        // Зберігаємо останній прогрес
        private int lastProgress = 0;

        public Form1()
        {
            InitializeComponent();
            buttonCancel.Enabled = false;
        }

        // ЗАВДАННЯ 1: Кнопка Start
        private async void buttonStart_Click(object sender, EventArgs e)
        {
            isCancelled = false;
            lastProgress = 0;

            buttonStart.Enabled = false;
            buttonCancel.Enabled = true;

            labelStatus.Text = "Операція виконується...";
            labelPercent.Text = "0%";
            progressBar.Value = 0;
            labelResult.Text = "Результат: обчислюється...";

            // ЗАВДАННЯ 3: IProgress для відображення прогресу
            IProgress<int> onChangeProgress = new Progress<int>((i) =>
            {
                if (!isCancelled)
                {
                    labelPercent.Text = i.ToString() + "%";
                    progressBar.Value = i;
                    lastProgress = i;
                }
            });

            cts = new CancellationTokenSource();

            try
            {
                int result = await Process(100, onChangeProgress, cts.Token);

                // ЗАВДАННЯ 2: Результат (якщо не скасовано)
                if (!isCancelled)
                {
                    labelResult.Text = $"Результат: {result}";
                    labelStatus.Text = "Операція завершена!";
                }
            }
            
            finally
            {
                buttonStart.Enabled = true;
                buttonCancel.Enabled = false;
                cts?.Dispose();
                cts = null;
            }
        }

        // ЗАВДАННЯ 4: Кнопка Cancel
        private void buttonCancel_Click(object sender, EventArgs e)
        {
            if (cts != null && !isCancelled)
            {
                // Встановлюємо прапорець
                isCancelled = true;

                labelResult.Text = $"Результат: ПЕРЕРВАНО (зупинено на {lastProgress}%)";
                labelStatus.Text = "Операцію перервано!";

                // Скасовуємо операцію
                cts.Cancel();
            }
        }

        // Метод виконання операції
        private Task<int> Process(int count, IProgress<int> ChangeProgressBar, CancellationToken cancellToken)
        {
            return Task.Run(() =>
            {
                int i;
                for (i = 1; i <= count; i++)
                {
                    if (cancellToken.IsCancellationRequested)
                    {
                        break;
                    }
                    ChangeProgressBar.Report(i);
                    Thread.Sleep(50);
                }
                return i - 1;
            });
        }
    }
}