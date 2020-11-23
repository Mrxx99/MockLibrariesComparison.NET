using System;
using Moq;
using Xunit;

namespace MockLibrariesComparison.Moq
{

    public class SimpleMocking
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var greetingServiceMock = new Mock<IGreetingService>();
            var nameProviderMock = new Mock<INameProvider>();

            nameProviderMock.Setup(x => x.GetName()).Returns("A Name");

            // Act
            var sut = new MyClassAuto(greetingServiceMock.Object, nameProviderMock.Object);
            sut.DoGreeting();

            // Assert
            greetingServiceMock.Verify(x => x.Greet("A Name"));
        }
    }
}
