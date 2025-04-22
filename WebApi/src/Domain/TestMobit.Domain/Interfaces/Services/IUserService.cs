using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestMobit.Domain.Entities;
using TestMobit.Domain.Interfaces.Services.Base;

namespace TestMobit.Domain.Interfaces.Services
{
    public interface IUserService : IService
    {
        Task Add(UserEntity user);
        Task<UserEntity> GetByUserName(string name);
        Task<UserEntity> Login(UserEntity userEntity);
    }
}
