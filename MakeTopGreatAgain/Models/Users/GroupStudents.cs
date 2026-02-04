using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace MakeTopGreatAgain.Models.Users;

[PrimaryKey(nameof(GroupId), nameof(StudentId))]
public class GroupStudents
{
    public virtual Guid GroupId { get; protected set; }
    public virtual Guid StudentId { get; protected set; }
    
    [ForeignKey(nameof(GroupId))]
    public virtual required Group Group { get; set; }
    
    [ForeignKey(nameof(StudentId))]
    public virtual required User Student { get; set; }
}