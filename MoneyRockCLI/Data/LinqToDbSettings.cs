using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LinqToDB.Configuration;

namespace MoneyRockCLI.Data
{
    public class ConnectionStringSettings : IConnectionStringSettings
    {
        public string ConnectionString { get; set; }
        public string Name { get; set; }
        public string ProviderName { get; set; }
        public bool IsGlobal => false;
    }

    public class MySettings : ILinqToDBSettings
    {
        public IEnumerable<IDataProviderSettings> DataProviders => Enumerable.Empty<IDataProviderSettings>();

        public string DefaultConfiguration => "PostgreSQL";
        public string DefaultDataProvider => "PostgreSQL";

        public IEnumerable<IConnectionStringSettings> ConnectionStrings
        {
            get
            {
                yield return
                    new ConnectionStringSettings
                    {
                        Name = "MyTestDB",
                        ProviderName = "PostgreSQL",
                        ConnectionString = @"Host = localhost; Port = 5432; Database = mytest; Username = postgres; Password = mysecretpassword;"
                    };
            }
        }
    }
}
