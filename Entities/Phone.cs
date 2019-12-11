using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grphql_test.Entities
{
    public enum PhoneType
    {
        Unknown = 0,
        Home = 1,
        Work = 2,
        Mobile = 3,
        Other = 4
    }

    public class Phone
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public PhoneType Type { get; set; }
    }
}
