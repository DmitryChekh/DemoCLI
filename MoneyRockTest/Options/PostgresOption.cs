using System;
using System.Collections.Generic;
using System.Text;
using CommandLine.Text;
using CommandLine;

namespace MoneyRockTest.Options
{
    [Verb("postgres", HelpText = "Work with postgres.")]
    class PostgresOption
    {

        [Option('r', "read", Required = false, HelpText = "Read string from Redis.")]
        public bool Read { get; set; }

        [Option('w', "write", Required = false, HelpText = "Write string to Redis")]
        public bool Write { get; set; }

        [Option('i', "id", Required = false, HelpText = "Write string to Redis")]
        public bool Id { get; set; }

        [Value(0, Required = true, HelpText = "Value")]
        public string Value { get; set; }

    }
}

