using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Controllers.Resources.Responses;
using Project.Database.Models;
using Project.Extentions;
using Project.Services.Interface;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/UserOperations")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService service)
        {
            _userService = service;
        }


        // GET: api/values
        [HttpGet("GetUsers")]
        public async Task<IActionResult> GetUsers(string numberOfUsers)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var response = await _userService.GetUsers(numberOfUsers);

                return Ok(response);
            }

            catch(Exception e)
            {
                return UnprocessableEntity("Error occured at RandomApi Server");
            }
            

        }

        // GET api/values/5
        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetUser(int id)
        //{
        //    try
        //    {
        //        var response = await _userService.GetUser(id);

        //        return Ok(response);
        //    }

        //    catch (Exception e)
        //    {
        //        return UnprocessableEntity("Error occured at RandomApi Server");
        //    }
        //}

       
    }
}

