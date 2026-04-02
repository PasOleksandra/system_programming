using System;
using System.IO;

class Program
{
    static void Main()
    {
        Console.Write("Введiть шлях до папки: ");
        string path = Console.ReadLine();

        Console.Write("Введiть назву файлу для пошуку: ");
        string fileName = Console.ReadLine();

        if (Directory.Exists(path))
        {
            DirectoryInfo dir = new DirectoryInfo(path);

            Console.WriteLine("\nРезультати пошуку:\n");

            SearchFile(dir, fileName);
        }
        else
        {
            Console.WriteLine("Папка не знайдена!");
        }

        Console.ReadKey();
    }

    static void SearchFile(DirectoryInfo dir, string fileName)
    {
        try
        {
            // Пошук файлів у поточній папці
            foreach (FileInfo file in dir.GetFiles())
            {
                if (file.Name.Equals(fileName, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("Файл знайдено:");
                    Console.WriteLine("Назва: " + file.Name);
                    Console.WriteLine("Повний шлях: " + file.FullName);
                    Console.WriteLine("Розмiр: " + file.Length + " байт");
                    Console.WriteLine("Дата створення: " + file.CreationTime);
                    Console.WriteLine("Остання змiна: " + file.LastWriteTime);
                    Console.WriteLine();
                }
            }

            // Пошук у підпапках
            foreach (DirectoryInfo subDir in dir.GetDirectories())
            {
                SearchFile(subDir, fileName);
            }
        }
        catch (UnauthorizedAccessException)
        {
            Console.WriteLine("Немає доступу до папки: " + dir.FullName);
        }
    }
}