namespace Marvel.Domain.Entities
{
    public class FavoriteComic
    {
        public int? Id { get; set; }
        public string User { get; set; } = string.Empty;
        public int ComicId { get; set; } 
        public string Title { get; set; } = string.Empty;
        public string  ImgUrl { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;

    }
}
