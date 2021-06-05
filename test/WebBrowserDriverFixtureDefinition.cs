using Xunit;

namespace webviewtest
{
    [CollectionDefinition(nameof(WebBrowserDriverFixture), DisableParallelization = true)]
    public class WebBrowserDriverFixtureDefinition : ICollectionFixture<WebBrowserDriverFixture> { }
}
