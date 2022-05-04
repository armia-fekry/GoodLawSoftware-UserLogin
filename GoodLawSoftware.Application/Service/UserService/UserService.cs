using AutoMapper;
using GoodLawSoftware.Application;
using GoodLawSoftware.Application.IRepositoies;
using GoodLawSoftware.Application.Shared;
using GoodLawSoftware.Application.Service.UserService.Dto;
using Microsoft.Extensions.Logging;

namespace GoodLawSoftware.Application.Service.UserService
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly ILogger<UserService> _logger;

		public UserService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<UserService> logger)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_logger = logger;
		}


		public async Task<UserDto> CreateUser(UserCreateDto userCreateDto)
		{
			try
			{
				var user = User.Create(Guid.NewGuid(),
					userCreateDto.UserName,
					userCreateDto.Password,
					userCreateDto.FisrtName,
					userCreateDto.LastName,
					userCreateDto.Email
					);
				var founded =  await _unitOfWork.UserRepository
					.FindAsync(e=>e.UserName==user.UserName);
				if (founded is not null)
				{
					_logger.LogInformation($"there is allready username {userCreateDto.UserName} before ");
					return null;
				}

				var res = await _unitOfWork.UserRepository.AddAsync(user);
				_unitOfWork.Complete();
				return _mapper.Map<UserDto>(res);
			}
			catch (Exception ex)
			{
				_logger.LogError($"Error Message {ex.Message}");
				return null;
			}
		}

		public async Task<bool> DeleteUser(Guid id)
		{
			AssertionConcern.AssertionAgainstNotNull(id, "Invalid User Id ");
			var user = await _unitOfWork.UserRepository.FindAsync(e => e.Id == id);
			AssertionConcern.AssertionAgainstNotNull(user, $"Not Found User With ID {id}");
			_unitOfWork.UserRepository.Delete(user);
			_unitOfWork.Complete();
			return true;
		}


	public async Task<List<UserDto>> GetAllUsers()
		{
			var res = await _unitOfWork.UserRepository.FindAllAsync(e=>e.Id !=default(Guid));
			if(res is not null && res.ToList().Count >0)
				return _mapper.Map<List<UserDto>>(res.ToList());
			return null;
		}

		public async Task<UserDto> GetUserById(Guid id)
		{
			AssertionConcern.AssertionAgainstNotNull(id, "Invalid User Id ");
			var user = await _unitOfWork.UserRepository.FindAsync(e => e.Id == id);
			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> GetUserByName(string name)
		{
			AssertionConcern.AssertionAgainstNotNullOrEmplty(name, "Invalid User Name ");
			var user = await _unitOfWork.UserRepository.FindAsync(e => e.UserName == name);
			return _mapper.Map<UserDto>(user);
		}

		public async Task<UserDto> UpdateUser(UserUpdateDto userDto)
		{
			AssertionConcern.AssertionAgainstNotNull(userDto, "Invalid request");
			var oldUser=await _unitOfWork.UserRepository.FindAsync(e=>e.Id==userDto.Id);
			AssertionConcern
				.AssertionAgainstNotNull(oldUser, $"User With ID {userDto.Id} , Not Found");
			var user = _mapper.Map<User>(userDto);
			var res= _unitOfWork.UserRepository.Update(user);
			_unitOfWork.Complete();
			return _mapper.Map<UserDto>(user);
		}
	}
}
