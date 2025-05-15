using Moq;
using Scraping.Tests.Fixtures;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scraping.Test.Handlers
{
    public class SwuScraperServiceTests : IClassFixture<SwuScraperFixture>
    {
        private readonly SwuScraperFixture _fixture;

        public SwuScraperServiceTests(SwuScraperFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact(DisplayName = "Deve retornar conteúdo HTML mockado")]
        public async void DeveRetornarConteudoHtmlMockado()
        {
            var url = "https://exemplo.com";

            var result = await _fixture.SwuScraperService.GetContentAsync(url);

            result.Should().NotBeNull();
            result.Should().Contain("Mocked Page Content"); // Verifique se esse conteúdo está correto
        }

        [Fact(DisplayName = "Deve chamar o método GetPageAsync uma vez")]
        public async void DeveChamarMetodoGetPageAsync()
        {
            var url = "https://exemplo.com";

            await _fixture.SwuScraperService.GetContentAsync(url);

            _fixture.SwuHandlerMock.Verify(x => x.GetPageAsync(It.IsAny<string>()), Times.Once);
        }
    }
}
