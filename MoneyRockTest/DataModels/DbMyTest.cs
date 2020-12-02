using LinqToDB;
using LinqToDB.Data;
using LinqToDB.Async;
using System;
using System.Collections.Generic;
using System.Text;

namespace MoneyRockTest.DataModels
{
    public class DbMyTest : DataConnection
    {

        public DbMyTest() : base("MyTestDB") {
        }


        public ITable<Message> Message => GetTable<Message>();
    }
}
