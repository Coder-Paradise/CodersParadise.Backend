using CodersParadise.Api.ApiModels;
using CodersParadise.Core.Interfaces.Logic;
using Microsoft.AspNetCore.Mvc;
using CodersParadise.Core.DTO;

namespace CodersParadise.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthLogic _authLogic;

        public AuthController(IAuthLogic authLogic)
        {
            _authLogic = authLogic;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(ApiModels.UserRegisterRequest request)
        {
            try
            {
                var userRegisterRequest = new Core.DTO.UserRegisterRequest()
                {
                    Email = request.Email + "@gmail.com",
                    Password = request.Password,
                    ConfirmPassword = request.ConfirmPassword
                };

                var result = await _authLogic.Register(userRegisterRequest);

                if (!result)
                    return BadRequest("Error Registering User!");
                
                return Ok("User Successfully Created!");    
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(ApiModels.UserLoginRequest request)
        {
            try
            {
                var userLoginRequest = new Core.DTO.UserLoginRequest()
                {
                    Email = request.Email + "@gmail.com",
                    Password = request.Password
                };

                var result = await _authLogic.Login(userLoginRequest);
                var roles = new int[] { 2001, 1984, 5150 };
                return Ok(new LoginResponse() { AccessToken = result.AccessToken, RefreshToken = result.RefreshToken, TokenExpiry = result.AccessTokenExpiry, Roles = roles  });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("refresh")]
        public async Task<IActionResult> RefreshToken([FromBody] ApiModels.RefreshTokenRequest refreshRequest)
        {
            try
            {
                var refreshTokenRequest = new Core.DTO.RefreshTokenRequest()
                {
                    ExpiredAccessToken = refreshRequest.ExpiredAccessToken,
                    RefreshToken = refreshRequest.RefreshToken
                };

                var result = await _authLogic.RefreshToken(refreshTokenRequest);
                return Ok(new LoginResponse() { AccessToken = result.AccessToken, TokenExpiry = result.AccessTokenExpiry, RefreshToken = result.RefreshToken });
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("verify")]
        public async Task<IActionResult> Verify(string token)
        {
            try
            {
                await _authLogic.Verify(token);
                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                await _authLogic.ForgotPassword(email);
                return Ok("You may now reset your password.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                var resetPasswordRequest = new ResetPasswordRequestDTO()
                {
                    Token = request.Token,
                    Password = request.Password,
                    ConfirmPassword = request.ConfirmPassword
                };

                await _authLogic.ResetPassword(resetPasswordRequest);
                return Ok("Password successfully reset.");
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
