using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Project.Controllers.Resources.Requests;
using Project.Database.Models;
using Project.Database.Repositories.Interfaces;
using Project.Extentions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/CourseOperations")]
    [ApiController]
    public class CourseController : Controller
    {

        private readonly IcourseRepository _repository;
        public CourseController(IcourseRepository repository)
        {
            _repository = repository;
        }
        // GET: api/values
        [HttpGet("GetCourses")]
        public async Task<IActionResult> GetCourses()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var resp = _repository.GetAll();
                return Ok(resp);
            }

            catch(Exception e)
            {
                return UnprocessableEntity("An error occured");
            }
        }

        [HttpGet("GetPagedCourses")]
        public async Task<IActionResult> GetPagedCourses([FromQuery] PagedRequest paging, [FromQuery] string sortColumn, [FromQuery] FilterRequest filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var sortquery = ProcessQuery<Course>.Sort(sortColumn);
                var filterquery = ProcessQuery<Course>.Filter(filter);
                var resp = _repository.GetPaged(paging.Page, paging.Limit, filterquery, sortquery);



                return Ok(resp);
            }

            catch (Exception e)
            {
                return UnprocessableEntity("An error occured");
            }
        }

        // GET api/values/5
        [HttpPost("GetCourse/{id}")]
        public async Task<IActionResult> GetCourse(int id)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var resp = await _repository.GetById(id);
                if (resp == null)
                    return NotFound("Resource not found");

                return Ok(resp);
            }

            catch (Exception e)
            {
                return UnprocessableEntity("An error occured");
            }
        }

        // POST api/values
        [HttpPost("CreateCourse")]
        public async Task<IActionResult> CreateCourse([FromBody] Course obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var resp = _repository.Add(obj);
                return CreatedAtAction(nameof(GetCourse), new { id = obj.Id }, obj);
            }

            catch (Exception e)
            {
                return UnprocessableEntity("An error occured");
            }
        }

        // PUT api/values/5
        [HttpPut("UpdateCourse/{id}")]
        public async Task<IActionResult> UpdateCourse(int id, [FromBody] Course obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {

                var resp = await _repository.GetById(id);
                if (resp == null)
                    return NotFound("Resource Not Found");

                
                await _repository.Update(obj);
                return NoContent();
            }

            catch (Exception e)
            {
                return UnprocessableEntity("An error occured");
            }
        }

        // DELETE api/values/5
        [HttpDelete("DeleteCourse/{id}")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var resp = await _repository.GetById(id);
                if (resp == null)
                    return NotFound("Resource not found");

                await _repository.Delete(resp);
                return NoContent();
            }

            catch (Exception e)
            {
                return UnprocessableEntity("An error occured");
            }
        }
    }
}

