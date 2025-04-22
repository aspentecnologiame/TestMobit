using TestMobit.Api.Models.DTO;
using TestMobit.Api.Models.Request.Base;

namespace TestMobit.Api.Models.Request
{
    public class UserRequest : BaseRequest<UserRequestDto>
    {
        public UserRequest(UserRequestDto data) : base(data)
        {
        }
    }
}
