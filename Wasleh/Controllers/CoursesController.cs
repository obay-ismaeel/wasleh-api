using Wasleh.Domain.Entities;
using Wasleh.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;

namespace Wasleh.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CoursesController : BaseController
{
    public CoursesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var courses = await _unitOfWork.Courses.GetAllAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id);
        if (course == null)
        {
            return NotFound();
        }
        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Post(Course course)
    {
        await _unitOfWork.Courses.AddAsync(course);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Course course)
    {
        if (id != course.Id)
        {
            return BadRequest();
        }

        var existingCourse = await _unitOfWork.Courses.GetByIdAsync(id);
        if (existingCourse == null)
        {
            return NotFound();
        }

        _mapper.Map(course, existingCourse);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id);
        if (course is null)
        {
            return NotFound();
        }

        _unitOfWork.Courses.Delete(course);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
