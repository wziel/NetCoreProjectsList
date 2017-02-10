using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreProjectsList.Models
{
    public class Task
    {
        [Key]
        public int? TaskId { get; set; }

        [ForeignKey(nameof(Project))]
        [Required]
        public int? ProjectId { get; set; }

        [Required]
        [MaxLength(64)]
        public string Name { get; set;}

        [MaxLength(2048)]
        public string Description { get; set; }

        public int? HoursSpent { get; set; }

        public int? HoursPredicted { get; set; }

        public int? PercentDone { get; set; }

        public Project Project { get; set; }
    }
}
