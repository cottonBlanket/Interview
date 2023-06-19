using Task4.Statistics.Interfaces;

namespace Task4.Statistics;

/// <summary>
/// сервис для обработки статистики полученной из консоли системы стиастики
/// </summary>
public class ConsoleStatisticManager: IConsoleStatisticManager
{
    /// <summary>
    /// поле для работы с хранилищем статистических данных
    /// </summary>
    private readonly IStatisticData _statisticData;
    
    /// <summary>
    /// конструктор без аргументов, инициализирующий поля
    /// </summary>
    public ConsoleStatisticManager()
    {
        _statisticData = new StatisticsData();
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
        var key = commandArray[1];
        string response;
        switch (commandArray[0])
        {
            case "append":
                response = await _statisticData.Append(key, string.Join(' ', commandArray[2..]));
                break;
            case "clear":
                response = await _statisticData.Clear(key);
                break;
            case "stat":
                response = await _statisticData.Stat(key);
                break;
            default:
                response = $"{commandArray[0]} - не является внутренней командой";
                break;
        }
        Console.WriteLine(response);
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
            Console.WriteLine("Введена не корректная строка!");
            return false;
        }
        return true;
    }
}