namespace Marvel.Domain.Entities
{
    public class FavoriteComic
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string ComicId { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string  ImgUrl { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

    }
}
