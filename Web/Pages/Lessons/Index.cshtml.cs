using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Subjects;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Web.Pages.Lessons
{
    public class IndexModel(
        ApplicationDbContext context) : PageModel
    {
        public IList<Subject> Subjects { get; set; } = [];

        [BindProperty]
        public required Subject Subject { get; set; }

        public void OnGet()
        {
            Subjects = context.Subjects.ToList();
        }

        public IActionResult OnPost()
        {
            context.Subjects.Add(Subject);
            context.SaveChanges();
            return RedirectToPage();
        }
    }
}
