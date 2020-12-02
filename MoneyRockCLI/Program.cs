using System;
using Autofac;
using LinqToDB.Data;
using MoneyRockCLI.Data;

namespace MoneyRockCLI
{
    class Program
    {
        public static bool keepRunnig = true;
        static void Main()
        {
            var container = ContainerConfigure.Configure();
            DataConnection.DefaultSettings = new MySettings();

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

                using (var scope = container.BeginLifetimeScope())
                {
                    var commandExecuter = scope.Resolve<ICommandExecuter>();
                    commandExecuter.Execute(args);
                }

            }
            
        }

    }
}
