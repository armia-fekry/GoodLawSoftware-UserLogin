using GoodLawSoftware.Application.Service.UserService.Dto;

namespace GoodLawSoftware.Application.Service.UserService
{
	public interface IUserService
    {
        Task<UserDto> GetUserById(Guid id);
        Task<UserDto> GetUserByName(string name);
        Task<UserDto> UpdateUser(UserUpdateDto userDto);
        Task<bool> DeleteUser(Guid id);
        Task<UserDto> CreateUser(UserCreateDto userCreateDto);
        Task<List<UserDto>> GetAllUsers();
	}
}
