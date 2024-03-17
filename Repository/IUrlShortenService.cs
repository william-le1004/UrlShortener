using UrlShortener.Models;

namespace UrlShortener.Repository
{
    public interface IUrlShortenService
    {
        Task<string> GenerateShortUrl(UrlQuery request);
        Task<string> GetUrl(string code);
    }
}
