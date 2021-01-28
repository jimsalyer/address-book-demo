using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using AddressBook.Api.Helpers;
using AddressBook.Api.Models;
using AddressBook.Api.Services;

namespace AddressBook.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/v1/users")]
    public class UsersController : ControllerBase
    {
        private readonly AppSettings _appSettings;
        private readonly IUserService _userService;

        public UsersController(IOptions<AppSettings> appSettings, IUserService userService)
        {
            _appSettings = appSettings.Value;
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public async Task<ActionResult<AuthToken>> AuthenticateUserAsync(UserValidateDto userValidateDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.ValidateUserAsync(userValidateDto);
                if (user != null)
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, user.UserId.ToString())
                        }),
                        Expires = DateTime.UtcNow.AddDays(7),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    var tokenString = tokenHandler.WriteToken(token);

                    var userTokenDto = new AuthToken
                    {
                        AccessToken = tokenString
                    };
                    return userTokenDto;
                }
                return BadRequest(new { message = "Your email address or password is incorrect." });
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> DeleteUserAsync(int id)
        {
            var user = await _userService.DeleteUserAsync(id);
            if (user != null)
            {
                return user;
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<User>> GetUserAsync(int id)
        {
            var user = await _userService.GetUserAsync(id);
            if (user != null)
            {
                return user;
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> ListUsersAsync(string filters, string sorts)
        {
            return await _userService.ListUsersAsync(filters, sorts);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> RegisterUserAsync(UserRegisterDto userRegisterDto)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.AddUserAsync(userRegisterDto);
                if (user != null)
                {
                    return Ok();
                }
                return BadRequest(new { message = $"A user with the email address \"{user.EmailAddress}\" already exists." });
            }
            return BadRequest();
        }
    }
}
