namespace SB.Foundation.Repository.Cassandra
{
    using global::Cassandra.Mapping;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class CassandraMappings : Mappings
    {
        public CassandraMappings Add<T>(string tableName)
        {
            For<T>().TableName(tableName);

            return this;
        }
    }
}
