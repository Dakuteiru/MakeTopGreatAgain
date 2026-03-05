using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeTopGreatAgain.Data;

public class GroupCreateRequest
{
    public required string Title { get; init; }
    public DateTime? startsAt { get; init; }
    public String? TeacherId { get; init; }//
}
