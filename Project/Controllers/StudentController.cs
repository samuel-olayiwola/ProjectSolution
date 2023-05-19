using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Controllers.Resources.Requests;
using Project.Database.Models;
using Project.Database.Repositories.Interfaces;
using Project.Extentions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/StudentOperations")]
    [ApiController]
    public class StudentController : Controller
    {

        private readonly IStudentRepository _repository;
        public StudentController(IStudentRepository repository)
        {
            _repository = repository;
        }


        // GET: api/values
        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var resp = _repository.GetAll();
                return Ok(resp);
            }

            catch (Exception e)
            {
                return UnprocessableEntity("An error occured");
            }
        }

        // GET api/values/5
        [HttpGet("GetStudent/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {
            if (!ModelState.IsValid)
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
        [HttpPost("CreateStudent")]
        public async Task<IActionResult> CreateStudent([FromBody] Student obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var resp = _repository.Add(obj);
                return CreatedAtAction(nameof(GetStudent), new { id = obj.Id }, obj);
            }

            catch (Exception e)
            {
                return UnprocessableEntity("An error occured");
            }

        }

        [HttpGet("GetPagedStudents")]
        public async Task<IActionResult> GetPagedStudents([FromQuery] PagedRequest paging, [FromQuery] string sortColumn, [FromQuery] FilterRequest filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var sortquery = ProcessQuery<Student>.Sort(sortColumn);
                var filterquery = ProcessQuery<Student>.Filter(filter);
                var resp = _repository.GetPaged(paging.Page, paging.Limit, filterquery, sortquery);



                return Ok(resp);
            }

            catch (Exception e)
            {
                return UnprocessableEntity("An error occured");
            }
        }

        // PUT api/values/5
        [HttpPut("UpdateStudent{id}")]
        public async Task<IActionResult> UpdateStudent(int id, [FromBody] Student obj)
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
        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
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

