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
    /// обработчик статистики, полученной с консоли
    /// </summary>
    private readonly IConsoleStatisticManager _consoleStatisticManager;

    /// <summary>
    /// конструктор, иницализирующий поля класса
    /// </summary>
    /// <param name="consoleStatisticManager">обработчик статистики, полученной с консоли</param>
    public Program(IConsoleStatisticManager consoleStatisticManager)
    {
        _consoleStatisticManager = consoleStatisticManager;
    }
    
    public static void Main()
    {
    }

    /// <summary>
    /// считывает команды с консоли и обрабатывает их
    /// </summary>
    public void ReadConsoleCommand()
    {
        while (true) 
            _consoleStatisticManager.Execute(Console.ReadLine());
    }
}