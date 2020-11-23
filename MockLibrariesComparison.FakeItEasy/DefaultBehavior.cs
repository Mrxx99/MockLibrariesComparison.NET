using FakeItEasy;
using Xunit;

namespace MockLibrariesComparison.FakeItEasy
{
    public class DefaultBehavior
    {
        [Fact]
        public void EnumerableShouldBeEmptyAtDefault()
        {
            var nameProviderMock = A.Fake<INameProvider>();

            Assert.NotNull(nameProviderMock.Genders);
            Assert.Empty(nameProviderMock.Genders);
        }
    }
}
