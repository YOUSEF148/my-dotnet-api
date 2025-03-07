using APIS2.DTOS;
using AutoMapper;
using core.DTOS.Core.DTOS;
using Core.Entites;

namespace APIS2.Helpers
{

	public class JobProfile : Profile
	{
		public JobProfile()
		{
			CreateMap<Job, JobDto>().ReverseMap();
			CreateMap<User, UserDto>().ReverseMap();
		}
		
	}
}
