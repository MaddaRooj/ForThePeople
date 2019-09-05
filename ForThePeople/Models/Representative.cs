using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForThePeople.Models
{
    public class Representative
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string LastName { get; set; }
        public string Role { get; set; }
        public string PoliticalPartyId { get; set; }
        public string Bio { get; set; }
        public string Contact { get; set; }
        public int NextElectionYear { get; set; }
    }
}
