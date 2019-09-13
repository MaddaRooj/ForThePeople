using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ForThePeople.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        public string Content { get; set; }
        [Required]
        public string UserId { get; set; }
        public string RepId { get; set; }
        public string BillId { get; set; }
    }
}
