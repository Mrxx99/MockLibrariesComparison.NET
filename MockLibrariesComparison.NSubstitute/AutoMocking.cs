using System;
using Xunit;
using Ninject.MockingKernel.NSubstitute;
using Ninject;
using NSubstitute;

namespace MockLibrariesComparison.NSubstitute
{
    public class AutoMocking
    {
        [Fact]
        public void Test()
        {
            // Arrange
            using var kernel = new NSubstituteMockingKernel();

            kernel.Get<INameProvider>().GetName().Returns("A Name");

            // Act
            var sut = kernel.Get<MyClassAuto>();
            sut.DoGreeting();

            // Assert
            kernel.Get<IGreetingService>().Received().Greet("A Name");
        }
    }
}
