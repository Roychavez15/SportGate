using SportGate.API.Models;

namespace SportGate.API.Services
{
    public interface IAuthService
    {
        string GenerateToken(User user);
    }
}

