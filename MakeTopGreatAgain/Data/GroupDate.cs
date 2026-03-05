using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeTopGreatAgain.Data;

public class GroupDate
{
   
    public virtual Guid Id { get; init; }


    public virtual required string Name { get; init; }

    //public virtual required DateTime StartedAt { get; set; }

    public virtual UserData? Sensei { get; init; }
}