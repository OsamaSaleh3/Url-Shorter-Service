using BuisnessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [ApiController]
    [Route("api/urls")]
    public class UrlController : ControllerBase
    {
        private readonly UrlService _urlService;
        private readonly string _baseUrl = "https://localhost:7204/";

        public UrlController(UrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpPost]
        public async Task<IActionResult> ShortenUrl([FromBody] string longUrl, CancellationToken ct)
        {
            if (string.IsNullOrEmpty(longUrl))
            {
                return BadRequest("URL cannot be null or empty.");
            }
            var shortCode = await _urlService.GenerateUniqueShortUrl(longUrl, ct);
            var shortUrl = $"{_baseUrl}{shortCode}";
            return Ok(new { ShortUrl = shortUrl });
        }
    }
}
