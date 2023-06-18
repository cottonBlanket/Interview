namespace Task1.Checking;

/// <summary>
/// Класс, отвечающий за создание объектов классов
/// </summary>
public static class Factory
{
    /// <summary>
    /// создает объект класса Genearator, возвращающий коллекции целых чисел
    /// </summary>
    /// <returns>Объект класса, реализующий интерфейс IReturnable&lt;IEnumerable&lt;int&gt;&gt;</returns>
    public static IReturnable<IEnumerable<int>> CreateInstance()
    {
        return new Generator();
    }
}