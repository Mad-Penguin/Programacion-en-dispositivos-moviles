using Listing.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Listing.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly IConfiguration configuration;
        private readonly SignInManager<IdentityUser> signInManager;

        public AccountController(UserManager<IdentityUser> userManager, IConfiguration configuration, SignInManager<IdentityUser> signInManager)
        {
            this.userManager = userManager;
            this.configuration = configuration;
            this.signInManager = signInManager;
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthResponse>> Login(UserCredential userCredential)
        {
            var result = await signInManager.PasswordSignInAsync(userCredential.UserName, userCredential.Password, isPersistent: false, lockoutOnFailure: false);
            if (!result.Succeeded) return BadRequest("Login incorrecto");
            return GenerarToken(userCredential);
        }

        [HttpPost("registrar")]
        public async Task<ActionResult<AuthResponse>> Registrar(UserCredential userCredential)
        {
            var user = new IdentityUser { UserName = userCredential.UserName, Email = userCredential.Email };
            var result = await userManager.CreateAsync(user, userCredential.Password);

            if (!result.Succeeded) return BadRequest(result.Errors); // Para fines didacticos
            if (!result.Succeeded) return BadRequest(); // Forma correcta

            return GenerarToken(userCredential);
        }

        private AuthResponse GenerarToken(UserCredential userCredential)
        {
            var claims = new List<Claim> {
                new Claim( "usuario" , userCredential.UserName ),
                new Claim( "email", userCredential.Email )
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["jwtkey"]));
            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.Now.AddDays(1);
            var security_token = new JwtSecurityToken(issuer: null, claims: claims, expires: expiration, signingCredentials: credential);

            return new AuthResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(security_token),
                Expiration = expiration,
            };
        }
    }
}
