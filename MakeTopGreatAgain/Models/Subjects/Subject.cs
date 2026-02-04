using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeTopGreatAgain.Models.Subjects;

public class Subject
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual Guid Id { get; protected set; }
    
    [MaxLength(255)]
    public virtual required string Title { get; set; }
}