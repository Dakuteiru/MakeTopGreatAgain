using System.ComponentModel.DataAnnotations.Schema;
using MakeTopGreatAgain.Models.Common;
using MakeTopGreatAgain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace MakeTopGreatAgain.Models.Lessons;

[PrimaryKey(nameof(LessonId), nameof(StudentId))]
public class Attendees
{
    public virtual Guid LessonId { get; protected set; }
    public virtual string StudentId { get; protected set; }
    
    [ForeignKey(nameof(LessonId))]
    public virtual required Lesson Lesson { get; set; }
    
    [ForeignKey(nameof(StudentId))]
    public virtual required User Student { get; set; }
    
    public virtual Presence Presence { get; set; }
}