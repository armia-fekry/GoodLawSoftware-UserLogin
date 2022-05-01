using GoodLawSoftware.LoginApi.AuthModels;
using GoodLawSoftware.LoginApi.Data.AuthModels;
using GoodLawSoftware.Service;
using Microsoft.AspNetCore.Mvc;

namespace GoodLawSoftware.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AuthController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AuthController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("Register")]
		public async Task<ActionResult> Register([FromBody] UserRegisterModel registerModel)
		{
			if(!ModelState.IsValid)
				return BadRequest("Please, Complete Requiered Fields Before Submit!!");
			var result = await _authService.RegisterAsync(registerModel);
			if(!result.IsAuthenticated) return BadRequest(result.Message);
			return Ok(result);
		}
		[HttpPost("LogIn")]
		public async Task<ActionResult> LogIn([FromBody] LoginModel loginModel)
		{
			if (!ModelState.IsValid)
				return BadRequest("Please, Complete Requiered Fields Before Submit!!");
			var result = await _authService.LoginAsync(loginModel);
			if (!result.IsAuthenticated) return BadRequest(result.Message);
			return Ok(result);
		}
		[HttpPost("AssignRole")]
		public async Task<ActionResult> AssignRoleToUser([FromBody] UserRoleModel userRoleModel)
		{
			if (!ModelState.IsValid)
				return BadRequest("Incompleted Data, invalid user id or role");
			var result = await _authService.AddRoleToUserAsync(userRoleModel);
			return string.IsNullOrEmpty(result)? Ok():BadRequest(result);
		}
	}
}
