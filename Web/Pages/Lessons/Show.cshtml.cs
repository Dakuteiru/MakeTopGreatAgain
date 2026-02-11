using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Subjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Web.Pages.Lessons
{
    public class ShowModel(
        ApplicationDbContext context) : PageModel
    {
        public Subject? Subject { get; set; }

        public IActionResult OnGet(Guid id)
        {
            Subject = context.Subjects.FirstOrDefault(x => x.Id == id);

            if (Subject is null) 
            {
                return NotFound();
            }

            return Page();
        }
    }
}
