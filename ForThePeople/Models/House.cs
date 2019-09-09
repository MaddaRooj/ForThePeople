using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForThePeople.Models
{
    public class House
    {
        [Key]
        public int Id { get; set; }
        public string Status { get; set; }
        public List<Result> Results { get; set; }
    }
}