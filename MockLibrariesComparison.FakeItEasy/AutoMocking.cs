using Autofac.Extras.FakeItEasy;
using FakeItEasy;
using Xunit;

namespace MockLibrariesComparison.FakeItEasy
{
    public class AutoMocking
    {
        [Fact]
        public void Test()
        {
            // Arrange
            using var fake = new AutoFake();

            A.CallTo(() => fake.Resolve<INameProvider>().GetName()).Returns("A Name");

            // Act
            var sut = fake.Resolve<MyClassAuto>();
            sut.DoGreeting();

            // Assert
            A.CallTo(() => fake.Resolve<IGreetingService>().Greet("A Name")).MustHaveHappened();
        }
    }
}
