using System.ComponentModel.DataAnnotations.Schema;
using MakeTopGreatAgain.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace MakeTopGreatAgain.Models.Lessons;

[PrimaryKey(nameof(HomeworkId), nameof(StudentId))]
public class HomeworkCompletion
{
    public virtual Guid HomeworkId { get; protected set; }
    public virtual string StudentId { get; protected set; }
    
    [ForeignKey(nameof(HomeworkId))]
    public virtual required Homework Homework { get; set; }
    
    [ForeignKey(nameof(StudentId))]
    public virtual required User Student { get; set; }
    
    public virtual int Score { get; set; }
}