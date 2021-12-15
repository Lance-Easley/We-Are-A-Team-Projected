using Projected.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.Data.Profiles
{
    public interface IProjectProfileRepo
    {
        bool SaveChanges();

        Task<IEnumerable<ProjectProfileReadDto>> GetAllProfilesAsync();
        Task<ProjectProfileReadDto> GetProfileByIdAsync(int id);
        Task<bool> AddProfileAsync(ProjectProfileCreateDto createDto);
    }
}
