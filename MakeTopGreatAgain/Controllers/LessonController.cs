using AutoMapper;
using AutoMapper.QueryableExtensions;
using MakeTopGreatAgain.Data;
using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Lessons;
using MakeTopGreatAgain.Models.Subjects;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace MakeTopGreatAgain.Controllers;

[Route("[controller]")]
[ApiController]

public class LessonController(
    ApplicationDbContext context, UserManager<User> userManager, IMapper mapper) : ControllerBase
{

    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IList<LessonGCR>>> Get()
    {
        var user = await userManager.GetUserAsync(User);

        var group = user.Group;
        if (group == null)
        {
            return NotFound();
        }

       var lesson = await context.Lessons
            .Where(x => x.Group.Id==(group.Group.Id))
            .ProjectTo<LessonGCR>(mapper.ConfigurationProvider)
            .ToListAsync();


        if (lesson == null)
        {
            return NotFound();
        }

        return lesson; 
    }
    [HttpPut]
    [Authorize/*(Roles = "modder,adimin")*/]
    public async Task<ActionResult> Put(LessonBase lessonBase)
    {
        
        
        Group group = await context.Groups.FindAsync(lessonBase.GroupID);
        Subject subject = await context.Subjects.FindAsync(lessonBase.SubjectID);
        var les = new Lesson
        {
            Teacher = group.Sensei,
            Group = group,
            Subject = subject,
            Homework = null,
            StartedAt = lessonBase.StartedAt
        };
        var entry = await context.Lessons.AddAsync(les);
        await context.SaveChangesAsync();
        return Ok();
    }




    public class LessonGCR
    {
        public virtual GroupCreateRequest Group { get; init; }

        public virtual required User Teacher { get; set; }
        public virtual required Subject Subject { get; set; }

        public virtual required Homework? Homework { get; set; }

        public virtual required DateTime StartedAt { get; set; }
    }


}