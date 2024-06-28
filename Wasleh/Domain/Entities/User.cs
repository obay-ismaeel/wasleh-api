using Microsoft.AspNetCore.Identity;

namespace Wasleh.Domain.Entities;

public class User : IdentityUser<int>
{
    public ICollection<Question> Questions { get; set; } = [];
    public ICollection<Answer> Answers { get; set;} = [];
    public ICollection<Vote> Votes { get; set; } = [];
}
