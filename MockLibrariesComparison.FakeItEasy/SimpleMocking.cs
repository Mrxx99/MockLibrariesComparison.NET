using System;
using FakeItEasy;
using Xunit;

namespace MockLibrariesComparison.FakeItEasy
{

    public class SimpleMocking
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var greetingServiceMock = A.Fake<IGreetingService>();
            var nameProviderMock = A.Fake<INameProvider>();

            A.CallTo(() => nameProviderMock.GetName()).Returns("A Name");

            // Act
            var sut = new MyClassAuto(greetingServiceMock, nameProviderMock);
            sut.DoGreeting();

            // Assert
            A.CallTo(() => greetingServiceMock.Greet("A Name")).MustHaveHappened();
        }
    }
}
