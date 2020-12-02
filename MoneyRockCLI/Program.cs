using System;
using System.Threading;
using System.Threading.Tasks;
using StackExchange.Redis;
using CommandLine.Text;
using CommandLine;
using MoneyRockCLI.Options;
using MoneyRockCLI.Services;
using LinqToDB.Data;
using MoneyRockCLI.DataModels;

namespace MoneyRockCLI
{
    class Program
    {
        public static bool keepRunnig = true;
        static void Main()
        {

            DataConnection.DefaultSettings = new MySettings();

            CommandExecuter commandExecuter = new CommandExecuter();

            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
                Program.keepRunnig = false;
            };

            while (Program.keepRunnig)
            {
                var command = Console.ReadLine();

                if(string.IsNullOrWhiteSpace(command))
                {
                    continue;
                }

                var args = command.Split(' ');

                commandExecuter.Execute(args);

            }
            
        }

    }
}
