using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wasleh.Domain.Abstractions;
using Wasleh.Domain.Entities;
using Wasleh.Dtos;

namespace Wasleh.Controllers;

public class QuestionsController : BaseController
{
    public QuestionsController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _unitOfWork.Questions.GetAllAsync());
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
    public async Task<IActionResult> Post(QuestionRequestDto questionDto)
    {
        var user = await _unitOfWork.Questions.GetByIdAsync(questionDto.UserId);

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
    public async Task<IActionResult> Put(int id, QuestionRequestDto questionDto)
    {
        if(id != questionDto.Id)
            return BadRequest("Ids don't match!");

        var user = _unitOfWork.Questions.GetByIdAsync(questionDto.UserId);

        if (user is null)
            return BadRequest("No such user");

        var item = _unitOfWork.Questions.GetById(id);

        if (item is null)
            return NotFound();

        var questionToSave = _mapper.Map<Question>(questionDto);

        _unitOfWork.Questions.Update(questionToSave);
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
