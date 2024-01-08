using Authority.Models;
using Microsoft.AspNetCore.Authentication;

namespace Authority.Services
{
    public interface IAuthorizationService
    {
        AuthenticationToken? Authenticate(User user);
    }
}
