using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace Fragile.Models
{
    public class TeamMember
    {
        [Key]
        public String Name { get; set; }
        public string Email { get; set; }
        public String Role { get; set; }
        public String ProfileImageUrl { get; set; }
        public String TwitterUrl { get; set; }
        public String FacebookUrl { get; set; }
        public String LinkedinUrl { get; set; }

        [NotMapped]
        public PasswordHashModel PasswordHash { get; set; }

        public string SessionKey { get; set; }

        public string PasswordHashData
        {
            get
            {
                return PasswordHash != null ? JsonConvert.SerializeObject(PasswordHash) : null;
            }
            set
            {
                if (value != null)
                    PasswordHash = JsonConvert.DeserializeObject<PasswordHashModel>(value);
                else
                    PasswordHash = null;
            }
        }
    }
}
