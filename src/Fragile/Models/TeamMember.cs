using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fragile.Models
{
    public class TeamMember
    {
        [Key]
        public String Name { get; set; }
        public String Role { get; set; }
        public String ProfileImageUrl { get; set; }
        public String TwitterUrl { get; set; }
        public String FacebookUrl { get; set; }
        public String LinkedinUrl { get; set; }
    }
}
