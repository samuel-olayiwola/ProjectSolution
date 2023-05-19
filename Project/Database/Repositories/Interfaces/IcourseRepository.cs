using System;
using System.Linq.Expressions;
using Project.Database.Models;

namespace Project.Database.Repositories.Interfaces
{
    public interface IcourseRepository : IProjectRepository<Course>
    {
        //operations particular to Course gets added here
        
    }
}

