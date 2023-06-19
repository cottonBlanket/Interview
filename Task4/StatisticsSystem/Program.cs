using System.ComponentModel;
using Task4.Statistics;
using Task4.Statistics.Interfaces;
using Console = System.Console;

namespace Task4;

/// <summary>
/// главный класс этого приложения
/// </summary>
class Program
{
    /// <summary>
    /// входная точка приложения
    /// запускает вечный цикл для работы консоли
    /// </summary>
    public static void Main()
    {
        IStatisticManager executor = new StatisticManager();
        while (true) 
            executor.Execute(Console.ReadLine());
    }
}