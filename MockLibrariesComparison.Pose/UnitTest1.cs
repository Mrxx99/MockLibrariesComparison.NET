using System;
using Pose;
using Xunit;

namespace MockLibrariesComparison.Pose
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            Shim dateTimeShim = Shim.Replace(() => DateTime.Now).With(() => new DateTime(2004, 4, 4));

            PoseContext.Isolate(() =>
            {
                Assert.Equal(new DateTime(2004, 4, 4), DateTime.Now);
            }, dateTimeShim);


            var myClass = new MyClassAuto(null, null);

            var myClassShim = Shim.Replace(() => myClass.GetGreetingDate(Is.A<string>()))
                //.With<MyClassAuto, DateTime>(x => new DateTime(2021, 01, 01));
                .With(delegate (MyClassAuto c) { return new DateTime(2021, 01, 01); });

            DateTime actualResult = new DateTime();

            PoseContext.Isolate(() =>
            {
                actualResult = myClass.GetGreetingDate(null);
            }, myClassShim);

            Assert.Equal(new DateTime(2021, 01, 01), actualResult);

        }
    }
}
