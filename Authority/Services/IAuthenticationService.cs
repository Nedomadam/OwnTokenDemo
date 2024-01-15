using Authority.Models;
using Microsoft.AspNetCore.Authentication;

namespace Authority.Services
{
    public interface IAuthenticationService
    {
        AuthenticationToken? Authenticate(User user);
    }
}
