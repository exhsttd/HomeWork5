using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    static void CountLetters(char[] letters)
    {
        // Упражнение 6.1 Написать программу, которая вычисляет число гласных и согласных букв в
        // файле. Имя файла передавать как аргумент в функцию Main.
        
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

    static string OpenAndRead(string filePath)
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
    // Упражнение 6.2. Написать программу, реализующую умножению двух матриц, заданных в
    // виде двумерного массива.
    public static void PrintMatrix(int[,] matr) // визуал матриц
    {
        int str = matr.GetLength(0);
        int stl = matr.GetLength(1);

        for (int i = 0; i < str; i++)
        {
            for (int j = 0; j < stl; j++)
            {
                Console.Write(matr[i, j] + "\t");
            }
        }
    }
    public static int[,] MultiplyMatrices(int[,] matr1, int[,] matr2)
    {
        int str1 = matr1.GetLength(0);
        int stl1 = matr1.GetLength(1);
        int str2 = matr2.GetLength(0);
        int stl2 = matr2.GetLength(1);
        
        if (stl1 != str2)
        {
            throw new Exception("Количество столбцов первой матрицы должно быть равно количеству строк второй матрицы");
        }

        int[,] result = new int[str1, stl2];
        
        for (int i = 0; i < str1; i++)
        {
            for (int j = 0; j < stl2; j++)
            {
                result[i, j] = 0;
                for (int a = 0; a < stl1; a++)
                {
                    result[i, j] += matr1[i, a] * matr2[a, j];
                }
            }
        }
        return result;
    }
    
    // Упражнение 6.3 Написать программу, вычисляющую среднюю температуру за год. Создать
    // двумерный рандомный массив temperature[12,30], в котором будет храниться температура
    // для каждого дня месяца (предполагается, что в каждом месяце 30 дней). Сгенерировать
    // значения температур случайным образом.
    public static int[,] GenerateTemperatureData(int months, int days)
    {
        Random random = new Random();
        int[,] data = new int[months, days];

        for (int month = 0; month < months; month++)
        {
            for (int day = 0; day < days; day++)
            {
                data[month, day] = random.Next(-30, 51);
            }
        }
        return data;
    }

    public static double[] CalculateAverageTemperatures(int[,] temps) // средняя темпа
    {
        double[] averageTemps = new double[temps.GetLength(0)];

        for (int month = 0; month < temps.GetLength(0); month++)
        {
            double sum = 0;

            for (int day = 0; day < temps.GetLength(1); day++)
            {
                sum += temps[month, day];
            }

            averageTemps[month] = sum / temps.GetLength(1);
        }

        return averageTemps;
    }
    
    public static async Task Main(string[] args)
    {
        Task1(args);
        Task2();
        Task3();
    }

    public static void Task1(string[] args1)
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
            char[] text = OpenAndRead(filePath)?.ToCharArray()!;

            if (text != null)
            {
                Console.WriteLine($"Файл {arg}:");
                CountLetters(text);
            }
        }
    } // Упражнение 6.1
    public static void Task2()
    {
        // Упражнение 6.2 Написать программу, реализующую умножению двух матриц, заданных в
        // виде двумерного массива.
        Console.WriteLine("Упр. 6.2");

        Console.WriteLine("Введите количество строк для первой матрицы: ");
        int str1;
        if (!int.TryParse(Console.ReadLine(), out str1) || str1 <= 0)
        {
            Console.WriteLine("Введите количество строк (число положительное)");
            return;
        }

        Console.WriteLine("Введите количество столбцов для первой матрицы: ");
        int stl1;
        if (!int.TryParse(Console.ReadLine(), out stl1) || stl1 <= 0)
        {
            Console.WriteLine("Введите количество столбцов (число положительное)");
            return; 
        }

        int[,] matr1 = new int[str1, stl1];

        Console.WriteLine("Введите элементы первой матрицы по строкам: ");
        for (int i = 0; i < str1; i++)
        {
            for (int j = 0; j < stl1; j++)
            {
                int digit;
                if (!int.TryParse(Console.ReadLine(), out digit))
                {
                    Console.WriteLine("Введите число для элемента матрицы");
                    return; 
                }
                matr1[i, j] = digit;
            }
        }

        Console.WriteLine("Введите количество строк для второй матрицы: ");
        int str2;
        if (!int.TryParse(Console.ReadLine(), out str2) || str2 <= 0)
        {
            Console.WriteLine("Введите количество строк (число положительное)");
            return; 
        }

        Console.WriteLine("Введите количество столбцов для второй матрицы: ");
        int stl2;
        if (!int.TryParse(Console.ReadLine(), out stl2) || stl2 <= 0)
        {
            Console.WriteLine("Введите количество столбцов (число положительное)");
            return; 
        }

        int[,] matr2 = new int[str2, stl2];

        Console.WriteLine("Введите элементы второй матрицы по строкам: ");
        for (int i = 0; i < str2; i++)
        {
            for (int j = 0; j < stl2; j++)
            {
                int digit;
                if (!int.TryParse(Console.ReadLine(), out digit))
                {
                    Console.WriteLine("Введите число для элемента матрицы");
                    return; 
                }
                matr2[i, j] = digit;
            }
        }
        
        int[,] resultMatr = MultiplyMatrices(matr1, matr2);
        
        Console.WriteLine("Первая матрица:");
        PrintMatrix(matr1);
        Console.WriteLine("Вторая матрица:");
        PrintMatrix(matr2);
        Console.WriteLine("Результат умножения матриц:");
        PrintMatrix(resultMatr);
    } // Упражнение 6.2
    public static void Task3()
    {
        Console.WriteLine("Упр. 6.3");
        int[,] temp = GenerateTemperatureData(12, 30);
        double[] averageTemp = CalculateAverageTemperatures(temp);
        
        Console.WriteLine("Средняя температура за каждый месяц:");
        for (int month = 0; month < averageTemp.Length; month++)
        {
            Console.WriteLine($"Месяц {month + 1}: {averageTemp[month]:F2} °C");
        }
        
        Array.Sort(averageTemp); 
        
        Console.WriteLine("\n Отсортированные средние температуры:");
        foreach (double avgTemp in averageTemp)
        {
            Console.WriteLine($"{avgTemp:F2} °C");
        }
    } // Упражнение 6.3
}
