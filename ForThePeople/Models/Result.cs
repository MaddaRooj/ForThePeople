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
        public int Id { get; set; }
        public List<Member> Members { get; set; }
        public List<Bill> Bills { get; set; }
    }
}
