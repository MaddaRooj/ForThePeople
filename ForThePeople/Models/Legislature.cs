using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForThePeople.Models
{
    public class Legislature
    {
        [Key]
        public string Id { get; set; }
        public List<Result> Results{ get; set; }
    }

    public class Bill
    {
        [Key]
        public string Bill_Id { get; set; }
        public string Title { get; set; }
        public string Short_Title { get; set; }
        public string Sponsor_Name { get; set; }
        public string Sponsor_State { get; set; }
        public string Sponsor_Party { get; set; }
        public string CongressDotGov_Url { get; set; }
        public string Govtrack_Url { get; set; }
        public string Introduced_Date { get; set; }
        public string Last_Vote { get; set; }
        public string Primary_Subject { get; set; }
        public string Summary { get; set; }
        public string Summary_Short { get; set; }
        public string Latest_Major_Action_Date { get; set; }
        public string Latest_Major_Action { get; set; }
    }
}
