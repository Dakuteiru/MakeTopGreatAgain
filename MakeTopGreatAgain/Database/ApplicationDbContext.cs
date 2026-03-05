using MakeTopGreatAgain.Models;
using MakeTopGreatAgain.Models.Lessons;
using MakeTopGreatAgain.Models.Subjects;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MakeTopGreatAgain.Database;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<User>(options)
{
    public DbSet<Group> Groups { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<Homework> Homeworks { get; set; }
    public DbSet<Attendees> Attendees { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<HomeworkCompletion> HomeworkCompletions { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>()
            .HasMany(x => x.Wishlist)
            .WithMany();

        builder.Entity<Group>()
            .HasMany(x => x.Users)
            .WithOne(x => x.Group);

        builder.Entity<Group>()
            .HasOne(x => x.Sensei)
            .WithMany();

        base.OnModelCreating(builder);
    }
   
}