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
        //public List<Committee> Committees { get; set; }
        public List<Result> Results { get; set; }
        public Note Note { get; set; }
        public string First_Name { get; set; }
        public string Last_Name { get; set; }
        public string Full_Name
        {
            get { return $"{First_Name} {Last_Name}"; }
        }
        public string Url { get; set; }
        public string Gender { get; set; }
        public string Date_of_Birth { get; set; }
        public string Bill_Id { get; set; }
        public string Bill_Slug { get; set; }
        public string Bill { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Latest_Major_Action { get; set; }
        public string Latest_Major_Action_Date { get; set; }
        public string Primary_Subject { get; set; }
        public string Sponsor_Title { get; set; }
        public string Sponsor { get; set; }
        public string Govtrack_Url { get; set; }
        public string Congressdotgov_Url { get; set; }
    }
}
