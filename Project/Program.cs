using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Project.Database.DbContexts;
using Project.Database.Repositories.Implementations;
using Project.Database.Repositories.Interfaces;
using Project.Network.Impementation;
using Project.Network.Interface;
using Project.Services.Implementation;
using Project.Services.Interface;

namespace Project;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Project", Version = "v1" });
        });

        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
        builder.Services.AddScoped<IStudentRepository,StudentRepository>();
        builder.Services.AddScoped<IGradeRepository, GradeRepository>();
        builder.Services.AddScoped<IcourseRepository, CourseRepository>();
        builder.Services.AddScoped<IApiClient, ApiClient>();
        builder.Services.AddHttpClient();
        builder.Services.AddDbContext<AppDbcontext>(options =>
        {
            options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection"));
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Project v1"));

        }

        app.UseRouting();

        app.UseEndpoints(Endpoint =>
        {
            Endpoint.MapControllers();
        });
        app.Run();
    }
      
}

