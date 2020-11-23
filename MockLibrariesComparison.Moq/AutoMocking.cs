using System;
using Xunit;
using Moq.AutoMock;

namespace MockLibrariesComparison.Moq
{
    public class AutoMocking
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var mocker = new AutoMocker();

            mocker.GetMock<INameProvider>().Setup(x => x.GetName()).Returns("A Name");

            // Act
            var sut = mocker.CreateInstance<MyClassAuto>();
            sut.DoGreeting();

            // Assert
            mocker.GetMock<IGreetingService>().Verify(x => x.Greet("A Name"));
        }
    }
}
