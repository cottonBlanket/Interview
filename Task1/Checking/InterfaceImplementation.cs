using Task1.Checking;

namespace Task1;

/// <summary>
/// часть класса, отвечающего за генерацию, реализующий наследуемый интерфейс
/// </summary>
public partial class Generator : IReturnable<IEnumerable<int>>
{
    /// <summary>
    /// выполняет действия со входными данными и возвращает коллекцию целых чисел
    /// </summary>
    /// <param name="args">массив входных данных</param>
    /// <returns>коллекция целых чисел</returns>
    public IEnumerable<int> Execute(params object[] args)
    {
        return MakeNumbers((int) args[0]);
    }
}