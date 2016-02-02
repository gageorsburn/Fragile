using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace Fragile.Models
{
    public class Service
    {
        [Key]
        public int ServiceID { get; set; }
        public string Heading { get; set; }
        public string Description { get; set; }
        public string ImageName { get; set; }
    }
}
