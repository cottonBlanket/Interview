namespace Task2;

public class Logger
{
    private readonly string _filename;

    public Logger()
    {
        _filename = "log.txt";
        using var f = File.Create(_filename);
    }
    
    public void Debug(string message)
    {
        Console.ForegroundColor = ConsoleColor.Gray;
        Console.WriteLine(message);
        Console.ResetColor();

        using (var f = File.AppendText(_filename))
        {
            f.WriteLine("[DBG] " + message);
        }
    }

    public void Information(string message)
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine(message);
        Console.ResetColor();
        
        using (var f = File.AppendText(_filename))
        {
            f.WriteLine("[INF] " + message);
        }
    }

    public void Warning(string message)
    {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine(message);
        Console.ResetColor();
        
        using (var f = File.AppendText(_filename))
        {
            f.WriteLine("[WRN] " + message);
        }
    }
}