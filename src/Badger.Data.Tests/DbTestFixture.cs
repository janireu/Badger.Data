using System;
using System.Data.Common;

namespace Badger.Data.Tests
{
    public abstract class DbTestFixture : IDisposable
    {
        public DbConnection Connection { get; private set;}
        public DbProviderFactory ProviderFactory { get; }
        public abstract string ConnectionString { get; }
        public string TestDatabase { get; }
        public readonly Person TestPerson1 = new Person { Name = "Bill", Dob = new DateTime(2000, 1, 1)};
        public readonly Person TestPerson2 = new Person { Name = "Ben", Dob = new DateTime(2001, 1, 1)};

        protected DbTestFixture(DbProviderFactory providerFactory)
        {
            this.TestDatabase = "badgerdata" + Guid.NewGuid().ToString().Replace("-", "");

            this.ProviderFactory = providerFactory;
        }

        protected void OpenTestConnection()
        {
            this.Connection = this.ProviderFactory.CreateConnection();
            this.Connection.ConnectionString = this.ConnectionString;
            this.Connection.Open();
        }

        public virtual void Dispose()
        {
            this.Connection.Dispose();
        }
    }
}
