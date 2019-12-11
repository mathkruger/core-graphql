﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grphql_test.Entities
{
    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Email> Emails { get; set; }
        public IList<Phone> Phones { get; set; }

        public Company()
        {
            Emails = new List<Email>();
            Phones = new List<Phone>();
        }
    }
}
