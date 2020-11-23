using Telerik.JustMock;
using Xunit;

namespace MockLibrariesComparison.JustMock
{
    public class DefaultBehavior
    {
        [Fact]
        public void EnumerableShouldBeEmptyAtDefault()
        {
            var nameProviderMock = Mock.Create<INameProvider>();

            Assert.NotNull(nameProviderMock.Genders);
            Assert.Empty(nameProviderMock.Genders);
        }
    }
}
