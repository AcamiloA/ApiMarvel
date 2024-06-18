using Marvel.Application.DTO;
using Marvel.Application.Services;
using Marvel.Domain.Entities;
using Marvel.Infrastruture.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.Api.Controllers
{
    /// <summary>
    /// Controlador para tratamiento de información del usuario
    /// </summary>
    /// <param name="userService"></param>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

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
        public async Task<ActionResult> Login(string? nickname, string? email, string password)
        {
            try
            {
               return Ok(await _userService.Login(nickname, email, password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
