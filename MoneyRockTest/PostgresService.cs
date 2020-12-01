using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Npgsql.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MoneyRockTest
{
    public class PostgresService
    {
        private readonly ApplicationContext _dbContext;

        public PostgresService()
        {
            _dbContext = new ApplicationContext();
        }

        public async Task<bool> WriteAsync(string strValue)
        {
            var result = await _dbContext.TestStrings.AddAsync(new TestString { StringValue = strValue });

            if(result.State == EntityState.Added)
            {
                return true;
            }
            return false;
        }

        public async Task<string> ReadAsync(string strValue)
        {
            var result = await _dbContext.TestStrings.FirstOrDefaultAsync(x => x.StringValue == strValue);

            if (result == null)
                return "Not found";
            return result.StringValue;
        }

    }


    public class ApplicationContext: DbContext
    {
        public DbSet<TestString> TestStrings { get; set; }
        

        public ApplicationContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=mytest;Username=postgres;Password=mysecretpassword");
        }
    }

    public class TestString
    {
        public int Id { get; set; }
        public string StringValue { get; set; }
    }

}
