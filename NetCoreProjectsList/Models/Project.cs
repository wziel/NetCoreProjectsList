using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreProjectsList.Models
{
    public class Project
    {
        [Key]
        public int? ProjectId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set; }

        public ICollection<Task> Tasks { get; set; }
    }
}
