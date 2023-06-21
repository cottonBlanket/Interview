using System.Net.NetworkInformation;
using Dal.Statistic.Repositories.Interfaces;
using Task4.Entities;
using Task4.Statistics.Api;
using Task4.Statistics.Interfaces;

namespace Task4.Statistics;

/// <summary>
/// сервис для обработки статистики полученной из консоли системы стиастики
/// </summary>
public class ConsoleStatisticManager: IConsoleStatisticManager
{
    /// <summary>
    /// обработчик статистики
    /// </summary>
    private readonly IStatisticManager _statisticManager;

    /// <summary>
    /// словарь консольных названий методов подсчета к значениям енамо
    /// </summary>
    private static readonly Dictionary<string, string> MethodNames = new()
    {
        { "avg", "0" },
        { "sum", "1" }
    };

    /// <summary>
    /// конструктор без аргументов, инициализирующий поля
    /// </summary>
    public ConsoleStatisticManager(IStatisticManager statisticManager)
    {
        _statisticManager = statisticManager;
    }
    
    /// <summary>
    /// обрабатывает входную команду, получает необходимое действие и ключ,
    /// выполняет соответсвующую команду для статистики по полученному ключу
    /// печатает в консоль о проделанной работе
    /// </summary>
    /// <param name="command">входная команда</param>
    public async Task Execute(string? command)
    {
        if(command == "")
            return;
        var commandArray = command.Split(' ').Where(x => x != "").ToArray();
        if(!IsCommandValid(commandArray))
            return;
        var response = await GetResponseFromCommand(commandArray);
        
        Console.WriteLine(response);
        Console.ResetColor();
    }

    /// <summary>
    /// определяет введенную команду, обрабатывает её, возвращает сообщение от обработчика
    /// </summary>
    /// <param name="commandArray">команда в виде массива</param>
    /// <returns>сообщение</returns>
    private async Task<string> GetResponseFromCommand(string[] commandArray)
    {
        var key = commandArray[1];
        switch (commandArray[0])
        {
            case "append":
                var statistic = new Statistic(key, string.Join(',', commandArray[2..]));
                return await _statisticManager.Append(statistic);
            case "clear":
                return await _statisticManager.Clear(key);
            case "stat":
                return await _statisticManager.Calculate(key);
            case "change-method":
                if (commandArray.Length != 3 || !MethodNames.ContainsKey(commandArray[2]))
                    return "Введена не корректная строка!";
                var statForChanger = new Statistic(key, MethodNames[commandArray[2]]);
                return await _statisticManager.ChangeMethod(statForChanger);
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                return $"{commandArray[0]} - не является внутренней командой";
        }
    }

    /// <summary>
    /// проверяет входную команду на валидность
    /// </summary>
    /// <param name="command">входная команда</param>
    /// <returns>истинна - если валидна, ложь - если не валидна</returns>
    private bool IsCommandValid(string[] command)
    {
        if (command.Length < 2)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Введена не корректная строка!");
            Console.ResetColor();
            return false;
        }
        return true;
    }
}