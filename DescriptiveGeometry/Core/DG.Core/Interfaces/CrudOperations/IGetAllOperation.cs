namespace DG.Core.Interfaces.CrudOperations;

public interface IGetAllOperation<T>
{
    Task<IEnumerable<T>> GetAll(
        CancellationToken cancellationToken);
}