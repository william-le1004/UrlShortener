using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Entities;
using UrlShortener.Models;
using UrlShortener.Repository;

namespace UrlShortener.Service;
public class UrlShortenService : IUrlShortenService
{
    private readonly UrlShortenerContext _context;
    private readonly IHttpContextAccessor _httpContext;
    public const int NumberOfChars = 7;
    private const string Alphabet = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private readonly Random _random = new();

    public UrlShortenService(UrlShortenerContext context, IHttpContextAccessor httpContext)
    {
        _context = context;
        _httpContext = httpContext;
    }

    /// <summary>Generates the short URL.</summary>
    /// <param name="request">The request.</param>
    /// <returns></returns>
    public async Task<string> GenerateShortUrl(UrlQuery request)
    {
        if (!Uri.TryCreate(request.URL, UriKind.Absolute, out _))
        {
            return "Invalid Url";
        }
        var currentHttpContext = _httpContext.HttpContext;
        var currentHost = currentHttpContext.Request.Host;
        while (true)
        {
            char[] codeChars = GenerateUniqueCode();
            string code = new string(codeChars);
            var url = new Url
            {
                Id = new Guid(),
                LongURL = request.URL,
                Code = code,
                ShortURL = $"{currentHttpContext}://{currentHost}/api/{code}",
                Create_at = DateTime.Now
            };
            if (!await _context.urls.AnyAsync(x => x.Code == code))
            {
                await _context.urls.AddAsync(url);
                await _context.SaveChangesAsync();
            }
            return url.ShortURL;
        }
    }

    /// <summary>Generates the unique code.</summary>
    /// <returns></returns>
    private char[] GenerateUniqueCode()
    {
        var codeChars = new char[NumberOfChars];

        for (int i = 0; i < NumberOfChars; i++)
        {
            var indexCode = _random.Next(Alphabet.Length - 1);
            codeChars[i] = Alphabet[indexCode];
        }

        return codeChars;
    }

    /// <summary>Gets the URL.</summary>
    /// <param name="code">The unique code.</param>
    /// <returns></returns>
    public async Task<string> GetUrl(string code)
    {
        if (code == null) { return "Invalid code !!"; }
        var originUrl = _context.urls.FirstOrDefault(x => x.Code == code);
        return originUrl.LongURL;
    }
}
