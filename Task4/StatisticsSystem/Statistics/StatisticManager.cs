using Task4.Statistics.Interfaces;

namespace Task4.Statistics;

/// <summary>
/// сервис для обработки получаемой статистики
/// </summary>
public class StatisticManager: IStatisticManager
{
    /// <summary>
    /// поле для работы с хранилищем статистических данных
    /// </summary>
    private readonly IStatisticData _statisticData;
    
    /// <summary>
    /// конструктор без аргументов, инициализирующий поля
    /// </summary>
    public StatisticManager()
    {
        _statisticData = new StatisticsData();
    }
    
    /// <summary>
    /// обрабатывает входную команду, получает необходимое действие и ключ,
    /// выполняет соответсвующую команду для статистики по полученному ключу
    /// </summary>
    /// <param name="command">входная команда</param>
    public void Execute(string? command)
    {
        if(command == "")
            return;
        var commandArray = command.Split(' ').Where(x => x != "").ToArray();
        if(!IsCommandValid(commandArray))
            return;
        var key = commandArray[1];
        switch (commandArray[0])
        {
            case "append":
                _statisticData.Append(key, string.Join(' ', commandArray[2..]));
                break;
            case "clear":
                _statisticData.Clear(key);
                break;
            case "stat":
                _statisticData.Stat(key);
                break;
            default:
                Console.WriteLine("{0} - не является внутренней командой", commandArray[0]);
                break;
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
            Console.WriteLine("Введена не корректная строка!");
            return false;
        }

        return true;
    }
}