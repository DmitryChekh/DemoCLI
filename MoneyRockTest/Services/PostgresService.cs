using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using LinqToDB;
using LinqToDB.Configuration;
using Npgsql;
using LinqToDB.Data;
using MoneyRockTest.DataModels;
using LinqToDB.Common;
using System.Linq;

namespace MoneyRockTest.Services
{

    public class PostgresService
    {
        public async Task GetMessage(string messageString)
        {
            using (var db = new DbMyTest())
            {
                try
                {
                    var message = await db.Message.Where(m => m.MessageString == messageString).ToListAsync();
                    if (message.Any())
                        Console.WriteLine($"{message.First().Id} : {message.First().MessageString}");
                    else
                        Console.WriteLine("Not found");
                }
                catch
                {
                    Console.WriteLine("[ERROR]: Can't connect to Postgres");
                }


            }
        }

        public void GetMessageById(int id)
        {
            using (var db = new DbMyTest())
            {
                var message = from m in db.Message
                              where m.Id == id
                              select m;

                if(message.Any())
                    Console.WriteLine($"{message.First().Id} : {message.First().MessageString}");
                else
                    Console.WriteLine("Not found");

            }
        }

        public void AddMessage(string messageString)
        {
            using(var db = new DbMyTest())
            {
                try
                {
                    var result = db.Insert(new Message { MessageString = messageString });
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Can't add:", ex.Message); ;
                }
            }
        }

    }

}
