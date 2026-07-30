using System.Runtime.InteropServices.JavaScript;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MakeTopGreatAgain.Data;
using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Lessons;
using MakeTopGreatAgain.Models.Users;
using MakeTopGreatAgain.Models.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace MakeTopGreatAgain.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AttendeesController(IMapper mapper, UserManager<User> userManager, ApplicationDbContext context)
        : Controller
    {
            [HttpPut]
            public async Task<ActionResult<Attendees>> Insert(Guid Lesson, String StudentId, Presence presence)
            {
                var user = await userManager.FindByIdAsync(StudentId);
                var lesson = await context.Lessons.FindAsync(Lesson);
                Attendees antendess = new Attendees
                {
                    Lesson = lesson,
                    Student = user,
                    Presence = presence
                };
                context.Attendees.AddAsync(antendess);
                await context.SaveChangesAsync();
                return Ok();
            }
        [HttpGet]
        public async Task<ActionResult<IList<GetAttend>>> Get(Guid Lesson)
        {
            var attendees = await context.Attendees.Where(x => x.LessonId== Lesson)
                .ProjectTo<GetAttend>(mapper.ConfigurationProvider)
                .ToListAsync();
           
            return attendees;
        }
        [HttpGet("allLessons")]
        public async Task<ActionResult<IList<GetAttend>>> GetAll(Guid GroupId)
        {
            try
            {
                var attendees = await context.Attendees.Where(x => x.Student.Group.GroupId == GroupId)
                    .ProjectTo<GetAttend>(mapper.ConfigurationProvider)
                    .ToListAsync();
                return attendees;
            }
            catch
            {
                return NotFound();
            }




        }
        [HttpGet("currentStudent")]
        public async Task<ActionResult<IList<GetAttend>>> GetStudent( string StudentId)
        {
            var user = await userManager.FindByIdAsync(StudentId);
            if (user == null)
            {
                return NotFound();
            }

            var attendees = await context.Attendees.Where(x => x.StudentId == StudentId)
                .ProjectTo<GetAttend>(mapper.ConfigurationProvider)
                .ToListAsync();
           
            return attendees;
        }
        [HttpPost]
        public async Task<ActionResult<Attendees>> allLate(Guid Lesson)
        {
            var lesson = await context.Lessons.FindAsync(Lesson);
            if (lesson == null)
            {
                return NotFound();
            }

            //List<Attendees> Att = null;
           // List<User> Students = null;
            
            
            var absents = await context.Users
                .Where(x => x.Group != null)
                .Where(x => x.Group!.GroupId == lesson.Group.Id) 
                .Where(x => !context.Attendees.Any(a => a.LessonId != lesson.Id && a.StudentId != x.Id))
                .Select(x => new Attendees
                {
                    Lesson = lesson,
                    Student = x,
                    Presence = Presence.Absence
                })
                .ToListAsync();

            await context.Attendees.AddRangeAsync(absents);
            /*try
            {
                Att = await context.Attendees.Where(x => x.Lesson.Id == lesson.Id)
                    .ToListAsync();

                Students = await context.Users.Where(x=>x.Group.GroupId == lesson.Group.Id)
                    .Where(x => Att.All(y => y.StudentId != x.Id))
                    .ToListAsync();
            }
            catch
            {
                Students = await context.Users
                    .Where(x => x.Group.GroupId == lesson.Group.Id)
                    .ToListAsync();

                //throw new InvalidOperationException($"use Imagination ");

            }
            context.Users.ForEachAsync()
             foreach (var Std in Students)
            {
                Attendees antendess = new Attendees
                 {
                     Lesson = lesson,
                     Student = Std,
                     Presence = Presence.Absence
                 };
                 context.Attendees.AddAsync(antendess);
                 await context.SaveChangesAsync();
             }*/
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPatch]
        public async Task<ActionResult<Attendees>> ForLate(Guid Lesson, String StudentId, Presence presence)
        {
            var lesson = await context.Attendees.FindAsync(Lesson,StudentId);

            context.Entry(Lesson).State = EntityState.Modified;
            try
            {
                await context.SaveChangesAsync();
            }
            catch
            {
                throw;
            }
            return lesson;
        }
    }


}