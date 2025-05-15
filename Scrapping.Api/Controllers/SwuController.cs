using Microsoft.AspNetCore.Mvc;
using Scraping.Application.Services;
using System.Threading.Tasks;

namespace Scraping.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SwuController : ControllerBase
    {
        private readonly SwuScraperService _swuScraperService;

        public SwuController(SwuScraperService swuScraperService)
        {
            _swuScraperService = swuScraperService;
        }

        [HttpGet("scrape")]
        public async Task<IActionResult> ScrapeWebsite([FromQuery] string url)
        {
            var content = await _swuScraperService.ScrapeWebsiteAsync(url);
            return Ok(content);
        }
    }
}
