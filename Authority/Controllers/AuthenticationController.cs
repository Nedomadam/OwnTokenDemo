using Authority.Data;
using Authority.Models;
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
        private readonly ILogger<AuthenticationController> _logger;
        private IAuthenticationService _auth;

        public AuthenticationController(
            ApplicationDbContext context, 
            ILogger<AuthenticationController> logger, 
            IAuthenticationService auth)
        {
            _context = context;
            _logger = logger;
            _auth = auth;
        }

        [HttpPost]
        [Route("authenticate")]
        public IActionResult Authenticate(LoginIM credentials)
        {
            var token = _auth.Authenticate(new Models.User { 
                Username = credentials.Username, 
                Password = credentials.Password 
            });

            if (token == null)
            {
                return Unauthorized();
            }
            return Ok(token);
        }
    }
}
