using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Projected.Data.LDAP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Projected.Controllers
{
    public class AccountController : Controller
    {
        private readonly string _releaseName;
        public IConfiguration _configuration { get; }
        private readonly IConnectToAuthLdapBehavior _connectToAuthLdapBehavior;


        /// <summary>
        /// Initializes a new instance of the <see cref="AccountController"/> class.
        /// </summary>
        /// <param name="appUserUtility">The application user utility.</param>
        /// <param name="configurationSettings">The configuration settings.</param>
        public AccountController(IConfiguration configuration, IConnectToAuthLdapBehavior connectToAuthLdapBehavior)
        {
            _configuration = configuration;
            _connectToAuthLdapBehavior = connectToAuthLdapBehavior;
        }

        /// <summary>
        /// Signs the in.
        /// </summary>
        /// <returns></returns>
        [Route("Account/SignIn")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult SignIn()
        {
            ViewBag.PageTitle = "Sign In";
            ViewBag.ReleaseName = _releaseName;
            return View("SignIn");
        }

        /// <summary>
        /// Unauthorizeds the request.
        /// </summary>
        /// <returns></returns>
        [Route("Account/Unauthorized")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult UnauthorizedRequest()
        {
            ViewBag.ReleaseName = _releaseName;
            return View("Unauthorized");
        }

        /// <summary>
        /// Represents an event that is raised when the sign-out operation is complete.
        /// </summary>
        /// <returns></returns>
        [Route("Account/SignOut")]
        [HttpGet]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> SignOut()
        {
            ViewBag.ReleaseName = _releaseName;
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Remove("Username");
            HttpContext.Session.Remove("Groups");
            return RedirectToAction("SignIn");
        }

        /// <summary>
        /// Signs the in.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password.</param>
        /// <param name="returnUrl">The return URL.</param>
        /// <returns></returns>
        [Route("Account/SignIn")]
        [HttpPost]
        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<IActionResult> SignIn(string username, string password)
        {
            var groups = _connectToAuthLdapBehavior.GetGroups(username, password);
            var groupString = groups.Aggregate((x, y) => x + "|" + y);
            var identity = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(ClaimTypes.Name, username),
                    new Claim("Groups", groupString)
                }, 
                CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(identity),
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddHours(24)
                });

            HttpContext.Session.SetString("Groups", groupString);
            HttpContext.Session.SetString("Username", username);

            return Ok(User.Identity.ToString());
        }
    }
}
