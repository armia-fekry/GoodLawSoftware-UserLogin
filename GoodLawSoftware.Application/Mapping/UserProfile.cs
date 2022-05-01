using AutoMapper;
using GoodLawSoftware.Application.Service.UserService.Dto;

namespace GoodLawSoftware.Application.Mapping
{
	public class UserProfile:Profile
	{
		public UserProfile()
		{
			CreateMap<User, UserDto>()
				.ForMember(dto => dto.Id, model => model.MapFrom(e => e.Id))
				.ForMember(dto => dto.FirstName, model => model.MapFrom(e => e.FirstName))
				.ForMember(dto => dto.LastName, model => model.MapFrom(e => e.LastName))
				.ForMember(dto => dto.Email, model => model.MapFrom(e => e.Email))
				.ForMember(dto => dto.UserName, model => model.MapFrom(e => e.UserName));
			
			CreateMap<UserDto,User>();
			CreateMap<UserUpdateDto, User>();
		}
	}
}
