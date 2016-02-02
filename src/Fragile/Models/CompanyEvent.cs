using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fragile.Models
{
    public class CompanyEvent
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public String Title { get; set; }
        public String Description { get; set; }
        public String ImageUrl { get; set; }
    }
}
