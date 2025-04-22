using System.Threading.Tasks;
using TestMobit.Domain.Interfaces.Services.Base;

namespace TestMobit.Domain.Interfaces.Services
{
    public interface ICryptographyService : IService
    {
        Task<string> Decrypt(string text);
        Task<string> Encrypt(string text);
    }
}
