using TestMobit.Api.Models.DTO;
using TestMobit.Api.Models.Response.Base;

namespace TestMobit.Api.Models.Response
{
    public class UserResponse : BaseResponse<UserResponseDto>
    {
        public UserResponse(UserResponseDto user) : base(user)
        {
        }
    }
}
