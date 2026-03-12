using MakeTopGreatAgain.Models.Lessons;
using MakeTopGreatAgain.Models.Subjects;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeTopGreatAgain.Data;

public class LessonBase
{
    public virtual Guid GroupID { get; init; }
    public virtual required Guid SubjectID { get; init; }
    
    public virtual required DateTime StartedAt { get; init; }
}