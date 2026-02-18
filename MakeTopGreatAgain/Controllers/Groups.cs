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
using Group = MakeTopGreatAgain.Models.Users.Group;

namespace MakeTopGreatAgain.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Groups(ApplicationDbContext context) : ControllerBase
    {
        [HttpPut]
        [Authorize]//admin
        public async Task<ActionResult<Group>> Create(string groupeName, GroupCreateRequest group)
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
            var entry = await context.Groups.AddAsync(new Group
            { 
                Name = group.Title,
                StartedAt=group.startsAt ?? DateTime.Now,
                Sensei = user
            
            });

            entry.Entity.StartedAt = DateTime.Now;
            entry.Entity.Name = groupeName;
            entry.Entity.Sensei = null;
            await context.SaveChangesAsync();
            return entry.Entity;
        }
        [HttpGet]
        [Authorize]//admin
        public async Task<ActionResult<IEnumerable<Group>> >Get()
        {
            return await context.Groups.ToListAsync();
        }
    }
}


public class GroupCreateRequest
{
    public required string Title { get; set; }
    public DateTime? startsAt { get; set; }
    public Guid? TeacherId { get; set; }//
}
