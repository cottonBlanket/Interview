namespace Task1.Checking;

/// <summary>
/// интерфейс, утверждающий что реализуемый класс будет содержать метод, возвращающий объект типа дженерик параметра
/// </summary>
/// <typeparam name="T">тип объекта, который возвращает реализуемый метод</typeparam>
public interface IReturnable<T>
{
    /// <summary>
    /// выполняет действия со входящим набором данных и возвращает объект заданного типа T
    /// </summary>
    /// <param name="args">входной массив данных</param>
    /// <returns>тип объекта, переданного дженерик параметром в класс</returns>
    T Execute(params object[] args);
}