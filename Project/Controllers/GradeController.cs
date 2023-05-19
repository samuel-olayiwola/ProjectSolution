using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Project.Controllers.Resources.Requests;
using Project.Database.Models;
using Project.Database.Repositories.Interfaces;
using Project.Extentions;
using static Project.Controllers.Resources.Responses.UserResponse;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Project.Controllers
{
    [Route("api/GradeOperations")]
    [ApiController]
    public class GradeController : Controller
    {

        private readonly IGradeRepository _repository;
        public GradeController(IGradeRepository repository)
        {
            _repository = repository;
        }

        // GET: api/values
        [HttpGet("GetGrades")]
        public async Task<IActionResult> GetGrades()
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
        [HttpGet("GetGrade/{id}")]
        public async  Task<IActionResult> GetGrade(int id)
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

        [HttpGet("GetPagedGrades")]
        public async Task<IActionResult> GetPagedGrades([FromQuery] PagedRequest paging, [FromQuery] string sortColumn, [FromQuery] FilterRequest filter)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var sortquery = ProcessQuery<Grade>.Sort(sortColumn);
                var filterquery = ProcessQuery<Grade>.Filter(filter);
                var resp = _repository.GetPaged(paging.Page, paging.Limit, filterquery, sortquery);



                return Ok(resp);
            }

            catch (Exception e)
            {
                return UnprocessableEntity("An error occured");
            }
        }

        // POST api/values
        [HttpPost("CreateGrade")]
        public async Task<IActionResult> CreateGrade([FromBody] Grade obj)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.GetErrorMessages());
            try
            {
                var resp = _repository.Add(obj);
                return CreatedAtAction(nameof(GetGrade), new { id = obj.Id }, obj);
            }

            catch (Exception e)
            {
                return UnprocessableEntity("An error occured");
            }

        }

        // PUT api/values/5
        [HttpPut("UpdateGrade{id}")]
        public async Task<IActionResult> UpdateGrade(int id,[FromBody] Grade obj)
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
        [HttpDelete("DeleteGrade/{id}")]
        public async Task<IActionResult> DeleteGrade(int id)
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

