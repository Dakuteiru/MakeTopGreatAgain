using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeTopGreatAgain.Models.Users;

public class Group
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual Guid Id { get; protected set; }
    
    [MaxLength(255)]
    public virtual required string Name { get; set; }
    
    public virtual required DateTime StartedAt { get; set; }
    
    public virtual required User Sensei { get; set; }
}