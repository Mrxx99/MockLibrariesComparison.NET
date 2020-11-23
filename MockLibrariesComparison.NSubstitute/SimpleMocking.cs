using System;
using NSubstitute;
using Xunit;

namespace MockLibrariesComparison.NSubstitute
{
    public class SimpleMocking
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var greetingServiceMock = Substitute.For<IGreetingService>();
            var nameProviderMock = Substitute.For<INameProvider>();

            nameProviderMock.GetName().Returns("A Name");

            // Act
            var sut = new MyClassAuto(greetingServiceMock, nameProviderMock);
            sut.DoGreeting();

            // Assert
            greetingServiceMock.Received().Greet("A Name");
        }
    }
}
