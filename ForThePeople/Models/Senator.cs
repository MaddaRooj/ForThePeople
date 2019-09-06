using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForThePeople.Models
{
    public class Senator
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public List<Result> Results { get; set; }
    }

    public class Member
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public DateTime Date_of_Birth { get; set; }
        public string Chamber { get; set; }
        public string State { get; set; }
        public int Seniority { get; set; }
        public int District { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public string Gender { get; set; }
        public string Twitter_Account { get; set; }
        public string Facebook_Account { get; set; }
        public string Current_Party { get; set; }
    }
}
