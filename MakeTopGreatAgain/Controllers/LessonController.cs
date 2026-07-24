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

    [HttpPost]
    [Authorize]
    public async Task<ActionResult> Update(Guid LessonId,  Guid? SubjectId, string? TeachId, DateTime dateTime)
    {
        var lesson = await context.Lessons.FindAsync(LessonId);
        if (SubjectId != null)
        {
            lesson.Subject= await context.Subjects.FindAsync(SubjectId);;
            
        }

        if (TeachId != null)
        {
            lesson.Teacher = await userManager.FindByIdAsync(TeachId);
        }

        if (dateTime != null)
        {
            lesson.StartedAt = dateTime;
        }

        await context.SaveChangesAsync();

        return Ok();
    }

    [HttpGet("forTeacher")]
    [Authorize]
    public async Task<ActionResult<IList<LessonGCR>>> GetByGroup(string TeacherId)
    {
        var Lesson = await context.Lessons
            .Where(x =>x.Teacher.Id==(TeacherId))
            .ProjectTo<LessonGCR>(mapper.ConfigurationProvider)
            .ToListAsync();
        return Lesson;

    }
    [HttpGet("byId")]
    [Authorize]
    public async Task<ActionResult<IList<LessonGCR>>> GetByGroup(Guid groupId)
    {
        var Lesson = await context.Lessons
            .Where(x =>x.Group.Id==(groupId))
            .ProjectTo<LessonGCR>(mapper.ConfigurationProvider)
            .ToListAsync();
        return Lesson;

    }

    [HttpGet("forStudent")]
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
            .Where(x => x.Group.Id==(group.GroupId))
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
        var user = await userManager.FindByIdAsync(lessonBase.TeacherId);
        var les = new Lesson
        {
            Teacher = user ,
            Group = group,
            Subject = subject,
            Homework = null,
            StartedAt = lessonBase.StartedAt
        };
        var entry = await context.Lessons.AddAsync(les);
           
        if (!await context.Lessons
                .Where(x => x.Group.Id==(group.Id))
                .Where(x =>(x.StartedAt>lessonBase.StartedAt.AddMinutes(-90)
                    && x.StartedAt<lessonBase.StartedAt)
                || (x.StartedAt<lessonBase.StartedAt.AddMinutes(90)
                    && x.StartedAt>lessonBase.StartedAt))
                .Where(x => x.StartedAt.Day==( lessonBase.StartedAt.Day))
                .Where(x => x.StartedAt.Year==( lessonBase.StartedAt.Year))
                .Where(x => x.StartedAt.Month==( lessonBase.StartedAt.Month))
                .AnyAsync()
            )
        {
            await context.SaveChangesAsync();
            return Ok();
        }
            
        //var  a= lessonBase.StartedAt.Minute;//(int) x.StartedAt.TimeOfDay.TotalMinutes//lessonBase.StartedAt
        throw new InvalidOperationException($"this group already have lesson at  { lessonBase.StartedAt} ");
        
        
        
    }

    [HttpDelete]
    [Authorize]
    public async Task Del(Guid LessonId)
    {
        var lesson = await context.Lessons.FindAsync(LessonId);
        context.Lessons.Remove(lesson);
        await context.SaveChangesAsync();
    }





    public class LessonGCR
    {
        public virtual Guid LessonId { get; set; }
        public virtual GroupCreateRequest Group { get; init; }

        public virtual required String TeacherName { get; set; }
        
        public virtual required String TeacherSurname { get; set; }
        
        public virtual required Subject Subject { get; set; }

        public virtual required Homework? Homework { get; set; }

        public virtual required DateTime StartedAt { get; set; }
    }


}