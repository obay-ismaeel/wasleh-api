using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wasleh.Domain.Abstractions;
using Wasleh.Domain.Entities;
using Wasleh.Dtos.Generic;
using Wasleh.Dtos.Incoming;
using Wasleh.Dtos.Outgoing;

namespace Wasleh.Controllers;
public class UniversitiesController : BaseController
{
    private readonly IFileService _fileService;
    public UniversitiesController(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService) : base(unitOfWork, mapper)
    {
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(int pageNumber, int pageSize)
    {
        var data = (await _unitOfWork.Universities.Paginate(pageNumber, pageSize))
            .Select(x => _mapper.Map<ResponseUniversityDto>(x))
            .ToList();
        var result = new PageResult<ResponseUniversityDto>
        {
            Data = data,
            ResultCount = data.Count(),
            TotalCount = await _unitOfWork.Universities.CountAsync(),
            Page = pageNumber,
            ResultsPerPage = pageSize
        };
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var university = await _unitOfWork.Universities.GetByIdAsync(id);
        
        if (university is null)
        {
            return NotFound();
        }

        var result = new Result<ResponseUniversityDto> { Data = _mapper.Map<ResponseUniversityDto>(university) };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(RequestUniversityDto universityDto)
    {
        universityDto.Id = 0;
        var university = _mapper.Map<University>(universityDto);

        var path = await _fileService.StoreAsync(universityDto.LogoFile);
        if (path is not null)
            university.LogoPath = path;

        await _unitOfWork.Universities.AddAsync(university);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(Get), new { id = university.Id }, _mapper.Map<ResponseUniversityDto>(university));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RequestUniversityDto universityDto)
    {
        if (id != universityDto.Id)
        {
            return BadRequest("Ids don't match");
        }

        var university = await _unitOfWork.Universities.GetByIdAsync(id);
        if (university is null)
        {
            return NotFound("No such university");
        }

        if (universityDto.LogoFile is not null)
        {
            _fileService.Delete(university.LogoPath);
            var path = await _fileService.StoreAsync(universityDto.LogoFile);
            university.LogoPath = path;
        }

        _mapper.Map(_mapper.Map<University>(universityDto), university);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var university = await _unitOfWork.Universities.GetByIdAsync(id);
        
        if (university is null)
        {
            return NotFound();
        }

        _unitOfWork.Universities.Delete(university);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}