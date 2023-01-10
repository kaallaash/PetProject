using DG.Core.Interfaces.CrudOperations;

namespace DG.Core.Repositories;

public interface IBaseCrudRepository<T> :
    IGetByIdOperation<T>,
    IGetAllOperation<T>,
    ICreateOperation<T>,
    IUpdateOperation<T>,
    IDeleteOperation<T>
{}