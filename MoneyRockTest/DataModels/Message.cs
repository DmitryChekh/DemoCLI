using System;
using System.Collections.Generic;
using System.Text;
using LinqToDB.Mapping;

namespace MoneyRockTest.DataModels
{

    [Table(Name ="messagestrings")]
    public class Message
    {
        [Column(Name="id")]
        [PrimaryKey, Identity]
        public int Id { get; set; }
        
        [Column(Name = "name"), NotNull]
        public string MessageString { get; set; }
    }
}
