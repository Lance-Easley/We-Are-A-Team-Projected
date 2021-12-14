using Projected.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.Data.Profiles
{
    public class ProjectProfileRepo : IProjectProfileRepo
    {
        private readonly ProjectProfileContext _context;

        public ProjectProfileRepo(ProjectProfileContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjectProfile>> GetAllProfilesAsync()
        {
            return _context.Profiles.ToList();
        }

        public async Task<ProjectProfile> GetProfileByIdAsync(int id)
        {
            return _context.Profiles.SingleOrDefault(p => p.ID == id);
        }
    }
}
