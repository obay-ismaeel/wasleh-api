using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wasleh.Domain.Abstractions;
using Wasleh.Domain.Entities;
using Wasleh.Dtos.Generic;
using Wasleh.Dtos.Incoming;
using Wasleh.Dtos.Outgoing;

namespace Wasleh.Controllers;

public class QuestionsController : BaseController
{
    public QuestionsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
    {
        var content = (await _unitOfWork.Questions.Paginate(pageNumber, pageSize))
            .Select(x => _mapper.Map<ResponseQuestionDto>(x))
            .ToList();
        
        var result = new PageResult<ResponseQuestionDto>
        {
            Page = pageNumber,
            ResultsPerPage = pageSize,
            ResultCount = content.Count(),
            TotalCount = await _unitOfWork.Questions.CountAsync(),
            Content = content
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var item = await _unitOfWork.Questions.GetByIdAsync(id);

        if (item is null)
            return NotFound();

        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Post(RequestQuestionDto questionDto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(questionDto.UserId);

        if (user is null)
        {
            return BadRequest("No such user");
        }

        questionDto.Id = 0;
        var question = _mapper.Map<Question>(questionDto);
        await _unitOfWork.Questions.AddAsync(question);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(Get), new {id = question.Id}, _mapper.Map<ResponseQuestionDto>(question));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RequestQuestionDto questionDto)
    {
        if(id != questionDto.Id)
            return BadRequest("Ids don't match!");

        var question = _unitOfWork.Questions.GetById(id);

        if (question is null)
            return NotFound();

        question.Body = questionDto.Body;
        question.Title = questionDto.Title;

        _unitOfWork.Questions.Update(question);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _unitOfWork.Questions.GetByIdAsync(id);

        if (item is null)
            return NotFound();

        _unitOfWork.Questions.Delete(item);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }
}
