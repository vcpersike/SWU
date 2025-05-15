using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping.Application.Interfaces
{
    public interface ISwuScraper
    {
        Task<string> ScrapeWebsiteAsync(string url);
    }
}
