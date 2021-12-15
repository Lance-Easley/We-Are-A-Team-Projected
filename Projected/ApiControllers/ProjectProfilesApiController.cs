using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Projected.Data.Profiles;
using Projected.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.ApiControllers
{
    [Route("api/profiles")]
    [ApiController]
    public class ProjectProfilesApiController : ControllerBase
    {
        private readonly IProjectProfileRepo _repo;

        public ProjectProfilesApiController(IProjectProfileRepo repo)
        {
            _repo = repo;
        }

        //GET api/profiles
        [HttpGet]
        public async Task<IActionResult> GetAllProfiles()
        {
            return Ok(await _repo.GetAllProfilesAsync());
        }

        //GET api/profiles/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetProfileById(int id)
        {
            return Ok(await _repo.GetProfileByIdAsync(id));
        }

        //POST api/profiles
        [HttpPost]
        public async Task<IActionResult> AddProfileAsync(ProjectProfileCreateDto createDto)
        {
            await _repo.AddProfileAsync(createDto);

            return NoContent();
        }
    }
}
