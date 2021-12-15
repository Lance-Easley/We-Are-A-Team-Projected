using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.Data.Profiles
{
    public class ProjectProfileCreateDto
    {
        [Required]
        [MaxLength(50)]
        public string ProjectName { get; set; }
        [Required]
        public string PermissionGroups { get; set; } // Likely stored as a comma-separated-string
        [MaxLength(50)]
        public string Author { get; set; }
        public DateTime? DateSubmitted { get; set; }
        [MaxLength(50)]
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
