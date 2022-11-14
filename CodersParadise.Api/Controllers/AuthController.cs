using CodersParadise.Api.ApiModels;
using CodersParadise.Core.Interfaces.Logic;
using Microsoft.AspNetCore.Http;
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
                    Email = request.Email,
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
    }
}
