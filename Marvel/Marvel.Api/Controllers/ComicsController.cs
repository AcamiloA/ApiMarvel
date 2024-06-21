using Marvel.Api.Helpers;
using Marvel.Application.Repositories;
using Marvel.Application.Services;
using Marvel.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marvel.Api.Controllers
{
    /// <summary>
    /// Controlador para gestionar los comics
    /// </summary>
    [Route("v1/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class ComicsController(IComicService comicService) : ControllerBase
    {
        private readonly IComicService _comicService = comicService;
        /// <summary>
        /// Método para añadir comics a la lista de favoritosde cada usuario.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Response>> FavoriteComic([FromBody] FavoriteComic comic)
        {
            try
            {
                await _comicService.AddFavoriteComic(comic);
                return Ok(new Response()
                {
                    IsSuccess = true,
                    Message = "Comic agregado a favoritos"
                });
            }
            catch (Exception ex)
            {
                return Conflict(new Response()
                {
                    IsSuccess = false,
                    Message = "Ocurrió un error al guardar el comic",
                    Errors = ex.Message
                });
            }            
        }

        /// <summary>
        /// Método para quitar comics favoritos de la lista
        /// </summary>
        /// <param name="user"></param>
        /// <param name="comic"></param>
        /// <returns></returns>
        [HttpDelete("{user}/{comic}")]
        public async Task<ActionResult<Response>> UnfavoriteComic(string user, int comic)
        {
            try
            {
                FavoriteComic model = new()
                {
                    User = user,
                    ComicId = comic
                };
                await _comicService.RemoveFavoriteComic(model);

                return Ok(new Response()
                {
                    IsSuccess = true,
                    Message = "Comic eliminado exitosamente",

                }); ;
            }
            catch (Exception ex)
            {
                return Conflict(new Response()
                {
                    IsSuccess = false,
                    Message = "Error al elminar comic de favoritos",
                    Errors = ex.Message
                }); 
            }
            
        }

        /// <summary>
        /// Método paralistar todos los comics favoritos de un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("{user}")]
        public async Task<ActionResult<List<FavoriteComic>>> FavoriteComics(string user)
        {
            var list = await _comicService.GetAllFavorites(user);
            if (list.Count != 0)
                return Ok(list);
            return NoContent();
        }
    }
}
