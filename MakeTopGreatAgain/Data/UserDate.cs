using MakeTopGreatAgain.Models.Subjects;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MakeTopGreatAgain.Data;

public class UserData
{
    public required string Id { get; init; }
    public string? Email { get; init; }
}