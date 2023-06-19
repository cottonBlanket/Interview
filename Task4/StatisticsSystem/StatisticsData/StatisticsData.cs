using System.Threading.Channels;
using System.Collections.Specialized;
using System.Configuration;
using System.Net.Mime;
using Task4.Statistics.Interfaces;

namespace Task4.Statistics;

/// <summary>
/// класс, отвечающий за хранение статистических данных
/// </summary>
public class StatisticsData: IStatisticData
{
    /// <summary>
    /// путь до файла с данными
    /// </summary>
    private readonly string _data;
    
    /// <summary>
    /// конструктор класса
    /// полю _data присваивает значение из конфигурации приложения по ключу pathToData
    /// при отсутствии файла по полученному пути останавливает работу приложения
    /// </summary>
    public StatisticsData()
    {
        var path = ConfigurationManager.AppSettings.Get("pathToData");
        _data = path;
        if (!IsValidFile().Result)
        {
            Console.WriteLine("Файл в папке {0} не существует!", path);
            Environment.Exit(1);
        }
    }
    /// <summary>
    /// считывает файл с данными, добавляет новые значения по входному ключу
    /// при отсутствии ключа добавляет его
    /// печатает в консоль соотвествующие сообщения
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="values">новые значения</param>
    /// <returns>асинхронное действие</returns>
    public async Task Append(string key, string values)
    {
        var rows = await GetRowsAsync();
        var keys = rows[0].Split(' ').ToList();
        if (!keys.Contains(key))
        {
            rows[0] = $"{rows[0]} {key}";
            rows.Add(values);
            Console.WriteLine("Ключ \"{0}\" успешно добавлен!", key);
        }
        else
        {
            var keyIndex = keys.IndexOf(key);
            rows[keyIndex] = $"{values} {rows[keyIndex]}";
        }
        
        await WriteRowsToFileAsync(rows);
        
        if (values != "")
            Console.WriteLine("Новые значения по ключу \"{0}\" успешно добавлены!", key);
    }

    /// <summary>
    /// считывает данные из файла,
    /// удаляет значения по входному ключу и сообщает об успешном удалении в консоли
    /// при отсутствии ключа сообщает об этом в консоли
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронное действие</returns>
    public async Task Clear(string key)
    {
        var rows = await GetRowsAsync();
        var keys = rows[0].Split(' ').ToList();
        
        if (!keys.Contains(key))
            Console.WriteLine("Ключ \"{0}\" не найден!", key);
        var keyIndex = keys.IndexOf(key);
        rows[keyIndex] = "";
        await WriteRowsToFileAsync(rows);
        Console.WriteLine("Данные по ключу \"{0}\" успешно удалены!", key);

        }

    /// <summary>
    /// считывает файл с данными,
    /// печатает в консоль среднее всех значений по входному ключу 
    /// при отсутствии данных или ключа печатает соответствующее сообщение
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронное действие</returns>
    public async Task Stat(string key)
    {
        var rows = await GetRowsAsync();
        var keys = rows[0].Split(' ').ToList();
        if (!keys.Contains(key))
            Console.WriteLine("Ключ \"{0}\" не найден!", key);
        
        var keyIndex = keys.IndexOf(key);
        var values = rows[keyIndex];
        if (values != "")
        {
            Console.WriteLine(values.TrimEnd().Split(' ').Select(int.Parse).Average());
            return;
        }
        Console.WriteLine("Данных по ключу не найдено!");
            
    }

    /// <summary>
    /// асинхронно считывает данные из файла в список строк
    /// </summary>
    /// <returns>асинхронная задача, возвращающая список строк</returns>
    private async Task<List<string>> GetRowsAsync()
    {
        var lines = await File.ReadAllLinesAsync(_data);
        return lines.ToList();
    }

    /// <summary>
    /// асинхронно записывает построчно в файл коллекцию строк
    /// </summary>
    /// <param name="rows">входная коллекция строк</param>
    private async Task WriteRowsToFileAsync(IEnumerable<string> rows)
    {
        using (var writer = new StreamWriter(_data, false))
        {
            foreach (var line in rows)
                await writer.WriteLineAsync(line);
        }
    }

    /// <summary>
    /// проверяет существует ли файл по пути из конфигурации приложения
    /// если файл существует и он пустой, то добавляет в него новую пустую строку
    /// </summary>
    /// <returns>асинхронная задача, возвращающая истинну если файл существует, ложь если файл не существует</returns>
    private async Task<bool> IsValidFile()
    {
        if (!File.Exists(_data)) 
            return false;
        var rows = await GetRowsAsync();
        if (rows.Count == 0)
            await WriteRowsToFileAsync(new[] { "" });
        return true;

    }
}