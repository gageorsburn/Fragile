using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fragile.Models
{
    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String PhoneNumber { get; set; }
        public String Message { get; set; }
    }
}
