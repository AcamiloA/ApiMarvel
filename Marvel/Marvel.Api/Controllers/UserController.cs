using Marvel.Application.DTO;
using Marvel.Application.Security;
using Marvel.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Marvel.Api.Controllers
{
    /// <summary>
    /// Controlador para tratamiento de información del usuario
    /// </summary>
    /// <param name="userService"></param>
    /// <param name="jwtSettings"></param>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [AllowAnonymous]
    public class UserController(IUserService userService, IOptions<JwtSecuritySettings> jwtSettings) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly JwtSecuritySettings _jwtSettings = jwtSettings.Value;

        /// <summary>
        ///     Endpoint de prueba para validar si el api esta funcionando correctamente.
        /// </summary>
        /// <param name="echo"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        public ActionResult<string> Echo(string echo)
        {
            return Ok($"Ejecución correcta: {echo}");
        }

        /// <summary>
        /// Método para registrarse por primera vez
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserDTO user)
        {
            try
            {
                var registeredUser = await _userService.RegisterAsync(user);
                return Ok(registeredUser);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Método para registrarse por primera vez
        /// </summary>
        /// <param name="nickname">Alias escogido por el usuario para llamarse dentro del aplicativo</param>
        /// <param name="email">Email al cual quiere recibir notificaciones y para el acceso</param>
        /// <param name="password">Password del usuario</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<IOAuth2Response>> Login(string? nickname, string? email, string password)
        {
            try
            {
                TokenDTO? token = await _userService.Login(nickname, email, password);
                if (token.IsSucceeded)
                {
                    token.Token = BuildToken(token);
                    token.ExpiresIn = 3600;
                    token.NickName = nickname ?? "";
                    token.Email = email ?? "";

                    return Ok(token.Result);
                }
                return BadRequest(token.Result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Construye el token de autenticación
        /// </summary>
        /// <param name="user">Usuario</param>
        /// <returns></returns>
        private string BuildToken(TokenDTO user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Secret!);
            user.ExpiresIn = _jwtSettings.ExpireTimeHours * 60 * 60;
            user.TokenType = "Bearer";

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(
                [
                    new(System.Security.Claims.ClaimTypes.Name, user.NickName ?? user.Email)
                ]),
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpireTimeHours),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }

    }
}
