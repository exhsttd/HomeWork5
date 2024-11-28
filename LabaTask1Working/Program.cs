using System;
using System.IO;

class Program
{
    static void Main(string[] args)
    {
        string[] testArgs = { "Laba.txt" }; 
        Task1(testArgs);
    }

    static void Task1(string[] args1)
    {
        Console.WriteLine("Упражнение 6.1");
        string basePath = Path.Combine(Directory.GetCurrentDirectory(), "resources");
        
        Console.WriteLine($"Путь к ресурсам: {basePath}");

        if (!Directory.Exists(basePath))
        {
            Console.WriteLine("Папка с ресурсами не найдена.");
            return;
        }
        
        if (args1.Length == 0)
        {
            Console.WriteLine("Не указаны файлы для обработки.");
            return;
        }

        foreach (string arg in args1)
        {
            string filePath = Path.Combine(basePath, arg);
            char[] text = OpenAndReadF(filePath)?.ToCharArray()!;

            if (text != null)
            {
                Console.WriteLine($"Файл {arg}:");
                CountLetters(text);
            }
        }
    }

    static void CountLetters(char[] letters)
    {
        long vowelsCount = 0;
        long consonantsCount = 0;
        foreach (char letter in letters)
        {
            if ("юияэоаыеуё".Contains(char.ToLower(letter)))
            {
                vowelsCount++;
            }
            else if ("ъхзщшгнкцйфвпрлджбьтмсчqwertyuiopasdfghjklzxcvbnm".Contains(char.ToLower(letter)))
            {
                consonantsCount++;
            }
        }

        Console.WriteLine($"Количество гласных: {vowelsCount}, согласных: {consonantsCount}");
    }

    static string OpenAndReadF(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                throw new Exception("Файл не найден");
            }
        }
        catch (Exception ex) 
        { 
            Console.WriteLine(ex.Message);
        }
        return null;
    }
}