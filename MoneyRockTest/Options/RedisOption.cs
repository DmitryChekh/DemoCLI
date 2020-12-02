using System;
using System.Collections.Generic;
using System.Text;
using CommandLine.Text;
using CommandLine;

namespace MoneyRockTest.Options
{
    [Verb("redis", HelpText = "Work with redis.")]
    public class RedisOption
    {


        [Option('r', "read", Required = false, HelpText = "Read string from Redis.")]
        public bool Read { get; set; }
        
        [Option('w', "write", Required = false, HelpText = "Write string to Redis")]
        public bool Write { get; set; }

        [Value(0, Required = true, HelpText = "Key")]
        public string Key { get; set; }

        [Value(0, Required = false, HelpText = "Value")]
        public string Value { get; set; }
    }
}
