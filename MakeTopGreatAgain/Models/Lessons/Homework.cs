using System.ComponentModel.DataAnnotations.Schema;

namespace MakeTopGreatAgain.Models.Lessons;

public class Homework
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual Guid Id { get; protected set; }
    
    public virtual string? Description { get; set; }
    public virtual DateTime Deadline { get; set; }
}