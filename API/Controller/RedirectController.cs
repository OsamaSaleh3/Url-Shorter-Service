using BuisnessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controller
{
    [ApiController]
    [Route("")]
    public class RedirectController : ControllerBase
    {
        private readonly UrlService _urlService;

        public RedirectController(UrlService urlService)
        {
            _urlService = urlService;
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToUrl(string shortCode, CancellationToken ct)
        {
            var longUrl = await _urlService.GetLongUrl(shortCode, ct);
            if (string.IsNullOrEmpty(longUrl))
            {
                return NotFound("Short URL not found.");
            }
            return Redirect(longUrl);
        }
    }
}
