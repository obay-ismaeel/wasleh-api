using Wasleh.Domain.Entities;

namespace Wasleh.Domain.Abstractions;

public interface IUnitOfWork
{
    IBaseRepository<User> Users { get; }
    IBaseRepository<Question> Questions { get; }
    IBaseRepository<Answer> Answers { get; }
    IBaseRepository<Vote> Votes { get; }
    IBaseRepository<Lecture> Lectures { get; }
    IBaseRepository<Course> Courses{ get; }
    IBaseRepository<Faculty> Faculties{ get; }
    IBaseRepository<University> Universities{ get; }
    Task<int> CompleteAsync();
    void Dispose();
}