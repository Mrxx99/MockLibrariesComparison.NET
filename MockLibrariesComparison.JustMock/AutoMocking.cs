using Telerik.JustMock.AutoMock;
using Telerik.JustMock.AutoMock.Ninject;
using Telerik.JustMock.Helpers;
using Xunit;

namespace MockLibrariesComparison.JustMock
{
    public class AutoMocking
    {
        [Fact]
        public void Test()
        {
            // Arrange
            using var container = new MockingContainer<MyClassAuto>();

            container.Arrange<INameProvider>(x => x.GetName()).Returns("A Name").MustBeCalled();
            // or
            //var nameProviderMock = Mock.Create<INameProvider>();
            //nameProviderMock.Arrange(x => x.GetName()).Returns("A Name").MustBeCalled();
            //container.Bind<INameProvider>().ToConstant(nameProviderMock);

            // Act
            var sut = container.Instance;
            sut.DoGreeting();

            // Assert
            container.Assert();

            var greetingServiceMock = container.Get<IGreetingService>();
            greetingServiceMock.Assert(x => x.Greet("A Name"));

            //Assert.Equal(1, Mock.GetTimesCalled(() => greetingServiceMock.Greet("A Name")));

            // This throws an exception
            //Assert.Equal(1, Mock.GetTimesCalled(() => container.Get<IGreetingService>().Greet("A Name")));
        }
    }
}
