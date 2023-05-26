namespace Task1.Checking;

public static class Factory
{
    public static IReturnable<IEnumerable<int>> CreateInstance()
    {
        return new Generator();
    }
}