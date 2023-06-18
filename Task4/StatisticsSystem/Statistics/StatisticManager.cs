using Task4.Statistics.Interfaces;

namespace Task4.Statistics;

/// <summary>
/// сервис для обработки получаемой статистики
/// </summary>
public class StatisticManager: IStatisticManager
{
    private readonly IStatisticData _statisticData;
    public StatisticManager()
    {
        _statisticData = new StatisticsData();
    }
    public void Execute(string? command)
    {
        if(command == null)
            return;
        var commandArray = command.Split(' ').Where(x => x != "").ToArray();
        if(!IsCommandValid(commandArray))
            return;
        var key = commandArray[1];
        switch (commandArray[0])
        {
            case "append":
                _statisticData.Append(key, commandArray[2..].Select(int.Parse));
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