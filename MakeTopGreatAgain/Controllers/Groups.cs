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
    public class Groups(IMapper mapper,ApplicationDbContext context) : ControllerBase
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
