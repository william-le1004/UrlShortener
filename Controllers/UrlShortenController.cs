using Microsoft.AspNetCore.Mvc;
using UrlShortener.Models;
using UrlShortener.Repository;

namespace UrlShortener.Controllers
{
    
    [ApiController]
    public class UrlShortenController : ControllerBase
    {
        private readonly IUrlShortenService _urlService;
        private readonly IConfiguration _config;

        public UrlShortenController(IUrlShortenService urlService, IConfiguration config)
        {
            _urlService = urlService;
            _config = config;
        }

        [HttpPost]
        [Route("api/short-url")]
        public async Task<IActionResult> URLShorten([FromBody]UrlQuery url)
        {
            if(url == null) { return BadRequest(); }
            var shortURL = await _urlService.GenerateShortUrl(url);
            return Ok(shortURL);
        }

        [HttpGet]
        [Route("api/{code}")]
        public async Task<IActionResult> RedirectUrl(string code)
        {
            if (code == null) { return BadRequest(); }
            var url = await _urlService.GetUrl(code);
            return Redirect(url.ToString());
        }
    }
}
