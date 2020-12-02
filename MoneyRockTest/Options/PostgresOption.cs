using System;
using System.Collections.Generic;
using System.Text;
using CommandLine.Text;
using CommandLine;

namespace MoneyRockCLI.Options
{
    [Verb("postgres", HelpText = "Work with postgres.")]
    class PostgresOption 
    {

        [Option('r', "read", Required = false, HelpText = "Read string from Postgres.")]
        public bool Read { get; set; }

        [Option('w', "write", Required = false, HelpText = "Write string to Postgres")]
        public bool Write { get; set; }

        [Option('i', "id", Required = false, HelpText = "Read string from Postgres by id")]
        public bool Id { get; set; }

        [Value(0, Required = true, HelpText = "Value")]
        public string Value { get; set; }

    }
}

