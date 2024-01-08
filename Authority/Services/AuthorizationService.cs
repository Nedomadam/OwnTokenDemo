using Authority.Data;
using Authority.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Authority.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        private ApplicationDbContext _context;
        private AuthenticationOptions _options;
        private ILogger<AuthorizationService> _logger;

        public AuthorizationService(
            ApplicationDbContext context, 
            IOptions<AuthenticationOptions> options, 
            ILogger<AuthorizationService> logger)
        {
            _context = context;
            _options = options.Value;
            _logger = logger;
        }

        public AuthenticationToken? Authenticate(User user)
        {
            var u = _context.Users.FirstOrDefault(x => x.Username == user.Username && x.Password == user.Password);
            if (u == null)
            {
                return null;
            }
            return CreateAuthenticationToken(u);
        }

        private AuthenticationToken CreateAuthenticationToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.UTF8.GetBytes(_options.Key);
            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, user.Username));
            claims.Add(new Claim(ClaimTypes.Email, user.Email));
            claims.Add(new Claim("sub", user.UserId.ToString()));
            claims.Add(new Claim("admin", user.Admin.ToString()));
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                IssuedAt = DateTime.UtcNow,
                Issuer = _options.Issuer,
                Audience = _options.Audience,
                Expires = DateTime.UtcNow.AddMinutes(_options.Expiration),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey), 
                    SecurityAlgorithms.HmacSha256Signature
                )
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new AuthenticationToken { 
                Name = "authentication_token",
                Value = tokenHandler.WriteToken(token)
            };
        }
    }

    public class AuthenticationOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public int Expiration { get; set; }
    }
}
