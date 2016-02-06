using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Fragile.Models
{
    public class Blog
    {
        [Key]
        public int Id { get; set; }

        public string Title { get; set; }
        public string Body { get; set; }
        public string AuthorName { get; set; }

        public DateTime PostDate { get; set; }
    }
}
