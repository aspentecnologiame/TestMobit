using TestMobit.Api.Models.DTO;
using TestMobit.Api.Models.Response.Base;

namespace TestMobit.Api.Models.Response
{
    public class EnterpriseResponse : BaseResponse<EnterpriseDto>
    {
        public EnterpriseResponse(EnterpriseDto data) : base(data)
        {
        }
    }
}
