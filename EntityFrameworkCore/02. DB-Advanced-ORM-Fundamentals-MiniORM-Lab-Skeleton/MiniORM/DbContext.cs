using System;
using System.Collections.Generic;

namespace MiniORM
{
	// TODO: Create your DbContext class here.
    public abstract class DbContext
    {
        private readonly DatabaseConnection connection;

        private readonly Dictionary<Type, PropertyInfo> dbSetProperties;

        internal static readonly Type[] AllowedSqlTypes =
        {
            typeof(string),
            typeof(int),
            typeof(uint),
            typeof(long),
            typeof(ulong),
            typeof(decimal),
            typeof(bool),
            typeof(DateTime)
        };

        protected DbContext(string connectionString)
        {
            this.connection = new DatabaseConnection(connectionString);

            this.dbSetProperties = this.DiscoverDbSets();

            using (new ConnectionManager(connection)
            {
                this.InitializeDbSets();
            }
            
        this.MapAllRelations();
        }


    }
}