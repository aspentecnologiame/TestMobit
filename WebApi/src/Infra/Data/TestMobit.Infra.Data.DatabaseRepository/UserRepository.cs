using TestMobit.Domain.Entities;
using TestMobit.Domain.Interfaces.Repositories.Database;
using TestMobit.Domain.Interfaces.Repositories.Database.Base;
using TestMobit.Infra.Data.DatabaseRepository.Base;

namespace TestMobit.Infra.Data.DatabaseRepository
{
    public class UserRepository : DataBaseRepository<UserEntity>, IUserRepository, IRepository
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<UserEntity> GetByUserName(string name) => await Task.FromResult(Find(x => x.Login == name).FirstOrDefault()) ?? new UserEntity();

        public async Task<UserEntity> Login(UserEntity userEntity) => await Task.FromResult(Find(x => x.Login == userEntity.Login && x.Password == userEntity.Password).FirstOrDefault()) ?? new UserEntity();
    }
}
