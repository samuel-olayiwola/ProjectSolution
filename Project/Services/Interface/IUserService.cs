using System;
using Project.Controllers.Resources.Responses;
using Project.Database.Models;

namespace Project.Services.Interface
{
    public interface IUserService
    {
        Task<UserResponse> GetUsers(string NoOfUsers);
        //Task<UserResponse> GetUser(int id);
        //other user service goes hereß
    }      
  
}

