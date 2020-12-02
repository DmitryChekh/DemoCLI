using System;
using System.Collections.Generic;
using System.Text;
using CommandLine.Text;
using CommandLine;


namespace MoneyRockCLI.Options
{
    [Verb("rabbit", HelpText = "Work with RabbitMQ.")]
    class RabbitOption 
    {

        [Option('s', "send", Required = false, HelpText = "Send message to RabbitMQ.")]
        public bool Send { get; set; }

        [Option('r', "receive", Required = false, HelpText = "Subscribe on Queue.")]
        public bool Receive { get; set; }


        [Value(0, Required = false, HelpText = "Value")]
        public string Value { get; set; }
    }
}
