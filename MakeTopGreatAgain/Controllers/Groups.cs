using System.Net;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MakeTopGreatAgain.Data;
using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Subjects;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using System.Text.RegularExpressions;
using MakeTopGreatAgain.Middleware.Restrict;
using Group = MakeTopGreatAgain.Models.Users.Group;

namespace MakeTopGreatAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Groups(IMapper mapper,ApplicationDbContext context,UserManager<User> userManager) : ControllerBase
    {

        [HttpGet]
//[Restrict(["127.0.0.1"])]
        public async Task<ActionResult<ICollection<GroupDate>>> Index()
        {
            return await context.Groups
                .Include(x => x.Sensei)
                .ProjectTo<GroupDate>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
        [HttpGet("studentsCurrentGroup")]
//[Restrict(["127.0.0.1"])]
        public async Task<ActionResult<ICollection<GroupDateStudents>>> IndexCurrenGroup(Guid groupId)
        {
            return await context.Groups
                .Include(x => x.Sensei)
                .Where(x =>  x.Id==groupId)
                .ProjectTo<GroupDateStudents>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
        [HttpGet("studentsAllGroups")]
//[Restrict(["127.0.0.1"])]
        public async Task<ActionResult<ICollection<GroupDateStudents>>> IndexFullList()
        {
            return await context.Groups
                .Include(x => x.Sensei)
                .ProjectTo<GroupDateStudents>(mapper.ConfigurationProvider)
                .ToListAsync();
        }
        [HttpPut]
        [Authorize]//admin
        public async Task<ActionResult<Group>> Create(GroupCreateRequest group)
        {
            User user = null;
            if (group.TeacherId is not null)
            {
                user = await context.Users.FindAsync(group.TeacherId);
              
                if (user is null)
                {
                    return NotFound();
                }
            }
            
            var gcr =  mapper.Map<Group>(group);
            gcr.Sensei = user;
            context.Groups.Add(gcr);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("ChangeSensei")]
        [Authorize]
        public async Task<IActionResult> Update(string userId, Guid groupId)
        {
            var user= await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            var group = await context.Groups.FindAsync(groupId);
            group.Sensei = user;
            await context.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete]
        [Authorize]
        public async Task Del(Guid GroupId)
        {
            var groups = await context.Groups.FindAsync(GroupId);
            context.Groups.Remove(groups);
            await context.SaveChangesAsync();
        }
        /* [HttpGet]
         [Authorize]//admin
         public async Task<ActionResult<IEnumerable<Group>> >Get()
         {
             return await context.Groups.ToListAsync();
         }*/
    }
}


/*public class GroupCreateRequest
{
    public required string Title { get; set; }
    public DateTime? startsAt { get; set; }
    public Guid? TeacherId { get; set; }//
}*/
