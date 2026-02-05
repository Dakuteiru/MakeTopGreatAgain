using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace MakeTopGreatAgain.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserConroller(UserManager<User> userManager, ApplicationDbContext context) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<User>> Get()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Update(string name, string surName)
        {
            var user = await userManager.GetUserAsync(User);

            if (name == null || surName == null)
            {
                return NotFound();
            }
            user.Name = name;
            user.Surname = surName;


            return Ok();
        }
    }
}
