using Microsoft.AspNetCore.Mvc;
using Project.Controllers.Resources.Requests;
using Project.Database.Models;
using Project.Database.Repositories.Interfaces;
using Project.Extentions;
using static Project.Controllers.Resources.Responses.UserResponse;

namespace Project.Controllers;

[ApiController]
[Route("api/StudentCourseOperations")]
public class StudentCourseController : Controller
{
    private readonly IStudentCourseRepository _repository;
    public StudentCourseController(IStudentCourseRepository repository)
    {
        _repository = repository;
    }

    // GET: api/values
    [HttpGet("GetStudentCourses")]
    public async Task<IActionResult> GetStudentCourses()
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        try
        {
            var resp =  _repository.GetAll();
            return Ok(resp);
        }

        catch (Exception e)
        {
            return UnprocessableEntity("An error occured");
        }
    }

    // GET api/values/5
    [HttpGet("GetStudentCourse/{id}")]
    public async Task<IActionResult> GetStudentCourse(int id)
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
    [HttpPost("CreateStudentCourse")]
    public async Task<IActionResult> CreateStudentCourse([FromBody] StudentCourse obj)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        try
        {
            var resp = _repository.Add(obj);
            return CreatedAtAction(nameof(GetStudentCourse), new { id = obj.Id }, obj);
        }

        catch (Exception e)
        {
            return UnprocessableEntity("An error occured");
        }

    }

    [HttpPut("UpdateStudentCourse/{id}")]
    public async Task<IActionResult> UpdateStudentCourse(int id, [FromBody] StudentCourse obj)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        try
        {
            var resp = await _repository.GetById(id);
            if ( resp == null )
                return NotFound("Resource Not Found");

            resp.CourseId = obj.CourseId;
            resp.StudentId = obj.StudentId;
            resp.GradeId = obj.GradeId;

            await _repository.Update(resp);
            return NoContent(); 
        }

        catch (Exception e)
        {
            return UnprocessableEntity("An error occured");
        }
    }

    [HttpGet("GetPagedStudentCourses")]
    public async Task<IActionResult> GetPagedStudentCourses([FromQuery] PagedRequest paging, [FromQuery] string sortColumn, [FromQuery] FilterRequest filter)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        try
        {
            var sortquery = ProcessQuery<StudentCourse>.Sort(sortColumn);
            var filterquery = ProcessQuery<StudentCourse>.Filter(filter);
            var resp = _repository.GetPaged(paging.Page, paging.Limit, filterquery, sortquery);



            return Ok(resp);
        }

        catch (Exception e)
        {
            return UnprocessableEntity("An error occured");
        }
    }

    // DELETE api/values/5
    [HttpDelete("DeleteStudentCourse/{id}")]
    public async Task<IActionResult> DeleteStudentCourse(int id)
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

