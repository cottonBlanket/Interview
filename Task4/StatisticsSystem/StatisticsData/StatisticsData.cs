using System.Threading.Channels;
using Task4.Statistics.Interfaces;

namespace Task4.Statistics;

/// <summary>
/// класс, отвечающий за хранение статистических данных
/// </summary>
public class StatisticsData: IStatisticData
{
    /// <summary>
    /// статистические данные
    /// </summary>
    private readonly Dictionary<string, List<int>> Data = new ();
    /*public static readonly Dictionary<string, Action<string>> Actions = new ()
    {
        {"append", Append},
        {"clear", Clear},
        {"stat", Stat}
    };*/

    /// <summary>
    /// добавляет в словарь данных новые значения по ключу
    /// если данного ключа нет в словаре, то добавляет его
    /// печатает консоль о выполненных действиях
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="values">новые значения</param>
    public void Append(string key, IEnumerable<int> values)
    {
        if (Data.ContainsKey(key))
            Data[key].AddRange(values);
        else
        {
            Data[key] = values.ToList();
            Console.WriteLine("Ключ \"{0}\" успешно добавлен!", key);
        }
        Console.WriteLine("Новые значения по ключу \"{0}\" успешно добавлены!", key);
    }

    /// <summary>
    /// очищает список значение в словаре по ключу и печатает в консоль об успешной операции
    /// при отсутствии ключу печает в консоль соответствующее сообщение
    /// </summary>
    /// <param name="key">ключ</param>
    public void Clear(string key)
    {
        if (Data.ContainsKey(key))
        {
            Data[key].Clear();
            Console.WriteLine("Данные по ключу \"{0}\" успешно удалены!", key);
        }
        else
            Console.WriteLine("Ключ \"{0}\" не найден!", key);
    }

    /// <summary>
    /// печатает в консоль среднее по всем значениям по ключу
    /// при отсутствии данных или ключа печатает соответствующее сообщение
    /// </summary>
    /// <param name="key">ключ</param>
    public void Stat(string key)
    {
        if (Data.ContainsKey(key))
        {
            var values = Data[key];
            if (values.Count == 0)
            {
                Console.WriteLine("Данных по ключу не найдено!");
                return;
            }
            Console.WriteLine(Data[key].Average());
        }
        else
            Console.WriteLine("Ключ \"{0}\" не найден!", key);
    }
}