using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace grphql_test.Entities
{
    public class Person
    {
        public Person CareerCounselor { get; set; }
        public IList<Company> Companies { get; set; }
        public IList<Email> Emails { get; set; }
        public string FirstName { get; set; }
        public int Id { get; set; }
        public string LastName { get; set; }
        public int MergedToPersonId { get; set; }
        public IList<Phone> Phones { get; set; }
        public Person Supervisor { get; set; }

        public Person()
        {
            Companies = new List<Company>();
            Emails = new List<Email>();
            Phones = new List<Phone>();
        }
    }
}
