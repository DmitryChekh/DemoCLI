﻿using System;
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
        public async Task GetMessage(string messageString)
        {
            using (var db = new PostgresDB())
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

        public async Task GetMessageById(int id)
        {
            using (var db = new PostgresDB())
            {
                try
                {
                    var message = await db.Message.FirstOrDefaultAsync(m => m.Id == id);

                    if (message != null)
                        Console.WriteLine($"{message.Id} : {message.MessageString}");
                    else
                        Console.WriteLine("Not found");
                }
                catch
                {
                    Console.WriteLine("[ERROR]: Can't connect to Postgres");
                }


            }
        }

        public async Task AddMessage(string messageString)
        {
            using (var db = new PostgresDB())
            {
                try
                {
                    var result = await db.InsertAsync(new Message { MessageString = messageString });
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Can't add:", ex.Message); ;
                }
            }
        }

    }

}