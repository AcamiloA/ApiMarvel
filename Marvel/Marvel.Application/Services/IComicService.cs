using Marvel.Domain.Entities;

namespace Marvel.Application.Services
{
    public interface IComicService
    {
        Task AddFavoriteComic(FavoriteComic favoriteComic);
        Task RemoveFavoriteComic(FavoriteComic favoriteComic);
        Task<List<FavoriteComic>> GetAllFavorites(Guid userId);
    }
}
