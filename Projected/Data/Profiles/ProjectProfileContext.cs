using Microsoft.EntityFrameworkCore;
using Projected.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.Data.Profiles
{
    public class ProjectProfileContext : DbContext
    {
        public ProjectProfileContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ProjectProfile> Profiles { get; set; }
    }
}
