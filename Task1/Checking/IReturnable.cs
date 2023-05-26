namespace Task1.Checking;

public interface IReturnable<T>
{
    T Execute(params object[] args);
}