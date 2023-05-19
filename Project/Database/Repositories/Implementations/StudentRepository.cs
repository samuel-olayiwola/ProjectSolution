using System;
using Project.Database.DbContexts;
using Project.Database.Models;
using Project.Database.Repositories.Interfaces;

namespace Project.Database.Repositories.Implementations
{
    public class StudentRepository:ProjectRepository<Student>,IStudentRepository
    {
        public StudentRepository(AppDbcontext dbcontext, ILogger<StudentRepository> logger):base(dbcontext, logger)
        {
        }
    }
}

