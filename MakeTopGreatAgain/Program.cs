using AutoMapper;
using MakeTopGreatAgain.Data;
using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Middleware.Restrict;
using MakeTopGreatAgain.Models.Lessons;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;
using static MakeTopGreatAgain.Controllers.LessonController;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies().UseSqlite(connectionString));

builder.Services.AddIdentityApiEndpoints<User>().AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();



builder.Services.AddAutoMapper(mapper =>
{
    mapper.CreateMap<Group, GroupDate>();
    mapper.CreateMap<Group, GroupCreateRequest>()
                    .ForMember(
                        data => data.Title,
                        expression => expression.MapFrom(group=>group.Name))
                    .ForMember(
                        data => data.TeacherId,
                        expression => expression.MapFrom(group => group.Sensei.Id))
                    .ForMember(
                        data => data.startsAt,
                        expression => expression.MapFrom(group => group.StartedAt)).ReverseMap(); 
    mapper.CreateMap<User, UserData>().ReverseMap();
    mapper.CreateMap<Lesson, LessonBase>()
    .ForMember(
        data => data.SubjectID,
        expression => expression.MapFrom(subject=>subject.Subject.Id)
    )
    .ForMember(
        data => data.TeacherId,
        expression => expression.MapFrom(subject=>subject.Teacher.Id)
    )
    .ForMember(
        data => data.TeacherId,
        expression => expression.MapFrom(subject=>subject.Teacher.Id)
    );;
    mapper.CreateMap<HomeworkCompletion, HwCOutput>()
        .ForMember(data => data.Homework,
            expression => expression.MapFrom(hw=>hw.Homework))
        .ForMember(data => data.Name,
            expression => expression.MapFrom(hw=>hw.Student.Name))
        .ForMember(data => data.Surname,
            expression => expression.MapFrom(hw=>hw.Student.Surname))
        .ForMember(data => data.score,
            expression => expression.MapFrom(hw=>hw.Score))
        .ReverseMap();

        
    mapper.CreateMap<GroupStudents, GroupSt>()
        .ForMember(data=>data.Group,
        expression=>expression.MapFrom(user=>user.Group))
        .ForMember(data=>data.Name,
            expression=>expression.MapFrom(obj=>obj.Student.Name))
        .ForMember(data=>data.Surname,
            expression=>expression.MapFrom(obj=>obj.Student.Surname))
        .ForMember(data=>data.BirthDate,
            expression=>expression.MapFrom(obj=>obj.Student.BirthDate))//LessonGCR
        .ForMember(data=>data.Wishlist,
            expression=>expression.MapFrom(obj=>obj.Student.Wishlist))
        .ForMember(data => data.Id,
        expression=>expression.MapFrom(obj => obj.Student.Id))
        .ReverseMap();
    mapper.CreateMap<Lesson, LessonGCR>()
     .ForMember(data => data.Group,
        expression => expression.MapFrom(obj => obj.Group))
     .ForMember(data => data.LessonId, expression => expression.MapFrom(obj => obj.Id))
     .ForMember(data => data.TeacherName, expression => expression.MapFrom(obj => obj.Teacher.Name))
     .ForMember(data => data.TeacherSurname, expression => expression.MapFrom(obj => obj.Teacher.Surname))
     .ForMember(data => data.Subject, expression => expression.MapFrom(obj => obj.Subject))
     .ReverseMap();
}
    );

var app = builder.Build();

app.UseCors(policy => policy
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<RestrictMiddleware>();
app.MapControllers();
app.MapIdentityApi<User>();
app.Run();
