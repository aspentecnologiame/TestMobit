using System.Threading.Tasks;
using TestMobit.Domain.Entities;
using TestMobit.Domain.Interfaces.Services.Base;

namespace TestMobit.Domain.Interfaces.Services
{
    public interface IEnterpriseService : IService
    {
        Task Add(EnterpriseEntity entity);
        Task<EnterpriseEntity> GetByName(string name);
    }
}
