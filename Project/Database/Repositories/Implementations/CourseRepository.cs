using System;
using Project.Database.DbContexts;
using Project.Database.Models;
using Project.Database.Repositories.Interfaces;

namespace Project.Database.Repositories.Implementations
{
    public class CourseRepository:ProjectRepository<Course>,IcourseRepository
    {
        public CourseRepository(AppDbcontext dbcontext, ILogger<CourseRepository> logger) : base(dbcontext, logger)
        {
        }
    }
}

