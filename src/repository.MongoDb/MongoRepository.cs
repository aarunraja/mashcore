namespace Masha.Foundation.Repository.MongoDb
{
    using MongoDB.Driver;
    using Masha.Foundation;
    using Masha.Foundation.Domain;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public abstract class MongoRepository<TEntity> : IRepository<TEntity> where TEntity : DomainEntity
    {
        bool disposed = false;

        protected IMongoCollection<TEntity> Collection { get; }
        protected readonly MongoDatabaseContext _dataContext;


        public MongoRepository(MongoDatabaseContext dataContext, string collection = "")
        {
            _dataContext = dataContext;

            if (string.IsNullOrWhiteSpace(collection))
            {
                collection = typeof(TEntity).Name;
            }
            Collection = _dataContext.Database.GetCollection<TEntity>(collection);
        }




        public virtual async Task<Result<List<TEntity>>> FindAllAsync(
                  Specification<TEntity> predicate)
        {
            return await Collection.Find(predicate.predicate).ToListAsync();
        }

        public virtual async Task<Result<TEntity>> GetByIdAsync(string id)
        {
            var result = await Collection.Find(x => x.Id.Equals(id)).FirstOrDefaultAsync();
            return result ?? Error.As<TEntity>(ErrorCodes.ResourceNotFound);
        }

        public virtual async Task<Result<TEntity>> AddAsync(TEntity entity)
        {
            entity.GenerateNewIdentity(entity.Id);
            await Collection.InsertOneAsync(entity);
            return entity;
        }

        public virtual async Task<Result<TEntity>> UpdateAsync(TEntity entity)
        {
            await Collection.ReplaceOneAsync(
                x => x.Id.Equals(entity.Id),
                entity,
                new UpdateOptions
                {
                    IsUpsert = false
                });
            return entity;
        }

        public virtual async Task<Result<long>> GetCountAsync(Specification<TEntity> predicate)
        {
            return await Collection.CountDocumentsAsync(predicate.predicate);
        }

        public virtual async Task<Result<bool>> DeleteAsync(string id)
        {
            var deleteResult = await Collection.DeleteOneAsync(x => x.Id.Equals(id));
            return deleteResult.IsAcknowledged;
        }

        public void Dispose()
        {
            Dispose(true);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                disposed = true;

            }
        }
    }
}
