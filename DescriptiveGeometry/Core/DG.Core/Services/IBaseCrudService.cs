using DG.Core.Interfaces.CrudOperations;

namespace DG.Core.Services;

public interface IBaseCrudService<T1, in T2> :
    IGetByIdOperation<T1>,
    IGetAllOperation<T1>,
    ICreateOperation<T1>,
    IUpdateOperation<T1>,
    IDeleteOperation<T2>
{ }