using GoodLawSoftware.Application;
using GoodLawSoftware.LoginApi.AuthModels;
using GoodLawSoftware.LoginApi.Data.AuthModels;
using GoodLawSoftware.LoginApi.Helper;
using GoodLawSoftware.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GoodLawSoftware.LoginApi.Service
{
	public class AuthService : IAuthService
	{
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole<Guid>> _roleManager;
		private readonly JWT _jwt;

		public AuthService(UserManager<User> userManager,IOptions<JWT> jwt,RoleManager<IdentityRole<Guid>> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
			_jwt = jwt.Value;
		}

		public async Task<string> AddRoleToUserAsync(UserRoleModel userRoleModel)
		{
			var user = await _userManager.FindByIdAsync(userRoleModel.UserId);
			if (user == null || !await _roleManager.RoleExistsAsync(userRoleModel.Role))
				return "Invalid User ID Or Role Name";
			if (await _userManager.IsInRoleAsync(user, userRoleModel.Role))
				return $"This User ID {user.Id} Already Assigned To This Role {userRoleModel.Role}";

			var result = await _userManager.AddToRoleAsync(user, userRoleModel.Role);
			return result.Succeeded ? String.Empty : "Error During Assigning Role To User";
		}

		public async Task<AuthModel> LoginAsync(LoginModel loginModel)
		{
			var authModel = new AuthModel();
			var user = await _userManager.FindByNameAsync(loginModel.UserName);
			if (user == null || !await _userManager.CheckPasswordAsync(user, loginModel.Password))
			{
				authModel.Message = "Invalid username Or Password!! ";
				return authModel;
			}
			var jwtSecurityToken = await CreateJwtToken(user);
			var roles = await _userManager.GetRolesAsync(user);
			authModel.UserName = user.UserName;
			authModel.Email = user.Email;
			authModel.IsAuthenticated = true;
			authModel.Roles = roles.ToList();
			authModel.UserId=user.Id;
			authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
			authModel.ExpireOn = jwtSecurityToken.ValidTo;
			
			return authModel;
		}

		public async Task<AuthModel> RegisterAsync(UserRegisterModel registerModel)
		{
			if(await _userManager.FindByNameAsync(registerModel.UserName) is not null)
				return new AuthModel() { Message="This Name Is Used Before,Try Another Name !!"};
			var user = new User()
			{
				Id=Guid.NewGuid(),
				UserName = registerModel.UserName,
				Password=registerModel.Password,
				FirstName=registerModel.FirstName,
				LastName=registerModel.LastName,
				Email=registerModel.Email
			};
			var result = await _userManager.CreateAsync(user,registerModel.Password);
			if (!result.Succeeded)
				return new AuthModel()
				{
					Message = string.Join
					(',', result.Errors.Select(e => e.Description).ToArray())
				};
			await _userManager.AddToRoleAsync(user, "User");
			var jwtSecurityToken =await CreateJwtToken(user);
			return new AuthModel()
			{
				UserName = user.UserName,
				UserId=user.Id,
				IsAuthenticated = true,
				Roles = new List<string>{registerModel.Role },
				Token=new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
				ExpireOn=jwtSecurityToken.ValidTo
			};
		}

		private async Task<JwtSecurityToken> CreateJwtToken(User user)
		{
			var userClaims = await _userManager.GetClaimsAsync(user);
			var roles = await _userManager.GetRolesAsync(user);
			var roleClaims = new List<Claim>();

			foreach (var role in roles)
				roleClaims.Add(new Claim("roles", role));

			var claims = new[]
			{
				new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
				new Claim(JwtRegisteredClaimNames.Email, user.Email.ToString()),
				new Claim("uid",user.Id.ToString())
			}
			.Union(userClaims)
			.Union(roleClaims);

			var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
			var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

			var jwtSecurityToken = new JwtSecurityToken(
				issuer: _jwt.Issuer,
				audience: _jwt.Audiance,
				claims: claims,
				expires: DateTime.Now.AddHours(_jwt.TimeOutInHours),
				signingCredentials: signingCredentials);

			return jwtSecurityToken;
		}
	}
}
