using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Async;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyRockCLI.DataModels
{
    public class PostgresDB : DataConnection
    {

        public PostgresDB() : base("MyTestDB") {
        }
        public ITable<Message> Message => GetTable<Message>();

    }
}
