using System;
using Project.Database.Models;

namespace Project.Database.Repositories.Interfaces
{
    public interface IGradeRepository: IProjectRepository<Grade>
    {
        //operations particular to Grade gets added here
    }
}

