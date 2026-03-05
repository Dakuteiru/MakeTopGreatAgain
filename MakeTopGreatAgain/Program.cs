using AutoMapper;
using MakeTopGreatAgain.Data;
using MakeTopGreatAgain.Database;
using MakeTopGreatAgain.Models.Lessons;
using MakeTopGreatAgain.Models.Users;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System.Linq.Expressions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddEndpointsApiExplorer();

var connectionString = builder.Configuration.GetConnectionString("Default");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseLazyLoadingProxies().UseSqlite(connectionString));//сюда много что мозно подключать

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
    ).ReverseMap();
}
    );

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<User>();
app.Run();
