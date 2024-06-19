namespace Marvel.Domain.Entities
{
    public class FavoriteComic
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
        public string ComicId { get; set; } = string.Empty;

    }
}
