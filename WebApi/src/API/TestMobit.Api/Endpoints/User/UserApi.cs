using AutoMapper;
using TestMobit.Api.Abstraction;
using TestMobit.Api.Models.DTO;
using TestMobit.Api.Models.Request;
using TestMobit.Api.Models.Response;
using TestMobit.Api.Security;
using TestMobit.Domain.Entities;
using TestMobit.Domain.Interfaces.Services;

namespace TestMobit.Api.Endpoints.User
{
    public class UserApi : IEndpointsApi
    {
        public string GroupName { get => "User";}

        public void MapEndpoints(RouteGroupBuilder group)
        {
            group.MapPost("SignUp", async (UserRequest userRequest, IMapper _mapper, IUserService _userService) =>
            {
                var userEntity = _mapper.Map<UserEntity>(userRequest.Data);
                await _userService.Add(userEntity);
                return Results.Ok(userRequest);
            });

            group.MapPost("SignIn", async (UserRequest userRequest, IMapper _mapper, IUserService _userService, JwtBearerToken jwtBearerToken) =>
            {
                var userEntity = _mapper.Map<UserEntity>(userRequest.Data);
                var result = await _userService.Login(userEntity);

                var errorResult = new UserResponse(new UserResponseDto());
                errorResult.Message.Add("Invalid username or password");

                var response = result == null ? Results.Ok(errorResult)
                    : Results.Ok(new UserResponse(new UserResponseDto { Token = jwtBearerToken.GenerateToken(result) }));

                return response;
            });
        }
    }
}
