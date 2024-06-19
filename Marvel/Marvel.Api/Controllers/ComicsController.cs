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
        /// <param name="user"></param>
        /// <param name="comicId"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> FavoriteComic(Guid user, string comicId)
        {
            FavoriteComic model = new()
            {
                UserId = user,
                ComicId = comicId
            };
            await _comicService.AddFavoriteComic(model);

            return Ok();
        }

        /// <summary>
        /// Método para quitar comics favoritos de la lista
        /// </summary>
        /// <param name="user"></param>
        /// <param name="comicId"></param>
        /// <returns></returns>
        [HttpDelete("{comicId}")]
        public async Task<ActionResult> UnfavoriteComic(Guid user, string comicId)
        {
            FavoriteComic model = new()
            {
                UserId = user,
                ComicId = comicId
            };
            await _comicService.RemoveFavoriteComic(model);

            return Ok();
        }

        /// <summary>
        /// Método paralistar todos los comics favoritos de un usuario
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpGet("{user}")]
        public async Task<ActionResult<List<FavoriteComic>>> FavoriteComics(Guid user)
        {
            var list = await _comicService.GetAllFavorites(user);
            if (list.Count != 0)
                return Ok(list);
            return NoContent();
        }
    }
}
