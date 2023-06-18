namespace Task2;

/// <summary>
/// класс, отвечающий за логирование - вывода и сохранения сообщений, информирующих о том, что происходит в приложении
/// </summary>
public class Logger
{
    /// <summary>
    /// путь до файла, в который сохраняются сообщения о работе приложения
    /// </summary>
    private readonly string _filename;
    /// <summary>
    /// путь до файла, в который сохраняются сообщения о работе приложения, не относящиеся к отладке
    /// </summary>
    private readonly string _fileInfoName;

    /// <summary>
    /// конструктор класса, не принимающий аргументов, инициализирующий поля со значениями по умолчанию,
    /// а также создающий соответсвующие файлы для сохраненния логов
    /// </summary>
    public Logger()
    {
        _filename = "log.txt";
        _fileInfoName = "log_info.txt";
        using var f = File.Create(_filename);
        using var i = File.Create(_fileInfoName);
    }
    
    /// <summary>
    /// логирует входяшее сообщение при отладке приложения
    /// </summary>
    /// <param name="message">сообщение о работе приложения при отладке</param>
    public void Debug(string message)
    {
        message = $"{DateTime.Now}: {message}";
        WriteLogToConsole(ConsoleColor.Gray, message);
        WriteLogToFile("DBG", message, _filename);
    }

    /// <summary>
    /// логирует входящее сообщение о ходе выполения процесса компоновки
    /// </summary>
    /// <param name="message">сообщение о ходе выполнения</param>
    public void Verbose(string message)
    {
        message = $"{DateTime.Now}: {message}";
        WriteLogToConsole(ConsoleColor.DarkGray, message);
        WriteLogToFile("VRB", message, _filename);
    }

    /// <summary>
    /// логирует входящее информационное сообщение о работе приложения
    /// </summary>
    /// <param name="message">информационное сообщение о работе программы</param>
    public void Information(string message)
    {
        message = $"{DateTime.Now}: {message}";
        WriteLogToConsole(ConsoleColor.White, message);
        WriteLogToFile("INF", message, _filename);
        WriteLogToFile("INF", message, _fileInfoName);
    }

    /// <summary>
    /// логирует сообщение о внештатной ситуации, на которое стоит обратить внимание
    /// </summary>
    /// <param name="message">сообщение - предупреждение о внештатной ситуации</param>
    public void Warning(string message)
    {
        message = $"{DateTime.Now}: {message}";
        WriteLogToConsole(ConsoleColor.Yellow, message);
        WriteLogToFile("WRN", message, _filename);
        WriteLogToFile("WRN", message, _fileInfoName);
    }

    /// <summary>
    /// логирует сообщение об тшибке в ходн работы приложения
    /// </summary>
    /// <param name="message">сообщение об ошибке</param>
    public void Error(string message)
    {
        message = $"{DateTime.Now}: {message}";
        WriteLogToConsole(ConsoleColor.Red, message);
        WriteLogToFile("ERR", message, _filename);
        WriteLogToFile("ERR", message, _fileInfoName);
    }

    /// <summary>
    /// выводит в консоль лог с заданным цветом
    /// </summary>
    /// <param name="color">цвет логируемого сообщения</param>
    /// <param name="message">сообщение</param>
    private void WriteLogToConsole(ConsoleColor color, string message)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }

    /// <summary>
    /// записывает лог в файл
    /// </summary>
    /// <param name="reduction">сокращенное название уровня входящего лога</param>
    /// <param name="message">входящее сообщение</param>
    /// <param name="file">путь до файла, в который нужно залогировать входящее сообщение</param>
    private void WriteLogToFile(string reduction, string message, string file)
    {
        using (var f = File.AppendText(file))
        {
            f.WriteLine($"[{reduction}] {message}");
        }
    }
}