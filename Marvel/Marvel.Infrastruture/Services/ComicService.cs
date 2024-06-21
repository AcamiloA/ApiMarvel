using Marvel.Application.Repositories;
using Marvel.Application.Services;
using Marvel.Domain.Entities;

namespace Marvel.Infrastruture.Services
{
    public class ComicService(IRepository repository) : IComicService
    {
        private readonly IRepository _repository = repository;

        public async Task AddFavoriteComic(FavoriteComic favoriteComic)
        {
            await _repository.AddAsync(favoriteComic);
        }

        public async Task<List<FavoriteComic>> GetAllFavorites(string userId)
        {
            var list = await _repository.GetAllAsync<FavoriteComic>();

            return list.Where(_ => _.User == userId).ToList();
        }

        public async Task RemoveFavoriteComic(FavoriteComic favoriteComic)
        {
            var list = await _repository.GetAllAsync<FavoriteComic>();
            int? id = list.Where(_ => _.User == favoriteComic.User && _.ComicId == favoriteComic.ComicId).Select(_ => _.Id).FirstOrDefault();
            if (id > 0)
                await _repository.DeleteAsync<FavoriteComic>(id??0);
        }
    }
}
