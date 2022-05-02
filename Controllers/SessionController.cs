using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using GroceryListApi.DTOs;
using GroceryListApi.Repositories.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace GroceryListApi.Controllers
{
    [Authorize]
    public class SessionController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly SigningCredentials _creds;

        public SessionController(IUserRepository userRepository, SigningCredentials creds)
        {
            _userRepository = userRepository;
            _creds = creds;
        }

        [HttpPost("/session")]
        [AllowAnonymous]
        public async Task<IActionResult> PostAsync([FromBody] SessionDto request)
        {
            var header = new JwtHeader(_creds);

            try
            {
                var user = _userRepository.GetByUsername(request.Username);

                if (user == null)
                {
                    return NotFound();
                }

                if (!BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash)) // verify password with hashed password
                {
                    return new UnauthorizedResult();
                }

                var data = new
                {
                    username = user.Username,
                    id = user.Id
                };
                var payload = new JwtPayload() {
                    { "data", data },
                    { "exp", EpochTime.GetIntDate(DateTime.Now.AddHours(2)) }, // expire token in 2 hours
                    { "sub", user.Id }
                };

                var token = new JwtSecurityToken(header, payload);
                var signedToken = new JwtSecurityTokenHandler().WriteToken(token);

                return Ok(new
                {
                    token = signedToken
                });
            }
            catch (InvalidOperationException)
            {
                return new UnauthorizedResult();
            }
        }
    }
}