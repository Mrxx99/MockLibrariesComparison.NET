using System;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Xunit;

namespace MockLibrariesComparison.JustMock
{

    public class SimpleMocking
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var greetingServiceMock = Mock.Create<IGreetingService>();
            var nameProviderMock = Mock.Create<INameProvider>();

            //Mock.Arrange(() => nameProviderMock.GetName()).Returns("A Name").MustBeCalled();
            // or 
            nameProviderMock.Arrange(x => x.GetName()).Returns("A Name").MustBeCalled();

            // Act
            var sut = new MyClassAuto(greetingServiceMock, nameProviderMock);
            sut.DoGreeting();

            // Assert
            nameProviderMock.AssertAll();
            greetingServiceMock.Assert(x => x.Greet("A Name"));
            //Assert.Equal(1, Mock.GetTimesCalled(() => greetingServiceMock.Greet("A Name")));
        }
    }
}
