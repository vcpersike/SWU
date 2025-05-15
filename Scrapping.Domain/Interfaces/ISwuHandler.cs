using Scraping.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Scraping.Domain.Interfaces
{
    public interface ISwuHandler
    {
        Task InitializeAsync();
        Task<SwuPage> GetPageAsync(string url);

        Task CloseAsync();
    }
}
