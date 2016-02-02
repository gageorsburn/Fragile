using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fragile.Models
{
    public class Project
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Description { get; set; }
        public String ShortDescription { get; set; }
        public String ImageUrl { get; set; }
        public DateTime Date { get; set; }
        public String ClientName { get; set; }
        public String Category { get; set; }
    }
}
