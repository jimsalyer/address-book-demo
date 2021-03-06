using System.Collections.Generic;
using System.Threading.Tasks;
using AddressBook.Api.Helpers;
using AddressBook.Api.Models;
using AddressBook.Api.Repositories;

namespace AddressBook.Api.Services
{
    public interface IUserService
    {
        Task<User> AddUserAsync(UserRegisterDto userRegisterDto);
        Task<User> DeleteUserAsync(int userId);
        Task<User> GetUserAsync(int userId);
        Task<List<User>> ListUsersAsync(string filters, string sorts);
        Task<User> UpdateUserProfileAsync(int userId, UserProfileDto userProfileDto);
        Task<User> UpdateUserPasswordAsync(int userId, UserPasswordDto userPasswordDto);
        Task<User> ValidateUserAsync(UserValidateDto userValidateDto);
    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> AddUserAsync(UserRegisterDto userRegisterDto)
        {
            HashUtilities.CreateHash(userRegisterDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new User
            {
                EmailAddress = userRegisterDto.EmailAddress,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };
            return await _userRepository.AddUserAsync(user);
        }

        public async Task<User> DeleteUserAsync(int userId)
        {
            return await _userRepository.DeleteUserAsync(userId);
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await _userRepository.GetUserAsync(userId);
        }

        public async Task<List<User>> ListUsersAsync(string filters, string sorts)
        {
            return await _userRepository.ListUsersAsync(filters, sorts, "emailAddress");
        }

        public async Task<User> UpdateUserPasswordAsync(int userId, UserPasswordDto userPasswordDto)
        {
            HashUtilities.CreateHash(userPasswordDto.Password, out byte[] passwordHash, out byte[] passwordSalt);
            return await _userRepository.UpdateUserPasswordAsync(userId, passwordHash, passwordSalt);
        }

        public async Task<User> UpdateUserProfileAsync(int userId, UserProfileDto userProfileDto)
        {
            return await _userRepository.UpdateUserProfileAsync(userId, userProfileDto);
        }

        public async Task<User> ValidateUserAsync(UserValidateDto userValidateDto)
        {
            var user = await _userRepository.GetUserByEmailAddressAsync(userValidateDto.EmailAddress);
            if (user != null && HashUtilities.VerifyHash(userValidateDto.Password, user.PasswordHash, user.PasswordSalt))
            {
                return user;
            }
            return null;
        }
    }
}
