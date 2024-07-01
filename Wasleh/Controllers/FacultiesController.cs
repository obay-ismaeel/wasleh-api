using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wasleh.Domain.Abstractions;
using Wasleh.Domain.Entities;
using Wasleh.Dtos.Generic;
using Wasleh.Dtos.Incoming;
using Wasleh.Dtos.Outgoing;

namespace Wasleh.Controllers;

public class FacultiesController : BaseController
{
    public FacultiesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber = 1, int pageSize = 10)
    {
        var data = (await _unitOfWork.Faculties.Paginate(pageNumber, pageSize))
            .Select(x => _mapper.Map<ResponseFacultyDto>(x))
            .ToList();

        var result = new PageResult<ResponseFacultyDto>
        {
            Data = data,
            Page = pageNumber,
            ResultsPerPage = pageSize,
            ResultCount = data.Count,
            TotalCount = await _unitOfWork.Faculties.CountAsync()
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var faculty = await _unitOfWork.Faculties.GetByIdAsync(id);
        
        if (faculty is null)
        {
            return NotFound();
        }

        var result = new Result<ResponseFacultyDto> 
        { 
            Data = _mapper.Map<ResponseFacultyDto>(faculty) 
        };
        
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(RequestFacultyDto facultyDto)
    {
        var university = await _unitOfWork.Universities.GetByIdAsync(facultyDto.UniversityId);
        if(university is null)
        {
            return NotFound("No such university");
        }

        facultyDto.Id = 0;
        var faculty = _mapper.Map<Faculty>(facultyDto);

        await _unitOfWork.Faculties.AddAsync(faculty);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(Get), new { id = faculty.Id }, _mapper.Map<ResponseFacultyDto>(faculty));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id,RequestFacultyDto facultyDto)
    {
        if (id != facultyDto.Id)
        {
            return BadRequest("Ids don't match");
        }


        var university = await _unitOfWork.Universities.GetByIdAsync(facultyDto.UniversityId);
        if (university is null)
        {
            return NotFound("No such university");
        }

        var faculty = await _unitOfWork.Faculties.GetByIdAsync(id);
        if (faculty is null)
        {
            return NotFound();
        }

        _mapper.Map(facultyDto, faculty);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var faculty = await _unitOfWork.Faculties.GetByIdAsync(id);
        if (faculty is null)
        {
            return NotFound();
        }

        _unitOfWork.Faculties.Delete(faculty);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}