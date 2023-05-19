using System;
using Project.Database.DbContexts;
using Project.Database.Models;
using Project.Database.Repositories.Interfaces;

namespace Project.Database.Repositories.Implementations
{
    public class GradeRepository:ProjectRepository<Grade>,IGradeRepository
    {
        public GradeRepository(AppDbcontext dbcontext, ILogger<GradeRepository> logger) : base(dbcontext, logger)
        {
        }
    }
}

