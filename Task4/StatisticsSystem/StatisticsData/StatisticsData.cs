using System.Configuration;
using System.Text;
using Task4.Statistics.Interfaces;

namespace Task4.Statistics;

/// <summary>
/// класс, реализующий метода для работы с хранилищем статистики
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
    /// возвращает сообщение о проделанной работе
    /// </summary>
    /// <param name="key">ключ</param>
    /// <param name="values">новые значения</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public async Task<string> Append(string key, string values)
    {
        if (!IsValidValues(values))
            return "Введены не корректные данные!";
        var response = new StringBuilder();
        var rows = await GetRowsAsync();
        var keys = rows[0].Split(' ').ToList();
        if (!keys.Contains(key))
        {
            rows[0] = $"{rows[0]} {key}";
            rows.Add(values);
            response.Append($"Ключ \"{key}\" успешно добавлен!");
        }
        else
        {
            var keyIndex = keys.IndexOf(key);
            rows[keyIndex] = $"{values} {rows[keyIndex]}";
        }
        
        await WriteRowsToFileAsync(rows);
        
        if (values != "")
            response.Append($"\nНовые значения по ключу \"{key}\" успешно добавлены!");
        return response.ToString();
    }

    /// <summary>
    /// считывает данные из файла, удаляет значения по входному ключу и возвращает сообщение об успещном удалении
    /// при отсутствии ключа возвращает сообщение об этом
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public async Task<string> Clear(string key)
    {
        var rows = await GetRowsAsync();
        var keys = rows[0].Split(' ').ToList();
        
        if (!keys.Contains(key))
            return $"Ключ \"{key}\" не найден!";
        var keyIndex = keys.IndexOf(key);
        rows[keyIndex] = "";
        await WriteRowsToFileAsync(rows);
        return $"Данные по ключу \"{key}\" успешно удалены!";
    }

    /// <summary>
    /// считывает файл с данными,
    /// возвращает сообщение с  подсчитанными статистическими данными
    /// при отсутствии данных или ключа возвращает соответствубщее собщение
    /// </summary>
    /// <param name="key">ключ</param>
    /// <returns>асинхронная задача, возвращающая сообщение, информирующее о проделанных действиях</returns>
    public async Task<string> Stat(string key)
    {
        var rows = await GetRowsAsync();
        var keys = rows[0].Split(' ').ToList();
        if (!keys.Contains(key))
            return $"Ключ \"{key}\" не найден!";
        
        var keyIndex = keys.IndexOf(key);
        var values = rows[keyIndex];
        if (values != "")
            return values.Trim().Split(' ').Select(int.Parse).Average().ToString();
        return "Данных по ключу не найдено!";
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
    
    /// <summary>
    /// проверяет корректность введенных значений статистики
    /// </summary>
    /// <param name="values">значения</param>
    /// <returns>истина - значения корректны, ложь - значения не корректны</returns>
    private bool IsValidValues(string values)
    {
        var valuesArray = values.Split(' ').Where(x => x != "");
        foreach (var value in valuesArray)
        {
            if (!int.TryParse(value, out var a))
                return false;
        }
        return true;
    }
}