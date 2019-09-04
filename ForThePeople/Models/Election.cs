using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForThePeople.Models
{
    public class Election
    {
        public DateTime Date { get; set; }
        public string ElectionTitle { get; set; }
        public string City { get; set; }
        public string Office { get; set; }
    }
}
