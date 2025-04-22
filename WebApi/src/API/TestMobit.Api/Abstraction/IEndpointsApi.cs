namespace TestMobit.Api.Abstraction
{
    public interface IEndpointsApi
    {
        void MapEndpoints(IApplicationBuilder builder, IEndpointRouteBuilder endpoint);
    }
}
