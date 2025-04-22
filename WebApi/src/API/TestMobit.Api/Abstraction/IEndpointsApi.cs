namespace TestMobit.Api.Abstraction
{
    public interface IEndpointsApi
    {
        string GroupName { get; }
        void MapEndpoints(RouteGroupBuilder group);
    }
}
