using System;
using Project.Database.Models;

namespace Project.Database.Repositories.Interfaces
{
    public interface IStudentRepository:IProjectRepository<Student>
    {
        //operations particular to students gets added here
    }
}

