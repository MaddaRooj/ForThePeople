using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForThePeople.Models
{
    public class LocalRep
    {
        [Key]
        public int Id { get; set; }
        public List<Office> Offices { get; set; }
        public List<Official> Officials { get; set; }
    }

    public class Office
    {
        [Key]
        public string DivisionId { get; set; }
        public string Name { get; set; }
        public List<int> OfficialIndices { get; set; }
    }

    public class Official
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Party { get; set; }
        public List<Address> Addresses { get; set; }
        public List<string> Phones { get; set; }
        public List<string> Emails { get; set; }
        public List<string> Urls { get; set; }
        public string PhotoUrl { get; set; }
        public List<Channel> Channels { get; set; }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip { get; set; }
    }

    public class Channel
    {
        public string Type { get; set; }
        public string Id { get; set; }
    }
}
