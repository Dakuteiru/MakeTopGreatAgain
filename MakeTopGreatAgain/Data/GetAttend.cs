using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MakeTopGreatAgain.Models.Common;

namespace MakeTopGreatAgain.Data;

public class GetAttend
{
   
    public virtual Guid Id { get; init; }//lesson id


    public virtual required string Name { get; init; }//student name

    //public virtual required DateTime StartedAt { get; set; }

    public virtual Presence? Presence { get; init; }
}