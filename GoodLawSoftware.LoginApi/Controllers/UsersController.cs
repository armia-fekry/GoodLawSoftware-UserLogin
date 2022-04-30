using GoodLawSoftware.Application.Shared;
using JWT_NET_5.Application.Service.UserService;
using JWT_NET_5.Application.Service.UserService.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace JWT_NET_5.Controllers
{
	[Route("api")]
	[ApiController]
	public class UsersController : ControllerBase
	{
		private readonly IUserService _userService;
		private readonly IHttpContextAccessor _httpContext;

		public UsersController(IUserService userService, IHttpContextAccessor httpContext)
		{
			_userService = userService;
			_httpContext = httpContext;
		}
		
		// GET: api/Users
		[Authorize]
		[HttpGet("Users")]
		public async Task<List<UserDto>> Get()
		{
			return await _userService.GetAllUsers();
		}

		// GET api/users/5
		[Authorize]
		[HttpGet("Users/{id}")]
		public async Task<UserDto> Get(Guid id)
		{
			return await _userService.GetUserById(id); 
		}

		// POST api/users
		[HttpPost("User")]
		public async Task<UserDto> Post([FromBody] UserCreateDto userCreateDto)
		{
			return await _userService.CreateUser(userCreateDto);
		}

		// PUT api/users/5
		[Authorize]
		[HttpPut("User/{id}")]
		public async Task<ActionResult<UserDto>> Put(Guid id, [FromBody] UserUpdateDto updateDto)
		{
			if (ModelState.IsValid)
				return BadRequest("Comlete requierd Fields ");
			AssertionConcern.AssertionAgainstNotNull(id, "Invalid User Id");
			updateDto.Id = id;
			return Ok(await _userService.UpdateUser(updateDto));
		}

		// DELETE api/users/5
		[Authorize]
		[HttpDelete("User/{id}")]
		public async Task<ActionResult<bool>> Delete(Guid id)
		{
			if(id == default(Guid)) return BadRequest("Invalid ID ");
			return Ok(await _userService.DeleteUser(id));
		}
		
	}
}
