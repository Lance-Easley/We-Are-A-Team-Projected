using Projected.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.Data.Profiles
{
    public interface IProjectProfileRepo
    {
        Task<IEnumerable<ProjectProfile>> GetAllProfilesAsync();
        Task<ProjectProfile> GetProfileByIdAsync(int id);
    }
}
