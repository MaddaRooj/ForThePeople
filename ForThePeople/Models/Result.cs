using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForThePeople.Models
{
    public class Result
    {
        [Key]
        public string Member_Id { get; set; }
        public List<Member> Members { get; set; }
        public List<Bill> Bills { get; set; }
        public List<Role> Roles { get; set; }
        public List<Committee> Committees { get; set; }
        public List<Result> Results { get; set; }

        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Full_Name
        {
            get { return $"{First_Name} {Last_Name}"; }
        }
        public string Url { get; set; }
        public string Gender { get; set; }
        public string Date_of_Birth { get; set; }
    }
}
