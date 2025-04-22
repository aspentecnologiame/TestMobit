using System;
using System.Threading.Tasks;
using TestMobit.Domain.Entities;
using TestMobit.Domain.Interfaces.Repositories.Database;
using TestMobit.Domain.Interfaces.Services;

namespace TestMobit.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICryptographyService _cryptographyService;
        public UserService(IUserRepository userRepository, ICryptographyService cryptographyService)
        {
            _userRepository = userRepository;
            _cryptographyService = cryptographyService;
        }

        public async Task Add(UserEntity user)
        {
            if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password))
                throw new ArgumentNullException(nameof(user));

            try
            {
                user.Id = Guid.NewGuid();
                user.Password = await _cryptographyService.Encrypt(user.Password);
                var existingUser = await _userRepository.GetByUserName(user.Login);

                if (existingUser != null)
                    throw new ApplicationException("User already exists");

                await _userRepository.Add(user);
            }
            catch(Exception ex)
            {
                throw new Exception("Error while adding user", ex);
            }
        }

        public async Task<UserEntity> GetByUserName(string name) => await _userRepository.GetByUserName(name);

        public async Task<UserEntity> Login(UserEntity userEntity)
        {
            userEntity.Password = await _cryptographyService.Encrypt(userEntity.Password);
            return await _userRepository.Login(userEntity);
        }
    }
}
