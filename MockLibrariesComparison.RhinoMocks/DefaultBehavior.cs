using Rhino.Mocks;
using Xunit;

namespace MockLibrariesComparison.RhinoMocks
{
    public class DefaultBehavior
    {
        [Fact]
        public void EnumerableShouldBeEmptyAtDefault()
        {
            var nameProviderMock = MockRepository.GenerateStub<INameProvider>();

            Assert.NotNull(nameProviderMock.Genders);
            Assert.Empty(nameProviderMock.Genders);
        }
    }
}
