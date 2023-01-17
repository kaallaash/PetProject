namespace DG.Core.Interfaces.CrudOperations;

public interface IUpdateOperation<T>
{
    Task<T> Update(T entity, CancellationToken cancellationToken);
}