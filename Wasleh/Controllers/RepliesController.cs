using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Wasleh.Domain.Abstractions;
using Wasleh.Domain.Entities;
using Wasleh.Dtos.Generic;
using Wasleh.Dtos.Incoming;
using Wasleh.Dtos.Outgoing;

namespace Wasleh.Controllers;

public class RepliesController : BaseController
{
    public RepliesController(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork, mapper)
    {
    }

    [HttpGet]
    public async Task<IActionResult> Get(int pageNumber = 1, int pageSize = 10)
    {
        var data = (await _unitOfWork.Replies.Paginate(pageNumber, pageSize))
            .Select(x => _mapper.Map<ResponseReplyDto>(x))
            .ToList();

        var result = new PageResult<ResponseReplyDto>
        {
            Page = pageNumber,
            ResultsPerPage = pageSize,
            ResultCount = data.Count(),
            TotalCount = await _unitOfWork.Replies.CountAsync(),
            Data = data
        };

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var reply = await _unitOfWork.Replies.GetByIdAsync(id);

        if (reply is null)
        {
            return NotFound();
        }

        var result = new Result<ResponseReplyDto> { Data = _mapper.Map<ResponseReplyDto>(reply) };

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Post(RequestReplyDto replyDto)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(replyDto.UserId);

        if (user is null)
        {
            return BadRequest("No such user");
        }

        var answer = await _unitOfWork.Answers.GetByIdAsync(replyDto.AnswerId);

        if (answer is null)
        {
            return BadRequest("No such answer");
        }

        replyDto.Id = 0;
        var reply = _mapper.Map<Reply>(replyDto);

        await _unitOfWork.Replies.AddAsync(reply);
        await _unitOfWork.CompleteAsync();

        return CreatedAtAction(nameof(Get), new { id = reply.Id }, _mapper.Map<ResponseReplyDto>(reply));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, RequestReplyDto replyDto)
    {
        if (id != replyDto.Id)
        {
            return BadRequest("Ids don't match!");
        }

        var reply = _unitOfWork.Replies.GetById(id);

        if (reply is null)
        {
            return NotFound();
        }

        var answer = await _unitOfWork.Answers.GetByIdAsync(replyDto.AnswerId);

        if(answer is null)
        {
            return BadRequest("No such answer");
        }

        replyDto.Id = reply.Id;
        _mapper.Map(replyDto, reply);

        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var reply = await _unitOfWork.Replies.GetByIdAsync(id);

        if (reply is null)
        {
            return NotFound();
        }

        _unitOfWork.Replies.Delete(reply);
        await _unitOfWork.CompleteAsync();

        return NoContent();
    }

    //[HttpPost("{id}/vote")]
    //public async Task<IActionResult> AddVote(int id, RequestVoteDto voteDto)
    //{
    //    var answer = await _unitOfWork.Answers.GetByIdAsync(id);

    //    if (answer is null)
    //        return NotFound();

    //    var user = await _unitOfWork.Users.GetByIdAsync(voteDto.UserId);

    //    if (user is null)
    //        return NotFound();

    //    var vote = new Vote
    //    {
    //        UserId = user.Id,
    //        Value = voteDto.VoteValue,
    //        EntityType = Domain.Enums.EntityType.Answer,
    //        EntityId = answer.Id
    //    };

    //    await _unitOfWork.Votes.AddAsync(vote);

    //    answer.TotalVotes += vote.Value;

    //    await _unitOfWork.CompleteAsync();

    //    var result = new Result<ResponseVoteDto>
    //    {
    //        Data = new ResponseVoteDto { VoteTotal = answer.TotalVotes },
    //    };

    //    return Ok(result);
    //}
}
