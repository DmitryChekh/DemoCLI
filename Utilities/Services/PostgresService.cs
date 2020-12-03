using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using LinqToDB;
using LinqToDB.Configuration;
using LinqToDB.Data;
using Utilities.DataModels;
using LinqToDB.Common;
using System.Linq;

namespace Utilities.Services
{

    public class PostgresService : IPostgresService
    {
        public async Task<string> Get(string messageString)
        {
            using (var db = new PostgresDB())
            {
                try
                {
                    var message = await db.Message.Where(m => m.MessageString == messageString).ToListAsync();
                    if (message.Any())
                        return $"{message.First().Id} : {message.First().MessageString}";
                    else
                        return "Not found";
                }
                catch(Exception ex)
                {
                    return $"[ERROR]: Can't connect to Postgres [{ex.Message}]";
                }


            }
        }

        public async Task<string> GetById(int id)
        {
            using (var db = new PostgresDB())
            {
                try
                {
                    var message = await db.Message.FirstOrDefaultAsync(m => m.Id == id);

                    if (message != null)
                        return $"{message.Id} : {message.MessageString}";
                    else
                        return "Not found";
                }
                catch (Exception ex)
                {
                    return $"[ERROR]: Can't connect to Postgres [{ex.Message}]";
                }


            }
        }

        public async Task<string> Create(string messageString)
        {
            using (var db = new PostgresDB())
            {
                try
                {
                    var result = await db.InsertAsync(new Message { MessageString = messageString });
                    return "Added: " + messageString;
                }
                catch (Exception ex)
                {
                    return $"[ERROR]: Can't connect to Postgres [{ex.Message}]";
                }
            }
        }

    }

}
