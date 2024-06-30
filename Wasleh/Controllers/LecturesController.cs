using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wasleh.Domain.Abstractions;
using Wasleh.Domain.Entities;
using Wasleh.Dtos.Generic;
using Wasleh.Dtos.Incoming;
using Wasleh.Dtos.Outgoing;

namespace Wasleh.Controllers;

public class LecturesController : BaseController
{
    private readonly IFileService _fileService;
    public LecturesController(IUnitOfWork unitOfWork, IMapper mapper, IFileService fileService) : base(unitOfWork, mapper)
    {
        _fileService = fileService;
    }

    [HttpGet]
    public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
    {
        var lectures = (await _unitOfWork.Lectures.Paginate(pageNumber, pageSize, ["User"]))
            .Select(x => _mapper.Map<ResponseLectureDto>(x))
            .ToList();

        var result = new PageResult<ResponseLectureDto>
        {
            Data = lectures,
            TotalCount = await _unitOfWork.Lectures.CountAsync(),
            ResultCount = lectures.Count,
            ResultsPerPage = pageSize,
            Page = pageNumber,
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var lecture = await _unitOfWork.Lectures.GetByIdAsync(id);

        if(lecture is null)
            return NotFound();

        var result = new Result<ResponseLectureDto> { Data = _mapper.Map<ResponseLectureDto>(lecture) };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(RequestLectureDto lectureDto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(lectureDto.UserId);

        if(user is null)
            return NotFound("No such user");

        var course = await _unitOfWork.Courses.GetByIdAsync(lectureDto.CourseId);

        if (course is null)
            return NotFound("No such course");

        lectureDto.Id = 0;
        var lecture = _mapper.Map<Lecture>(lectureDto);

        var path = await _fileService.StoreAsync(lectureDto.File);

        if(path is not null)
        {
            lecture.FilePath = path;
        }

        await _unitOfWork.Lectures.AddAsync(lecture);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(Get), new { id = lecture.Id }, _mapper.Map<ResponseLectureDto>(lecture));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RequestLectureDto lectureDto)
    {
        if (id != lectureDto.Id)
            return BadRequest("Ids don't match!");

        var lecture = await _unitOfWork.Lectures.GetByIdAsync(id);

        if (lecture is null)
            return NotFound();

        var course = await _unitOfWork.Courses.GetByIdAsync(lectureDto.CourseId);

        if (course is null)
            return NotFound("No such course");

        lecture.Title = lectureDto.Title;
        lecture.Description = lectureDto.Description;
        lecture.Provider = lectureDto.Provider;
        lecture.CourseId = lectureDto.CourseId;
        lecture.UpdatedAt = DateTime.UtcNow;

        if(lectureDto.File is not null)
        {
            _fileService.Delete(lecture.FilePath);
            var path = await _fileService.StoreAsync(lectureDto.File);
            lecture.FilePath = path;
        }

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        var lecture = await _unitOfWork.Lectures.GetByIdAsync(id);

        if (lecture is null)
            return NotFound();

        _unitOfWork.Lectures.Delete(lecture);
        await _unitOfWork.CompleteAsync();
        _fileService.Delete(lecture.FilePath);

        return NoContent();
    }
}
