using System.ComponentModel.DataAnnotations;

namespace Wasleh.Dtos.Incoming;

public record RequestVoteDto
{
    public int UserId { get; set; }
    [AllowedValues([-1,1])]
    public int VoteValue { get; set; }
}
