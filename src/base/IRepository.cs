namespace Masha.Foundation
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<T, IT>
    {
        Task<Result<T>> Get(IT aggregateId);
        Task<Result<List<T>>> Get(Specification<T> specification);
        Task<Result<T>> Save(T entity);
        Task<Result<T>> Update(T entity);
    }
}
