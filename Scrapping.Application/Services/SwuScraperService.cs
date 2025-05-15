using System;
using PuppeteerSharp;
using Scraping.Domain.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using Scraping.Domain.Interfaces;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Scraping.Application.Interfaces;

namespace Scraping.Application.Services
{
    public class SwuScraperService : ISwuScraper
    {
        private readonly ISwuHandler _swuHandler;

        public SwuScraperService(ISwuHandler swuHandler)
        {
            _swuHandler = swuHandler;
        }

        public async Task<string> ScrapeWebsiteAsync(string url)
        {
            await _swuHandler.InitializeAsync();
            var page = await _swuHandler.GetPageAsync(url);
            await _swuHandler.CloseAsync();
            return page.Content;
        }
    }
}
