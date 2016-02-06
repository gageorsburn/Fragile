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

        public bool Read { get; set; }

        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public string IPAddress { get; set; }

        public DateTime Date { get; set; }
    }
}
