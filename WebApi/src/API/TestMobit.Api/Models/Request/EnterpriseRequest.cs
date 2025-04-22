using TestMobit.Api.Models.DTO;
using TestMobit.Api.Models.Request.Base;

namespace TestMobit.Api.Models.Request
{
    public class EnterpriseRequest : BaseRequest<EnterpriseDto>
    {
        public EnterpriseRequest(EnterpriseDto data) : base(data)
        {
        }
    }
}
