using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Projected.Controllers
{
    public class ProjectsController : Controller
    {
        [Route("Projects")]
        public IActionResult Projects()
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Groups = HttpContext.Session.GetString("Groups");

            if (ViewBag.Username == null)
            {
                return Redirect("Account/SignIn");
            }
            return View("ProjectsList");
        }

        [Route("Project/{id:int}")]
        public IActionResult Project(int id)
        {
            ViewBag.Username = HttpContext.Session.GetString("Username");
            ViewBag.Groups = HttpContext.Session.GetString("Groups");

            if (ViewBag.Username == null)
            {
                return Redirect("Account/SignIn");
            }
            ViewBag.id = id;
            return View("Project");
        }
    }
}
