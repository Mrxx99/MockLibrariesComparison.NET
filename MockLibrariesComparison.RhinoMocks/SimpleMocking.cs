using System;
using Xunit;
using Rhino.Mocks;

namespace MockLibrariesComparison.RhinoMocks
{
    public class SimpleMocking
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var greetingServiceMock = MockRepository.GenerateStub<IGreetingService>();
            var nameProviderMock = MockRepository.GenerateMock<INameProvider>();

            nameProviderMock.Expect(x => x.GetName()).Return("A Name");

            // Act
            var sut = new MyClassAuto(greetingServiceMock, nameProviderMock);
            sut.DoGreeting();

            // Assert
            nameProviderMock.VerifyAllExpectations();
            greetingServiceMock.AssertWasCalled(x => x.Greet("A Name"));
        }
    }
}
