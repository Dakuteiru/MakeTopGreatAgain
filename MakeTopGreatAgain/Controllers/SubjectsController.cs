using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Subjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MakeTopGreatAgain.Controllers;

[Route("[controller]")]
[ApiController]
public class SubjectsController(
    ApplicationDbContext context) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Subject>>> All()
    {
        return await context.Subjects.ToListAsync();
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Subject>> Get(Guid id)
    {
        var item = await context.Subjects.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }
        
        return item;
    }

    [HttpGet("{query}")]
    public async Task<ActionResult<IEnumerable<Subject>>> Find(string query)
    {
        var item = await context.Subjects
            .Where(x => x.Title.Contains(query))
            .ToListAsync();
        
        return item;
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> Delete(Guid id)
    {
        var item = await context.Subjects.FindAsync(id);

        if (item == null)
        {
            return NotFound();
        }
        
        context.Subjects.Remove(item);
        await context.SaveChangesAsync();

        return NoContent();
    }

    [HttpPut]
    public async Task<ActionResult<Subject>> Create(Subject item)
    {
        var entry = await context.Subjects.AddAsync(item);
        await context.SaveChangesAsync();
        
        return entry.Entity;
    }

    [HttpPatch]
    public async Task<ActionResult<Subject>> Edit(Subject item)
    {
        context.Entry(item).State = EntityState.Modified;

        try
        {
            await context.SaveChangesAsync();
        }
        catch
        {
            var obj = await context.Subjects.FindAsync(item.Id);

            if (obj == null)
            {
                return NotFound();
            }

            throw;
        }
        
        return item;
    }

    
}