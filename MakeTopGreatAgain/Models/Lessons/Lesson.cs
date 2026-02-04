using System.ComponentModel.DataAnnotations.Schema;
using MakeTopGreatAgain.Models.Subjects;
using MakeTopGreatAgain.Models.Users;

namespace MakeTopGreatAgain.Models.Lessons;

public class Lesson
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual Guid Id { get; protected set; }
    
    public virtual required User Teacher { get; set; } 
    public virtual required Group Group { get; set; } 
    public virtual required Subject Subject { get; set; }
    
    public virtual required Homework Homework { get; set; }
    
    public virtual required DateTime StartedAt { get; set; }
}