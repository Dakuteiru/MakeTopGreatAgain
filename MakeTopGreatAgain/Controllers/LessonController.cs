using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Lessons;
using MakeTopGreatAgain.Models.Subjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakeTopGreatAgain.Controllers;

[Route("[controller]")]
[ApiController]

public class LessonController(
    ApplicationDbContext context) : ControllerBase
{

    [HttpGet]
    public async Task<ActionResult<Lesson>> Get()
    {
        var lesson=await context.Lessons.FirstOrDefaultAsync();

        if (lesson == null)
        {
            return NotFound();
        }
        return lesson;
    }
}