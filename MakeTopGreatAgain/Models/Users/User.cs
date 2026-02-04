using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MakeTopGreatAgain.Models.Users;

public class User : IdentityUser
{
    [MaxLength(255)]
    public virtual string? Name { get; set; }
    [MaxLength(255)]
    public virtual string? Surname { get; set; }
    
    public virtual DateTime? BirthDate { get; set; }

    public virtual IList<Product>? Wishlist { get; set; }
}