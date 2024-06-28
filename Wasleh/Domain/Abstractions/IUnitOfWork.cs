using Wasleh.Domain.Entities;

namespace Wasleh.Domain.Abstractions;

public interface IUnitOfWork
{
    IBaseRepository<User> Users { get; }
    IBaseRepository<Question> Questions { get; }
    IBaseRepository<Answer> Answers { get; }
    IBaseRepository<Vote> Votes { get; }
    Task<int> CompleteAsync();
    void Dispose();
}