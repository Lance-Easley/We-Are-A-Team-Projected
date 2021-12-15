using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.Controllers
{
    public class ProjectsController : Controller
    {
        [Route("Projects/{id:int}")]
        public IActionResult Projects(int id)
        {
            ViewBag.id = id;
            return View();
        }
    }
}
