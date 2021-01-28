using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBook.Api.Data;
using AddressBook.Api.Models;
using Microsoft.EntityFrameworkCore;
using Sieve.Models;
using Sieve.Services;

namespace AddressBook.Api.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task<User> DeleteUserAsync(int userId);
        Task<User> GetUserAsync(int userId);
        Task<User> GetUserByEmailAddressAsync(string emailAddress);
        Task<List<User>> ListUsersAsync(string filters, string sorts, string defaultSorts);
        Task<User> UpdateUserPasswordAsync(int userId, byte[] passwordHash, byte[] passwordSalt);
        Task<User> UpdateUserProfileAsync(int userId, UserProfileDto userProfileDto);
    }

    public class UserRepository : IUserRepository
    {
        private readonly AddressBookDbContext _dbContext;
        private readonly ISieveProcessor _sieveProcessor;

        public UserRepository(AddressBookDbContext dbContext, ISieveProcessor sieveProcessor)
        {
            _dbContext = dbContext;
            _sieveProcessor = sieveProcessor;
        }

        public async Task<User> AddUserAsync(User user)
        {
            var existingUser = await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.EmailAddress == user.EmailAddress);

            if (existingUser == null)
            {
                await _dbContext.AddAsync(user);
                await _dbContext.SaveChangesAsync();
                return user;
            }
            return null;
        }

        public async Task<User> DeleteUserAsync(int userId)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                _dbContext.Remove(user);
                await _dbContext.SaveChangesAsync();
            }
            return user;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.UserId == userId);
        }

        public async Task<User> GetUserByEmailAddressAsync(string emailAddress)
        {
            return await _dbContext.Users
                .AsNoTracking()
                .SingleOrDefaultAsync(u => u.EmailAddress == emailAddress);
        }

        public async Task<List<User>> ListUsersAsync(string filters, string sorts, string defaultSorts)
        {
            var sieveModel = new SieveModel
            {
                Filters = filters,
                Sorts = !string.IsNullOrWhiteSpace(sorts) ? sorts : defaultSorts
            };

            var users = _dbContext.Users.AsNoTracking();
            var usersResult = _sieveProcessor.Apply(sieveModel, users);
            return await usersResult.ToListAsync();
        }

        public async Task<User> UpdateUserPasswordAsync(int userId, byte[] passwordHash, byte[] passwordSalt)
        {
            var existingUser = await _dbContext.Users.FindAsync(userId);
            if (existingUser != null)
            {
                existingUser.PasswordHash = passwordHash;
                existingUser.PasswordSalt = passwordSalt;
                _dbContext.Update(existingUser);
                await _dbContext.SaveChangesAsync();
            }
            return existingUser;
        }

        public async Task<User> UpdateUserProfileAsync(int userId, UserProfileDto userProfileDto)
        {
            var user = await _dbContext.Users.FindAsync(userId);
            if (user != null)
            {
                user.EmailAddress = userProfileDto.EmailAddress;
                _dbContext.Update(user);
                await _dbContext.SaveChangesAsync();
            }
            return user;
        }
    }
}
