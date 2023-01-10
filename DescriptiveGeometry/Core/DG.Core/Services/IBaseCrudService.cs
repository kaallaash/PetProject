using DG.Core.Interfaces.CrudOperations;

namespace DG.Core.Services;

public interface IBaseCrudService<T1, in T2> :
    IGetByIdOperation<T1>,
    IGetAllOperation<T1>,
    ICreateOperation<T1>,
    IUpdateOperation<T1>,
    IDeleteOperation<T2>
{ }

//namespace DG.Core.Services;

//public interface IBaseCrudService<T>
//{
//    Task<T?> GetById(int id,
//        CancellationToken cancellationToken);

//    Task<IEnumerable<T>> GetAll(
//        CancellationToken cancellationToken);

//    Task<T> Create(T entity,
//        CancellationToken cancellationToken);

//    Task<T> Update(T entity,
//        CancellationToken cancellationToken);

//    Task Delete(int id,
//        CancellationToken cancellationToken);

//}


