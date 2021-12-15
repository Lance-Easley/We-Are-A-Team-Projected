using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.Data.Profiles
{
    public class ProjectProfileReadDto
    {
        public int ID { get; set; }
        public string ProjectName { get; set; }
        public string[] PermissionGroups { get; set; }
        public string Author { get; set; }
        public DateTime? DateSubmitted { get; set; }
    }
}
