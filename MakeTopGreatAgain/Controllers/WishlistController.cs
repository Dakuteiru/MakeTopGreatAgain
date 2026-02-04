using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        
        //var product = await context.Products.FindAsync(id);

      //  if (product == null)
       // {
       //     return NotFound();
       // }
        
       // user.Wishlist.Add(product);

       // await context.SaveChangesAsync();

        return Ok();
    }
}