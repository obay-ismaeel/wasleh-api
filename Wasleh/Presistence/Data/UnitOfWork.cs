using Wasleh.Domain.Abstractions;
using Wasleh.Domain.Entities;
using Wasleh.Presistence.Repositories;

namespace Wasleh.Presistence.Data;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _context;
    public IBaseRepository<User> Users { get; private set; }
    public IBaseRepository<Question> Questions { get; private set; }
    public IBaseRepository<Answer> Answers { get; private set; }
    public IBaseRepository<Reply> Replies{ get; private set; }
    public IBaseRepository<Vote> Votes { get; private set; }
    public IBaseRepository<Lecture> Lectures { get; private set; }
    public IBaseRepository<Course> Courses { get; private set; }
    public IBaseRepository<Faculty> Faculties { get; private set; }
    public IBaseRepository<University> Universities{ get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Users = new BaseRepository<User>(context);
        Questions = new BaseRepository<Question>(context);
        Answers = new BaseRepository<Answer>(context);
        Votes = new BaseRepository<Vote>(context);
        Lectures = new BaseRepository<Lecture>(context);
        Courses = new BaseRepository<Course>(context);
        Faculties = new BaseRepository<Faculty>(context);
        Universities = new BaseRepository<University>(context);
        Replies = new BaseRepository<Reply>(context);
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}