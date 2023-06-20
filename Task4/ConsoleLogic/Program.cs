using Task4.Statistics;
using Task4.Statistics.Interfaces;
using Console = System.Console;

namespace Task4;

/// <summary>
/// класс косольного приложения системы статистики
/// </summary>
public class Program
{
    /// <summary>
    /// входная точка приложения
    /// запускает вечный цикл для работы консоли
    /// </summary>
    public static void Main()
    {
        IConsoleStatisticManager executor = new ConsoleStatisticManager();
        while (true) 
            executor.Execute(Console.ReadLine());
    }
}