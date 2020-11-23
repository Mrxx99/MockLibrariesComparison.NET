using System;
using Telerik.JustMock;
using Telerik.JustMock.Helpers;
using Xunit;

namespace MockLibrariesComparison.JustMock
{
    public class OutAndRefMocking
    {
        [Fact]
        public void Test()
        {
            var greetingService = Mock.Create<IGreetingService>();

            DateTime date = new DateTime(2021, 1, 1);

            greetingService.Arrange(x => x.TryParseDate("2021-01-01", out date)).Returns(true);

            var c = new MyClassAuto(greetingService, null);
            var d = c.GetGreetingDate("2021-01-01");

            Assert.Equal(date, d);
        }
    }
}
