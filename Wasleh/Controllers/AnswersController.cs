using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wasleh.Domain.Abstractions;
using Wasleh.Domain.Entities;
using Wasleh.Dtos.Generic;
using Wasleh.Dtos.Incoming;
using Wasleh.Dtos.Outgoing;

namespace Wasleh.Controllers;

public class AnswersController : BaseController
{
    public AnswersController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
    {
        var content = (await _unitOfWork.Answers.Paginate(pageNumber, pageSize, ["User"]))
            .Select(x => _mapper.Map<ResponseAnswerDto>(x))
            .ToList();

        var result = new PageResult<ResponseAnswerDto>
        {
            Page = pageNumber,
            ResultsPerPage = pageSize,
            ResultCount = content.Count(),
            TotalCount = await _unitOfWork.Answers.CountAsync(),
            Data = content
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var answer = await _unitOfWork.Answers.GetByIdAsync(id);

        if (answer is null)
            return NotFound();

        var result = new Result<ResponseAnswerDto> { Data = _mapper.Map<ResponseAnswerDto>(answer) };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(RequestAnswerDto answerDto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(answerDto.UserId);

        if (user is null)
        {
            return BadRequest("No such user");
        }

        var question = await _unitOfWork.Questions.GetByIdAsync(answerDto.QuestionId);

        if (question is null)
        {
            return BadRequest("No such question");
        }

        answerDto.Id = 0;
        var answer = _mapper.Map<Answer>(answerDto);
        await _unitOfWork.Answers.AddAsync(answer);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(Get), new { id = answer.Id }, _mapper.Map<ResponseAnswerDto>(answer));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RequestAnswerDto answerDto)
    {
        if (id != answerDto.Id)
            return BadRequest("Ids don't match!");

        var answer = _unitOfWork.Answers.GetById(id);

        if (answer is null)
            return NotFound();

        answer.Body = answerDto.Body;
        answer.UpdatedAt = DateTime.UtcNow;

        _unitOfWork.Answers.Update(answer);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var item = await _unitOfWork.Answers.GetByIdAsync(id);

        if (item is null)
            return NotFound();

        _unitOfWork.Answers.Delete(item);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpPost("{id}/vote")]
    public async Task<IActionResult> AddVote(int id, RequestVoteDto voteDto)
    {
        var answer = await _unitOfWork.Answers.GetByIdAsync(id);

        if (answer is null)
            return NotFound();

        var user = await _unitOfWork.Users.GetByIdAsync(voteDto.UserId);

        if (user is null)
            return NotFound();

        var vote = new Vote
        {
            UserId = user.Id,
            Value = voteDto.VoteValue,
            EntityType = Domain.Enums.EntityType.Answer,
            EntityId = answer.Id
        };

        await _unitOfWork.Votes.AddAsync(vote);

        answer.TotalVotes += vote.Value;

        await _unitOfWork.CompleteAsync();

        var result = new Result<ResponseVoteDto>
        {
            Data = new ResponseVoteDto { VoteTotal = answer.TotalVotes },
        };

        return Ok(result);
    }
}
