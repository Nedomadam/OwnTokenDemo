using Authority.Data;
using Authority.Services;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authority.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private ApplicationDbContext _context;
        private readonly Logger<AuthenticationController> _logger;
        private IAuthorizationService _auth;
    }
}
