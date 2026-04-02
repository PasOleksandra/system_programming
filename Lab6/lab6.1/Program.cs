using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Введiть шлях до папки: ");
        string path = Console.ReadLine();

        // Перевірка існування папки
        if (Directory.Exists(path))
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            Console.WriteLine("\nСтруктура папки:\n");

            // Виклик методу
            ShowDirectory(dir, 0);
        }
        else
        {
            Console.WriteLine("Папка не знайдена!");
        }

        Console.ReadKey();
    }

    // Рекурсивний метод
    static void ShowDirectory(DirectoryInfo dir, int level)
    {
        // Вивід папки
        Console.WriteLine(new string(' ', level * 2) + "[Папка] " + dir.Name);

        try
        {
            // Вивід файлів
            foreach (FileInfo file in dir.GetFiles())
            {
                Console.WriteLine(new string(' ', (level + 1) * 2) + file.Name);
            }

            // Вивід підпапок
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                ShowDirectory(subDir, level + 1);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine(new string(' ', (level + 1) * 2) + "(немає доступу)");
        }
    }
}