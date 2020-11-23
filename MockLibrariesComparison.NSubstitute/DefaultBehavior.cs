using NSubstitute;
using Xunit;

namespace MockLibrariesComparison.NSubstitute
{
    public class DefaultBehavior
    {
        [Fact]
        public void EnumerableShouldBeEmptyAtDefault()
        {
            var nameProviderMock = Substitute.For<INameProvider>();

            Assert.NotNull(nameProviderMock.Genders);
            Assert.Empty(nameProviderMock.Genders);
        }
    }
}
