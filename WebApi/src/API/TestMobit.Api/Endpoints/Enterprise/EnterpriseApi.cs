using AutoMapper;
using TestMobit.Api.Abstraction;
using TestMobit.Api.Models.Request;
using TestMobit.Domain.Entities;
using TestMobit.Domain.Interfaces.Services;

namespace TestMobit.Api.Endpoints.Enterprise
{
    public class EnterpriseApi : IEndpointsApi
    {
        public string GroupName { get => "Enterprise"; }
        public void MapEndpoints(RouteGroupBuilder group)
        {
            group.MapPost("/", async (IMapper _mapper, IEnterpriseService _enterpriseService, EnterpriseRequest enterpriseRequest) =>
            {
                var enterpriseEntity = _mapper.Map<EnterpriseEntity>(enterpriseRequest.Data);
                await _enterpriseService.Add(enterpriseEntity);
                return Results.Ok(enterpriseRequest);
            });
        }
    }
}
