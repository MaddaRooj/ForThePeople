using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForThePeople.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }
        public string Chamber { get; set; }
        public string Title { get; set; }
        public string Seniority { get; set; }
        public string State { get; set; }
        public string Party { get; set; }
        public string District { get; set; }
        public string Office { get; set; }
        public string Phone { get; set; }
        public double Missed_Votes_Pct { get; set; }
        public double Votes_with_Party_Pct { get; set; }
        public string Contact_Form { get; set; }
    }
}
