using System;
using Newtonsoft.Json;
using Project.Controllers.Resources.Responses;
using Project.Database.Models;
using Project.Database.Repositories.Interfaces;
using Project.Network.Interface;
using Project.Services.Interface;

namespace Project.Services.Implementation
{
    public class UserService:IUserService
    {
        private readonly IApiClient _apiClient;
        private string endpoint;
        private readonly ILogger<UserService> _logger;
        private readonly IApiClient _client;
        public UserService(IApiClient apiClient, ILogger<UserService> logger, IConfiguration configuration, IApiClient apiClient1)
        {
            _apiClient = apiClient;
            _logger = logger;
            _client = apiClient;
            endpoint = configuration["RandomApi:Url"];
        }

        public async Task<UserResponse> GetUsers(string NoOfUsers = "2")
        {
            try
            {
                endpoint += "?results=" + NoOfUsers;
                var response = await _client.JsonGetDataAsync(endpoint);
                var users = JsonConvert.DeserializeObject<UserResponse>(response);
                LogActivity("Responnse recieved and desrialized succesfully");
                return users;
            }

            catch(Exception e)
            {
                return null;
            }



        }

        private void LogActivity(string activity)
        {
            _logger.LogInformation("{OperationType} operation performed at {DateTime}", activity, DateTime.UtcNow);
        }

        // --- new change, in the form of a comment ---

        //RandomApi does not allow getting by ID


        //public async Task<UserResponse> GetUser(int id)
        //{
        //    try
        //    {
        //        endpoint += "?results=" ;
        //        var response = await _client.JsonGetDataAsync(endpoint);
        //        var user = JsonConvert.DeserializeObject<UserResponse>(response);
        //        return user;
        //    }

        //    catch (Exception e)
        //    {
        //        return null;
        //    }

        //}
    }
}

