using PuppeteerSharp;
using Scraping.Domain.Interfaces;
using Scraping.Domain.Models;
using System.Threading.Tasks;
using System.IO;

namespace Scraping.Infra
{
    public class SwuHandler : ISwuHandler
    {
        private IBrowser _browser;

        public async Task InitializeAsync()
        {
            var browserFetcher = Puppeteer.CreateBrowserFetcher(new BrowserFetcherOptions
            {
                Path = ".local-chromium"
            });

            var latestRevision = await browserFetcher.DownloadAsync();
            var executablePath = latestRevision.GetExecutablePath();

            if (!File.Exists(executablePath))
            {
                throw new FileNotFoundException($"Chromium não encontrado no caminho: {executablePath}");
            }

            _browser = await Puppeteer.LaunchAsync(new LaunchOptions
            {
                Headless = true,
                ExecutablePath = executablePath,
                Args = new[]
                {
                    "--no-sandbox",
                    "--disable-setuid-sandbox",
                    "--ignore-certificate-errors",
                    "--disable-web-security",
                    "--allow-insecure-localhost"
                }
            });
        }

        public async Task<SwuPage> GetPageAsync(string url)
        {
            if (_browser == null)
                await InitializeAsync();

            var puppeteerPage = await _browser.NewPageAsync();

            // ✅ Ignora imagens para acelerar o carregamento
            await puppeteerPage.SetRequestInterceptionAsync(true);
            puppeteerPage.Request += async (sender, e) =>
            {
                if (e.Request.ResourceType == ResourceType.Image)
                {
                    await e.Request.AbortAsync();
                }
                else
                {
                    await e.Request.ContinueAsync();
                }
            };

            // ✅ 1. Acessa a URL fornecida e espera o carregamento completo
            await puppeteerPage.GoToAsync(url, WaitUntilNavigation.Networkidle0);

            // ✅ 2. Substituição do WaitForTimeoutAsync por Task.Delay
            await Task.Delay(2000);

            // ✅ 3. Alternativa para aguardar conteúdo específico
            await puppeteerPage.WaitForSelectorAsync("body");

            // ✅ 4. Captura o conteúdo renderizado
            var content = await puppeteerPage.GetContentAsync();
            return new SwuPage(content);
        }

        public async Task CloseAsync()
        {
            if (_browser != null)
                await _browser.CloseAsync();
        }
    }
}
