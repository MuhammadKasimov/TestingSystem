using System.Threading.Tasks;
using TestingSystem.Service.DTOs.Users;

namespace TestingSystem.Service.Interfaces.Users
{
    public interface IAuthService
    {
        ValueTask<string> GenerateToken(string username, string password);
    }
}
