namespace Task1;

public partial class Generator
{
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