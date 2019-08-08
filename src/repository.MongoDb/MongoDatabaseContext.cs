using Microsoft.Extensions.Options;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Masha.Foundation.Repository.MongoDb
{
    public class MongoDatabaseContext
    {
        public IMongoDatabase Database { get; }

        public MongoDatabaseContext(IOptions<MongoDBSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            if (client != null)
                Database = client.GetDatabase(settings.Value.Database);
        }
    }
}
