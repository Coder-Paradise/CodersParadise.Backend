using CodersParadise.Api.ApiModels;
using CodersParadise.Core.DTO;
using CodersParadise.Core.Interfaces.Logic;
using Microsoft.AspNetCore.Mvc;
using System.Data;

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
                    Username = request.Username,
                    Password = request.Password,
                    ConfirmPassword = request.ConfirmPassword,
                    Email = request.Email
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
                    Username = request.Username,
                    Password = request.Password
                };

                var result = await _authLogic.Login(userLoginRequest);
                var roles = new int[] { 2001, 1984, 5150 };

                SetTokensInsideCookie(result.AccessToken, result.RefreshToken, HttpContext);

                //return Ok(new LoginResponse() { AccessToken = result.AccessToken, RefreshToken = result.RefreshToken, TokenExpiry = result.AccessTokenExpiry, Roles = roles  });
                return Ok(new LoginResponse() { AccessToken = result.AccessToken, Roles = roles });
            }
            catch(UnauthorizedAccessException e)
            {
                return Unauthorized(e.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                HttpContext.Request.Cookies.TryGetValue("accessToken", out var accessToken);
                HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken);

                if (!string.IsNullOrEmpty(accessToken))
                {
                    Response.Cookies.Delete("accessToken");
                }

                if (!string.IsNullOrEmpty(refreshToken))
                {
                    Response.Cookies.Delete("refreshToken");
                    await _authLogic.DeleteRefreshToken(refreshToken);
                }

                return Ok();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet]
        [Route("refresh")]
        //public async Task<IActionResult> RefreshToken([FromBody] ApiModels.RefreshTokenRequest refreshRequest)
        public async Task<IActionResult> RefreshToken()
        {
            try
            {
                HttpContext.Request.Cookies.TryGetValue("accessToken", out var accessToken);
                HttpContext.Request.Cookies.TryGetValue("refreshToken", out var refreshToken);

                var refreshTokenRequest = new Core.DTO.RefreshTokenRequest()
                {
                    ExpiredAccessToken = accessToken ?? string.Empty,
                    RefreshToken = refreshToken ?? string.Empty
                };

                var result = await _authLogic.RefreshToken(refreshTokenRequest);
                var roles = new int[] { 2001, 1984, 5150 };
                SetTokensInsideCookie(result.AccessToken, result.RefreshToken, HttpContext);

                return Ok(new LoginResponse() { AccessToken = result.AccessToken, Roles = roles });
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

        [ApiExplorerSettings(IgnoreApi = true)]
        public void SetTokensInsideCookie(string accessToken, string refreshToken, HttpContext context)
        {
            context.Response.Cookies.Append("accessToken", accessToken,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddMinutes(10),
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });

            context.Response.Cookies.Append("refreshToken", refreshToken,
                new CookieOptions
                {
                    Expires = DateTimeOffset.UtcNow.AddMinutes(131400),
                    HttpOnly = true,
                    IsEssential = true,
                    Secure = true,
                    SameSite = SameSiteMode.None
                });     
        }
    }
}
