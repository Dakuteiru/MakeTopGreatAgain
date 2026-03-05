using AutoMapper;
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
    public async Task<ActionResult<IList<Lesson>>> Get()
    {
        var user = await userManager.GetUserAsync(User);

        var group = user.Group;
        if (group == null)
        {
            return NotFound();
        }

        var lesson = await context.Lessons
            .Where(x => x.Group==(group))
            .ToListAsync();


        if (lesson == null)
        {
            return NotFound();
        }
        return lesson;
    }
    [HttpPut]
    [Authorize(Roles = "modder,adimin")]
    public async Task<ActionResult> Put(LessonBase lessonBase, Guid IDGroupe)
    {
        var lessons = mapper.Map<Lesson>(lessonBase);
        Group group = await context.Groups.FindAsync(IDGroupe);
        lessons.Group = group;
        await context.SaveChangesAsync();
        return Ok();
    }
    


   



}