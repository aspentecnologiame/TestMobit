using TestMobit.Domain.Entities;
using TestMobit.Domain.Interfaces.Repositories.Database.Base;

namespace TestMobit.Domain.Interfaces.Repositories.Database
{
    public interface IEnterpriseRepository : IRepository, IDatabaseRepository<EnterpriseEntity>
    {
    }
}
