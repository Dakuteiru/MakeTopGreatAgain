using MakeTopGreatAgain.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakeTopGreatAgain.Data;

public class GroupCreateRequest
{
    [Required(ErrorMessage ="Необходимо указать название группа")]
    [MinLength(10, ErrorMessage ="Название группа должно содержать как минимум 10 символов")]
    public required string Title { get; init; }
    [afterDate("2026-01-26T16:25:17.798Z")]
    public DateTime? startsAt { get; init; }
    public String? TeacherId { get; init; }//
}
