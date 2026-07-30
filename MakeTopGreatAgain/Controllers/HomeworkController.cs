using AutoMapper;
using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Lessons;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakeTopGreatAgain.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HomeworkController(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context)
        : ControllerBase
    {
        [HttpPut]
        public async Task<ActionResult> Homework(Homework homework, Guid groupId)
        {
            var entry = await context.Homeworks.AddAsync(homework);

            var Homeworks = await context.Users
                .Where(x => x.Group != null)
                .Where(x => x.Group!.GroupId == groupId)
                .Where(x => !context.HomeworkCompletions.Any(y => y.StudentId != x.Id && y.HomeworkId != homework.Id))
                .Select(x => new HomeworkCompletion
                {
                    Homework = homework,
                    Student = x,
                    Score = 0
                }).ToListAsync();
            await context.HomeworkCompletions.AddRangeAsync(Homeworks);
            await context.SaveChangesAsync();
            return Ok();
        }
        
        [HttpPut("Mark")]
        public async Task<ActionResult> MarkHomework(Guid Id, string UserID, int score)
        {
            
            var user = await userManager.FindByIdAsync(UserID);

            var homework = await context.Homeworks.FindAsync(Id);
            
            var hmw = await  context.HomeworkCompletions.FindAsync(homework.Id, UserID);
            hmw.Score = score;
            await context.SaveChangesAsync();
            return Ok();
            
            /*
            var hmwComplet = new HomeworkCompletion
            {
                Homework = homework,
                Student =  user,
                Score = score
            };
            var entry = await context.HomeworkCompletions.AddAsync(hmwComplet);
            await context.SaveChangesAsync();
            return Ok();*/
        }
        

        [HttpGet]
        [Authorize]
        public async Task<ActionResult<IList<HwCOutput>>> Get()
        {
            var user = await userManager.GetUserAsync(User);
            //var myHomework = await context.HomeworkCompletions
              //  .Where(x => x.StudentId == user.Id)
              //  .ToListAsync();
            
            var homework = await context.HomeworkCompletions.Where(x => x.StudentId == (user.Id)).ToListAsync();
            var hmwoutput = mapper.Map<List<HwCOutput>>(homework);
            return hmwoutput;
        }
        [HttpGet("Teacher")]
        [Authorize]
        public async Task<ActionResult<IList<HwCOutput>>> GetSomeone(string UserID)
        {
            //var myHomework = await context.HomeworkCompletions
            //  .Where(x => x.StudentId == user.Id)
            //  .ToListAsync();
            
            var homework = await context.HomeworkCompletions.Where(x => x.StudentId == UserID).ToListAsync();
            var hmwoutput = mapper.Map<List<HwCOutput>>(homework);
            return hmwoutput;
        }
        [HttpGet("studentById")]
        [Authorize]
        public async Task<ActionResult<IList<HwCOutput>>> GetMe(Guid id)
        {
            var user = await userManager.GetUserAsync(User);
            //var myHomework = await context.HomeworkCompletions
            //  .Where(x => x.StudentId == user.Id)
            //  .ToListAsync();
            var homework = await context.HomeworkCompletions
                .Where(x => x.StudentId == (user.Id))
                .Where(x =>x.HomeworkId == id).ToListAsync();
            var hmwoutput = mapper.Map<List<HwCOutput>>(homework);
            return hmwoutput;
        }

        [HttpGet("student")]
        public async Task<ActionResult<IEnumerable<Homework>>> GetHomework()
        {
            var homeworks = await context.Homeworks.ToListAsync();
            return homeworks;
        }
        [HttpDelete]
        [Authorize]
        public async Task Del(Guid HomeworkID)
        {
            var homework = await context.Homeworks.FindAsync(HomeworkID);
            context.Homeworks.Remove(homework);
            await context.SaveChangesAsync();
        }

        [HttpPatch("StudentFile")]
        [Authorize]
        public async Task<ActionResult<HomeworkCompletion>> AddHomework(String FileName, Guid HomeworkID)
        {
            var user = await userManager.GetUserAsync(User);
            var homework = await context.Homeworks.FindAsync(HomeworkID);
            
            var HomwC = await context.HomeworkCompletions.FindAsync(user.Id, HomeworkID);
            HomwC.StudentFile = FileName;
            await context.SaveChangesAsync();
            return Ok();
                
            
            /*
            HomeworkCompletion HmwC = new HomeworkCompletion
            {
                Homework = homework,
                Student =  user,
                Score = 0,
                StudentFile = FileName
            };
            await  context.HomeworkCompletions.AddAsync(HmwC);
            await context.SaveChangesAsync();
            return Ok();
            */
        }
        [HttpPatch("TeacherFile")]
        public async Task<ActionResult<Homework>> Patch(string? file, string? desc, Guid HomeworkID)
        {
            var homework = await context.Homeworks.FindAsync(HomeworkID);
            if(file!=null)
                homework.TeacherFile = file;
            if(desc!=null) 
                homework.Description = desc;
            await context.SaveChangesAsync();
            return Ok();
        }
    }

}

public class HwCOutput
{
    public virtual Homework Homework { get; set; }
    public virtual string? Name { get; set; }
   
    public virtual string? Surname { get; set; }
    public virtual int score { get; set; }
    public virtual string? StudentFile { get; set; }
}