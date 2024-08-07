using DongleSimulator.Domain.Entities;
using DongleSimulator.Domain.Repositories.User;
using DongleSimulator.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace DongleSimulator.Infra.Repositories;

public class UserRepository : IUserReadOnlyRepository, IUserWriteOnlyRepository
{
    private readonly DongleSimulatorDbContext _dbContext;

    public UserRepository(DongleSimulatorDbContext dbContext) => _dbContext = dbContext;
    
    public async Task<bool> ExistsUserWithUsername(string username)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Name.Equals(username));
    }

    public async Task<bool> ExistsUserWithEmail(string email)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .AnyAsync(u => u.Email.Equals(email));
    }

    public async Task<User?> GetUserByEmailAndPassword(string email, string password)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Email.Equals(email) && u.Password.Equals(password));
    }
    
    public async Task Create(User user) => await _dbContext.Users.AddAsync(user);
}