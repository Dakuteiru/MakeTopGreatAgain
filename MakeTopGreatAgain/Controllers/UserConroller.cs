using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using AutoMapper;
using MakeTopGreatAgain.Data;
using MakeTopGreatAgain.Models.Subjects;
using Group = MakeTopGreatAgain.Models.Users.Group;

namespace MakeTopGreatAgain.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserConroller(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context) : ControllerBase
    {
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<GroupSt>> Get()
        {
            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return Unauthorized();
            }
            var gcr =  mapper.Map<GroupSt>(user.Group);
            return gcr;
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

            await context.SaveChangesAsync();

            return Ok();
        }
        [HttpPost("join")]
        [Authorize]
        public async Task<IActionResult> JoinGroup(Guid GroupID)
        {
            
            var user = await userManager.GetUserAsync(User);
            var group = await context.Groups.FindAsync(GroupID);
            var groupStudents= new GroupStudents
            {
                Student= user,
                Group=group
            }; 
            user.Group= groupStudents;

           group.UsersSt.Add(groupStudents);
            await context.SaveChangesAsync();
            return Ok();
        }

      
    }
}
public class GroupSt
{
    public virtual GroupCreateRequest Group { get; set; }
    
    public virtual IdentityRole? Role { get; set; }
   
    public virtual string? Name { get; set; }
   
    public virtual string? Surname { get; set; }
    
    public virtual DateTime? BirthDate { get; set; }

    public virtual IList<Subject>? Wishlist { get; set; }
}