namespace UrlShortener.Entities
{
    public class Url
    {
        public Guid Id { get; set; }
        public string LongURL { get; set; } = string.Empty;
        public string ShortURL { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public DateTime Create_at { get; set; }
        //public string User_id { get; set; }
    }
}
