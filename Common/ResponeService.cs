using UrlShortener.Models;
using UrlShortener.Repository;

namespace UrlShortener.Common
{
    public class ResponeService : IResponeService
    {
        public Task<APIRespone<string>> Fail()
        {
            throw new NotImplementedException();
        }

        public Task<APIRespone<string>> Success()
        {
            throw new NotImplementedException();
        }
    }
}
