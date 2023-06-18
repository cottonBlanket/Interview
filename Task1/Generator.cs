namespace Task1;

/// <summary>
/// часть класса, отвечающего за генерацию, содержащего реализацию метода генерации
/// </summary>
public partial class Generator
{
    /// <summary>
    /// генерирует заданное количество простых чисел
    /// </summary>
    /// <param name="i">количество простых чисел, которое необходимо сгенерировать</param>
    /// <returns>коллекция простых чисел</returns>
    public IEnumerable<int> MakeNumbers(int i)
    {
        List<int> list = new();
        list.Add(2);

        int c = 3;
        while (list.Count < i)
        {
            bool pr = true;
            foreach (var opr in list)
            {
                if (c % opr == 0)
                {
                    pr = false;
                    break;
                }
            }
            if (pr)
                list.Add(c);
            c += 2;
        }
        return list;
    }
}