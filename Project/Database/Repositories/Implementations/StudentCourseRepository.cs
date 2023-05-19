using System;
using Project.Database.DbContexts;
using Project.Database.Models;
using Project.Database.Repositories.Interfaces;

namespace Project.Database.Repositories.Implementations
{
    public class StudentCourseRepository: ProjectRepository<StudentCourse> ,IStudentCourseRepository
    {
        public StudentCourseRepository(AppDbcontext dbcontext, ILogger<StudentCourseRepository> logger) : base(dbcontext,logger)
        {
        }
    }
}

