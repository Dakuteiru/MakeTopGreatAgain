using MakeTopGreatAgain.Models.Lessons;
using MakeTopGreatAgain.Models.Subjects;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeTopGreatAgain.Data;

public class LessonBase
{
    public virtual required String SubjectID { get; init; }
    
    public virtual required DateTime StartedAt { get; init; }
}