using System;
using Xunit;
using Rhino.Mocks;
using AutoMock;

namespace MockLibrariesComparison.RhinoMocks
{


    public class AutoMocking
    {
        [Fact]
        public void Test()
        {
            // Arrange
            var autoMocker = new RhinoAutoMocker<MyClassAuto>();

            autoMocker.Get<IGreetingService>().Stub(x => x.Greet("A Name"));
            autoMocker.Get<INameProvider>().Expect(x => x.GetName()).Return("A Name");

            // Act
            var sut = autoMocker.ClassUnderTest;
            sut.DoGreeting();

            // Assert
            autoMocker.Get<INameProvider>().VerifyAllExpectations();
            autoMocker.Get<IGreetingService>().AssertWasCalled(x => x.Greet("A Name"));
        }
    }
}
