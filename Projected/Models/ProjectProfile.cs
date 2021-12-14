using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.Models
{
    public class ProjectProfile
    {
        public int ID { get; set; }
        [Required]
        [MaxLength(50)]
        public string ProjectName { get; set; }
        [Required]
        public string PermissionGroups { get; set; }
        [MaxLength(50)]
        public string Author { get; set; }
        public DateTime? DateSubmitted { get; set; }
        [MaxLength(50)]
        public string ModifiedBy { get; set; }
        public DateTime? DateModified { get; set; }
    }
}
