using System.Threading.Tasks;
using TestMobit.Domain.Entities;
using TestMobit.Domain.Interfaces.Repositories.Database.Base;

namespace TestMobit.Domain.Interfaces.Repositories.Database
{
    public interface IUserRepository : IRepository, IDatabaseRepository<UserEntity>
    {
        Task<UserEntity> GetByUserName(string name);
        Task<UserEntity> Login(UserEntity userEntity);
    }
}
