using Task1.Checking;

namespace Task1;

public partial class Generator : IReturnable<IEnumerable<int>>
{
    public IEnumerable<int> Execute(params object[] args)
    {
        return MakeNumbers((int) args[0]);
    }
}