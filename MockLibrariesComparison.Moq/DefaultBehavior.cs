using Moq;
using Xunit;

namespace MockLibrariesComparison.Moq
{
    public class DefaultBehavior
    {
        [Fact]
        public void EnumerableShouldBeEmptyAtDefault()
        {
            var nameProviderMock = Mock.Of<INameProvider>();

            Assert.NotNull(nameProviderMock.Genders);
            Assert.Empty(nameProviderMock.Genders);
        }
    }
}
