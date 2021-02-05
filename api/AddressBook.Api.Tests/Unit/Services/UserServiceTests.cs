using System.Threading.Tasks;
using AddressBook.Api.Helpers;
using AddressBook.Api.Models;
using AddressBook.Api.Repositories;
using AddressBook.Api.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace AddressBook.Api.Tests.Unit.Services
{
    public class UserServiceTests
    {
        private readonly int _userId = 1;
        private readonly string _userEmailAddress = "test@test.com";
        private readonly string _userPassword = "test@123";
        private readonly byte[] _userPasswordHash;
        private readonly byte[] _userPasswordSalt;

        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            HashUtilities.CreateHash(_userPassword, out _userPasswordHash, out _userPasswordSalt);

            _userRepositoryMock = new Mock<IUserRepository>();

            _userRepositoryMock.Setup(repo => repo.AddUserAsync(It.IsAny<User>()))
                .ReturnsAsync((User user) =>
                {
                    user.UserId = _userId;
                    return user;
                });

            _userRepositoryMock.Setup(repo => repo.UpdateUserPasswordAsync(It.IsAny<int>(), It.IsAny<byte[]>(), It.IsAny<byte[]>()))
                .ReturnsAsync((int userId, byte[] passwordHash, byte[] passwordSalt) =>
                {
                    var user = new User
                    {
                        UserId = userId,
                        PasswordHash = passwordHash,
                        PasswordSalt = passwordSalt
                    };
                    return user;
                });

            _userRepositoryMock.Setup(repo => repo.GetUserByEmailAddressAsync(It.IsAny<string>()))
                .ReturnsAsync((string emailAddress) =>
                {
                    if (emailAddress == _userEmailAddress)
                    {
                        var user = new User
                        {
                            EmailAddress = emailAddress,
                            PasswordHash = _userPasswordHash,
                            PasswordSalt = _userPasswordSalt
                        };
                        return user;
                    }
                    return null;
                });

            _userService = new UserService(_userRepositoryMock.Object);
        }

        [Fact]
        public async Task AddUserAsync_WithUserRegisterDto_CreatesUserAndCallsRepoAddUserAsync()
        {
            var userRegisterDto = new UserRegisterDto
            {
                EmailAddress = _userEmailAddress,
                Password = _userPassword,
                ConfirmPassword = _userPassword
            };

            var user = await _userService.AddUserAsync(userRegisterDto);
            var hashVerification = user != null && HashUtilities.VerifyHash(userRegisterDto.Password, user.PasswordHash, user.PasswordSalt);

            _userRepositoryMock.Verify(repo => repo.AddUserAsync(user), Times.Once);
            Assert.True(hashVerification);
        }

        [Fact]
        public async Task DeleteUserAsync_WithUserId_CallsRepoDeleteUserAsyncAndReturnsDeletedUser()
        {
            var user = await _userService.DeleteUserAsync(_userId);

            _userRepositoryMock.Verify(repo => repo.DeleteUserAsync(_userId), Times.Once);
            user.UserId.Should().Be(_userId);
        }

        [Fact]
        public async Task GetUserAsync_WithUserId_CallsRepoGetUserAsync()
        {
            await _userService.GetUserAsync(_userId);

            _userRepositoryMock.Verify(repo => repo.GetUserAsync(_userId), Times.Once);
        }

        [Fact]
        public async Task ListUsersAsync_WithFiltersAndSortsAndDefaultSorts_CallsRepoListUsersAsync()
        {
            var filters = $"emailAddress=={_userEmailAddress}";
            var sorts = "emailAddress";
            var defaultSorts = "emailAddress";

            await _userService.ListUsersAsync(filters, sorts);

            _userRepositoryMock.Verify(repo => repo.ListUsersAsync(filters, sorts, defaultSorts), Times.Once);
        }

        [Fact]
        public async Task UpdateUserPasswordAsync_WithUserIdAndUserPasswordDto_CallsRepoUpdateUserPasswordAsyncAndReturnsUpdatedUser()
        {
            var userPasswordDto = new UserPasswordDto
            {
                Password = _userPassword,
                ConfirmPassword = _userPassword
            };

            var user = await _userService.UpdateUserPasswordAsync(_userId, userPasswordDto);
            var hashVerification = user != null && HashUtilities.VerifyHash(userPasswordDto.Password, user.PasswordHash, user.PasswordSalt);

            _userRepositoryMock.Verify(repo => repo.UpdateUserPasswordAsync(_userId, user.PasswordHash, user.PasswordSalt));
            Assert.True(hashVerification);
        }

        [Fact]
        public async Task UpdateUserProfileAsync_WithUserIdAndUserProfileDto_CallsRepoUpdateUserProfileAsyncAndReturnsUpdatedUser()
        {
            var userProfileDto = new UserProfileDto
            {
                EmailAddress = _userEmailAddress
            };

            var user = await _userService.UpdateUserProfileAsync(_userId, userProfileDto);

            _userRepositoryMock.Verify(repo => repo.UpdateUserProfileAsync(_userId, userProfileDto), Times.Once);
            user.EmailAddress.Should().Be(_userEmailAddress);
        }

        [Fact]
        public async Task ValidateUserAsync_WithUserValidateDto_CallsRepoGetUserByEmailAddressAsyncAndReturnsMatchingUser()
        {
            var userValidateDto = new UserValidateDto
            {
                EmailAddress = _userEmailAddress,
                Password = _userPassword
            };

            var user = await _userService.ValidateUserAsync(userValidateDto);
            var hashVerification = user != null && HashUtilities.VerifyHash(_userPassword, user.PasswordHash, user.PasswordSalt);

            _userRepositoryMock.Verify(repo => repo.GetUserByEmailAddressAsync(userValidateDto.EmailAddress), Times.Once);
            user.EmailAddress.Should().Be(_userEmailAddress);
            Assert.True(hashVerification);
        }

        [Fact]
        public async Task ValidateUserAsync_WithUserValidateDto_ReturnsNull_IfEmailAddressDoesNotMatch()
        {
            var userValidateDto = new UserValidateDto
            {
                EmailAddress = "notfound@test.com",
                Password = _userPassword
            };

            var user = await _userService.ValidateUserAsync(userValidateDto);

            Assert.Null(user);
        }

        [Fact]
        public async Task ValidateUserAsync_WithUserValidateDto_ReturnsNull_IfPasswordDoesNotMatch()
        {
            var userValidateDto = new UserValidateDto
            {
                EmailAddress = _userEmailAddress,
                Password = "b@dp@ssw0rd"
            };

            var user = await _userService.ValidateUserAsync(userValidateDto);

            Assert.Null(user);
        }
    }
}
