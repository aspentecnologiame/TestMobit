using AutoMapper;
using TestMobit.Domain.Entities.Base;
using TestMobit.Domain.Entities;
using TestMobit.Api.Models.DTO;

namespace TestMobit.Api.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMappingRequest();
            CreateMappingResponse();
        }

        private void CreateMappingRequest()
        {
            CreateMap<UserRequestDto, UserEntity>();
            CreateMap<EnterpriseDto, EnterpriseEntity>();
        }

        private void CreateMappingResponse()
        {
            CreateMap<UserEntity, UserRequestDto>();
            CreateMap<EnterpriseEntity, EnterpriseDto>();
        }
    }
}
