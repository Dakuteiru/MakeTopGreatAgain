using AutoMapper;
using MakeTopGreatAgain.Data;
using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace MakeTopGreatAgain.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RolesController(IMapper mapper,
    ApplicationDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManeger) : ControllerBase
{






    /*public async Task Example()
    {
        await roleManeger.CreateAsync(new IdentityRole("admin")); создать роль
        await userManager.AddToRoleAsync(user, "admin"); назначить роль
        await userManager.GetRolesAsync(user); список ролей
        await userManager.RemoveFromRoleAsync(user,"admin"); забрать роль

        await roleManeger.FindByIdAsync();
        await roleManeger.DeleteAsync(); 

    }*/
    [HttpPost]
    [Authorize(Roles = "admin, modder, manager")]
    public async Task Post(UserData targetUser, string NameRole)
    {
        var chosenUser = mapper.Map<User>(targetUser);
        var user = await userManager.FindByIdAsync(chosenUser.Id);

        await userManager.AddToRoleAsync(user, NameRole);
    }


    [HttpPut]
    [Authorize(Roles = "admin, modder")]
    public async Task Put(string NameRole)
    {
        //var user = await userManager.GetUserAsync(User);

        await roleManeger.CreateAsync(new IdentityRole(NameRole));
        // await userManager.AddToRoleAsync(user, "admin");

    }
    [HttpGet]
    //[Authorize(Roles = "admin")]
   // [Authorize(Roles = "modder")]//тут две роли будут только доспускать
    public async Task<IList<string>> Get(string UserEmail)

    {
        var user = await userManager.FindByEmailAsync(UserEmail);
        return await userManager.GetRolesAsync(user); 
    }

    [HttpGet("allRoles")]
    [Authorize(Roles = "admin")]
    public async Task<ActionResult<IEnumerable<IdentityRole>>> GetAllRoles()

    {
        var roles = await context.Roles.ToListAsync();
        return roles;
    }


    [HttpDelete]
    [Authorize(Roles = "admin, modder")]
    public async Task Del(string? UserID , string targerRole)
    {
        if (UserID != null)
        {
            var user = await userManager.FindByIdAsync(UserID);
            await userManager.RemoveFromRoleAsync(user, targerRole);

        }
        else
        {
            var role = await roleManeger.FindByNameAsync(targerRole);
            await roleManeger.DeleteAsync(role);
        }
       
    }
   
}
