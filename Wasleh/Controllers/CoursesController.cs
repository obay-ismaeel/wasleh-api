using Wasleh.Domain.Entities;
using Wasleh.Domain.Abstractions;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Wasleh.Dtos.Generic;
using Wasleh.Dtos.Outgoing;
using Wasleh.Dtos.Incoming;

namespace Wasleh.Controllers;

public class CoursesController : BaseController
{
    public CoursesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var data = (await _unitOfWork.Courses.Paginate(pageNumber, pageSize))
            .Select(x => _mapper.Map<ResponseCourseDto>(x))
            .ToList();

        var result = new PageResult<ResponseCourseDto>
        {
            Data = data,
            Page = pageNumber,
            ResultsPerPage = pageSize,
            ResultCount = data.Count,
            TotalCount = await _unitOfWork.Courses.CountAsync()
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var course = await _unitOfWork.Courses.GetByIdAsync(id);

        if (course == null)
        {
            return NotFound();
        }

        var result = new Result<ResponseCourseDto> 
        { 
            Data = _mapper.Map<ResponseCourseDto>(course) 
        };

        return Ok(course);
    }

    [HttpPost]
    public async Task<IActionResult> Post(RequestCourseDto courseDto)
    {
        var faculty = await _unitOfWork.Faculties.GetByIdAsync(courseDto.FacultyId);
        if(faculty is null)
        {
            return BadRequest("No such faculty");
        }

        courseDto.Id = 0;
        var course = _mapper.Map<Course>(courseDto);
        
        await _unitOfWork.Courses.AddAsync(course);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(Get), new { id = course.Id }, _mapper.Map<ResponseCourseDto>(course) );
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RequestCourseDto courseDto)
    {
        if (id != courseDto.Id)
        {
            return BadRequest("Ids don't match");
        }

        var faculty = await _unitOfWork.Faculties.GetByIdAsync(courseDto.FacultyId);
        if(faculty is null)
        {
            return BadRequest("No such faculty");
        }

        var course = await _unitOfWork.Courses.GetByIdAsync(id);
        if (course is null)
        {
            return NotFound();
        }

        _mapper.Map(courseDto, course);
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
