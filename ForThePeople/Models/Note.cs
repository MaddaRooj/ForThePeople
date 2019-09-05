using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ForThePeople.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ApplicationUserId { get; set; }
        public string LegislatureId { get; set; }
        public string RepId { get; set; }
    }
}
