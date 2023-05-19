using System;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Project.Database.Models;

namespace Project.Database.DbContexts
{
    public class AppDbcontext : DbContext
    {

        public DbSet<Course> Courses { get; set; }
        public DbSet<Grade> Grades { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<StudentCourse> StudentCourses { get; set; }


        public AppDbcontext(DbContextOptions<AppDbcontext> options) : base(options)
        {

        }
    }
}

