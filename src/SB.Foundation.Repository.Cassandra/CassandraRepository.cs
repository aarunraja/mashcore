namespace SB.Foundation.Repository.Cassandra
{
    using global::Cassandra;
    using global::Cassandra.Data.Linq;
    using global::Cassandra.Mapping;
    using Masha.Foundation;
    using Masha.Foundation.Domain;

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public abstract class CassandraRepository<TEntity> : IRepository<TEntity> where TEntity : DomainEntity
    {
        bool disposed = false;

        protected ISession session { get; set; }
        protected IMapper Mapper { get; set; }
        Table<TEntity> table;


        public CassandraRepository(string host, string databaseName)
        {

            var mapping = MappingConfiguration.Global.Define(
                          new Map<TEntity>()
                         .TableName(typeof(TEntity).Name)
                         .PartitionKey(u => u.Id));
            Dictionary<string, string> replication = new Dictionary<string, string>();

            replication.Add("class", "SimpleStrategy");
            replication.Add("replication_factor", "3");
            var cluster = Cluster.Builder()
                                           .AddContactPoints(host)
                                           .WithPort(9042)
                                           .WithLoadBalancingPolicy(new DCAwareRoundRobinPolicy("AWS_VPC_AP_SOUTHEAST_2"))
                                           .WithReconnectionPolicy(new FixedReconnectionPolicy(400, 5000, 2 * 60000, 60 * 60000))
                                           .Build();

            session = cluster.Connect(databaseName);
            session.CreateKeyspaceIfNotExists(databaseName, replication);

            table = new Table<TEntity>(session, mapping, typeof(TEntity).Name, databaseName);
            table.CreateIfNotExists();
        }

        private string _primaryKey;

        public virtual string PrimaryKey
        {
            get
            {
                if (_primaryKey != null)
                {
                    return _primaryKey;
                }

                var primaryKeyProperty = typeof(TEntity).GetProperties().FirstOrDefault(
                    p => p.GetCustomAttributes(typeof(PrimaryKeyAttribute), true).Any());
                if (primaryKeyProperty == null)
                {
                    throw new Exception($"Primary key not defined for type: {typeof(TEntity).Name}");
                }

                _primaryKey = primaryKeyProperty.Name;

                return _primaryKey;
            }
        }


        public virtual async Task<Result<List<TEntity>>> FindAllAsync(
                  Specification<TEntity> predicate)
        {
            try
            {
             return   await table.Select(a => a).ExecuteAsync();
                return new Result<<List<TEntity>>(result);
                //return await Mapper.FetchAsync<TEntity>();
                //return await Collection.Find(predicate.predicate).ToListAsync();
            }
            catch (Exception)
            {
                return Error.As<List<TEntity>>(ErrorCodes.InternalServerError);
            }
        }

       public async Task<Result<TEntity>> GetByIdAsync(string id)
        {
            try
            {
               return await table.Where(a => a.Id == id).FirstOrDefault().ExecuteAsync();
            }
            catch (Exception)
            {
                return Error.As<TEntity>(ErrorCodes.InternalServerError);
            }
        }


        public async Task<Result<TEntity>> AddAsync(TEntity entity)
        {
            try
            {
                table.Insert(entity).Execute();
                return await table.Where(a => a.Id == entity.Id).FirstOrDefault().ExecuteAsync();
            }
            catch (Exception)
            {
                return Error.As<TEntity>(ErrorCodes.InternalServerError);
            }
        }

        public async Task<Result<TEntity>> UpdateAsync(TEntity entity)
        {
            try
            {
                table.Where(u => u.Id == entity.Id)
                     .Select(u => entity)
                      .Update()
                      .Execute();
                return await table.Where(a => a.Id == entity.Id).FirstOrDefault().ExecuteAsync();
            }
            catch (Exception)
            {
                return Error.As<TEntity>(ErrorCodes.InternalServerError);
            }
        }

        Task<Result<long>> IRepository<TEntity>.GetCountAsync(Specification<TEntity> predicate)
        {
            try
            {
               return table.PageSize;
            }
            catch (Exception)
            {
                return Error.As<long>(ErrorCodes.InternalServerError);
            }
        }

       public async Task<Result<bool>> DeleteAsync(string id)
        {
            try
            {
                await table.Where(u => u.Id == id)
                     .Delete()
                     .ExecuteAsync();
                return new Result<bool>(true);
            }
            catch (Exception)
            {
                return Error.As<bool>(ErrorCodes.InternalServerError);
            }
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
