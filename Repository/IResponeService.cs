using UrlShortener.Models;

namespace UrlShortener.Repository;

public interface IResponeService
{
    Task<APIRespone<string>> Success();
    Task<APIRespone<string>> Fail();
}
