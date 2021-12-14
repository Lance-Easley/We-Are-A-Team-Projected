using Microsoft.EntityFrameworkCore;
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

        public async Task<bool> AddProfileAsync(ProjectProfileCreateDto createDto)
        {
            await _context.AddAsync(new ProjectProfile
            {
                ProjectName = createDto.ProjectName,
                PermissionGroups = createDto.PermissionGroups,
                Author = createDto.Author,
                DateSubmitted = createDto.DateSubmitted,
                ModifiedBy = createDto.ModifiedBy,
                DateModified = createDto.DateModified
            });

            return SaveChanges();
        }

        public async Task<IEnumerable<ProjectProfileReadDto>> GetAllProfilesAsync()
        {
            var result = new List<ProjectProfileReadDto>();

            foreach (var row in await _context.Profiles.ToListAsync())
            {
                result.Add(new ProjectProfileReadDto
                {
                    ID = row.ID,
                    ProjectName = row.ProjectName,
                    PermissionGroups = row.PermissionGroups.Split(','),
                    Author = row.Author,
                    DateSubmitted = row.DateSubmitted
                });
            }

            return result;
        }

        public async Task<ProjectProfileReadDto> GetProfileByIdAsync(int id)
        {
            var entity = await _context.Profiles.SingleOrDefaultAsync(p => p.ID == id);

            return new ProjectProfileReadDto
            {
                ID = entity.ID,
                ProjectName = entity.ProjectName,
                PermissionGroups = entity.PermissionGroups.Split(','),
                Author = entity.Author,
                DateSubmitted = entity.DateSubmitted
            };
        }

        public bool SaveChanges()
        {
            return _context.SaveChanges() >= 0;
        }
    }
}
