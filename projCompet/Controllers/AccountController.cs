using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using projCompet.ViewModels;
using projCompet.Data;
using projCompet.Models;
using Microsoft.EntityFrameworkCore;

namespace projCompet.Controllers
{
    public class AccountController : Controller
    {
        public async Task Login(string returnUrl = "/")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Authorize]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync("Auth0", new AuthenticationProperties
            {
                // Indicate here where Auth0 should redirect the user after a logout.
                // Note that the resulting absolute Uri must be whitelisted in the 
                // **Allowed Logout URLs** settings for the client.
                RedirectUri = Url.Action("Index", "Home")
            });
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }

        [Authorize]
        public IActionResult Profile()
        {
            //string role = User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
            //string firstName = User.FindFirst("given_name")?.Value;
            //string lastName = User.FindFirst("family_name")?.Value;
            //string email = User.FindFirst("email")?.Value;

            //var customer = new Customer
            //{
            //    Roles = new List<Role> listRoles(),
            //    FirstName = firstName,
            //    LastName = lastName,
            //    Email = email
            //};

            //using (var context = new projCompetContext())
            //{
            //    var existingCustomer = context.Customer.FirstOrDefault(x => x.Email == customer.Email);
            //    if (existingCustomer == null)
            //    {
            //        context.Customer.Add(customer);
            //        context.SaveChanges();
            //    }
            //}

            return View(new UserProfileViewModel()
            {
                Name = User.Identity.Name,
                EmailAddress = User.FindFirst(c => c.Type == ClaimTypes.Email)?.Value,
                ProfileImage = User.FindFirst(c => c.Type == "picture")?.Value
            });
        }

        /// <summary>
        /// This is just a helper action to enable you to easily see all claims related to a user. It helps when debugging your
        /// application to see the in claims populated from the Auth0 ID Token
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public IActionResult Claims()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetRole()
        {
            string connectionString = Environment.GetEnvironmentVariable("connectionString");
            string role = User.FindFirst(c => c.Type == ClaimTypes.Role)?.Value;
            using (var context = new projCompetContext(new DbContextOptionsBuilder<projCompetContext>().UseSqlServer(connectionString).Options))
            {
                var existingRole = context.Roles.FirstOrDefault(x => x.Name == role);
                if (existingRole == null)
                {
                    Role realRole = new Role() { Id = new Guid(), Name = role };
                    context.Roles.Add(realRole);
                    context.SaveChanges();
                }
            }
            return Ok();
        }

        //[HttpGet]
        //public IActionResult GetClaims()
        //{

        //}

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
