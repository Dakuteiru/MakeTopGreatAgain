using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models;
using MakeTopGreatAgain.Models.Subjects;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace MakeTopGreatAgain.Controllers;

[ApiController]
[Route("[controller]")]
public class WishlistController(
    ApplicationDbContext context,
    UserManager<User> userManager
    ) : ControllerBase
{
    [HttpPut("{id:guid}")]
    [Authorize]
    public async Task<ActionResult> Put(Guid id)
    {
        var user = await userManager.GetUserAsync(User);

        if (user == null)
        {
            return Unauthorized();
        }

        var product = await context.Subjects.FindAsync(id);

        if (product == null)
        {
            return NotFound();
        }

        await context.Entry(user).Collection(e => e.Wishlist).LoadAsync();

        user.Wishlist.Add(product);

        await context.SaveChangesAsync();

        return Ok();
    }
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<IList<Subject>>> Get()
    {
        var user = await userManager.GetUserAsync(User);
        await context.Entry(user).Collection(e => e.Wishlist).LoadAsync();
        if (user.Wishlist == null)
        {
            return NotFound();
        }
       
        return user.Wishlist.ToList(); 
    }

  
}