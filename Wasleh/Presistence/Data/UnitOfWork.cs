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
    public IBaseRepository<Vote> Votes { get; private set; }

    public UnitOfWork(AppDbContext context)
    {
        _context = context;
        Users = new BaseRepository<User>(context);
        Questions = new BaseRepository<Question>(context);
        Answers = new BaseRepository<Answer>(context);
        Votes = new BaseRepository<Vote>(context);
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