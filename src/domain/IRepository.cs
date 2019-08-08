namespace Masha.Foundation.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IRepository<TEntity> : IDisposable where TEntity : DomainEntity
    {
        Task<Result<List<TEntity>>> FindAllAsync(Specification<TEntity> predicate);
        Task<Result<TEntity>> GetByIdAsync(string id);
        Task<Result<TEntity>> AddAsync(TEntity entity);
        Task<Result<TEntity>> UpdateAsync(TEntity entity);
        Task<Result<bool>> DeleteAsync(string id);
        Task<Result<long>> GetCountAsync(Specification<TEntity> predicate);
    }
}
