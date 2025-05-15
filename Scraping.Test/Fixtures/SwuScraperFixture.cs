using Scraping.Application.Services;
using Scraping.Domain.Interfaces;
using Moq;
using Scraping.Domain.Models;

namespace Scraping.Tests.Fixtures
{
    public class SwuScraperFixture
    {
        public Mock<ISwuHandler> SwuHandlerMock { get; private set; }
        public SwuScraperService SwuScraperService { get; private set; }

        public SwuScraperFixture()
        {
            SwuHandlerMock = new Mock<ISwuHandler>();

            // Configuração inicial de mocks
            SwuHandlerMock.Setup(x => x.GetPageAsync(It.IsAny<string>()))
         .ReturnsAsync(new SwuPage("<html><body>Mocked Page Content</body></html>", "Página de Exemplo")
         {
             Url = "https://exemplo.com"
         });

            // Instanciando o serviço com o mock
            SwuScraperService = new SwuScraperService(SwuHandlerMock.Object);
        }
    }
}
