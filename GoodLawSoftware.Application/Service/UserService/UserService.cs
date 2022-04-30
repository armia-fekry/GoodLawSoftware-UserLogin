﻿using AutoMapper;
using GoodLawSoftware.Application;
using GoodLawSoftware.Application.IRepositoies;
using GoodLawSoftware.Application.Shared;
using JWT_NET_5.Application.Service.UserService.Dto;

namespace JWT_NET_5.Application.Service.UserService
{
	public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		public UserService(IUnitOfWork unitOfWork, IMapper mapper)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
		}


		public async Task<UserDto> CreateUser(UserCreateDto userCreateDto)
		{
			var user = User.Create(Guid.NewGuid(),
				userCreateDto.UserName,
				userCreateDto.Password
				);
			 await _unitOfWork.UserRepository.AddAsync(user);
			_unitOfWork.Complete();
			return _mapper.Map<UserDto>(user);
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
			var res = await _unitOfWork.UserRepository.FindAllAsync(e=>e.Id !=default(Guid),new string[]{"Products" });
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
