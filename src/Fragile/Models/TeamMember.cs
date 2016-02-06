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
        public string Name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string ProfileImageUrl { get; set; }
        public string TwitterUrl { get; set; }
        public string FacebookUrl { get; set; }
        public string LinkedinUrl { get; set; }
        public string GitHubUrl { get; set; }

        public Guid ResetPasswordToken { get; set; }

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
