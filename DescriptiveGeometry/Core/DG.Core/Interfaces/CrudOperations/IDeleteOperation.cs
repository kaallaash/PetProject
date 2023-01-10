namespace DG.Core.Interfaces.CrudOperations;

public interface IDeleteOperation<T>
{
    Task Delete(T value, CancellationToken cancellationToken);
}