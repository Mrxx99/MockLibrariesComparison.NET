using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using NSubstitute;
using NSubstitute.ReceivedExtensions;
using Xunit;

namespace MockLibrariesComparison.NSubstitute
{
    public class DynamicMocking
    {
        [Fact]
        public void ExpectAllPropertiesCalled()
        {
            var person = Substitute.For<IPerson>();

            PersonHasher.GetHashCode(person);

            //_ = person.Received().Heigth;
            //_ = SubstituteExtensions.Received(person).FirstName;

            foreach (var propertyInfo in typeof(IPerson).GetProperties())
            {
                if (Attribute.IsDefined(propertyInfo, typeof(ShouldBeCalledAttribute)))
                {
                    VerifyPropertyGet(person, propertyInfo);
                }
                else
                {
                    VerifyNoPropertyGet(person, propertyInfo);
                }
            }
        }

        private void VerifyPropertyGet<T>(T instance, PropertyInfo property)
        {
            var receiveMethod = typeof(DynamicMockHelper).GetMethod(nameof(DynamicMockHelper.Received));

            receiveMethod = receiveMethod.MakeGenericMethod(typeof(T));

            var receiver = receiveMethod.Invoke(null, new object[] { instance });

            property.GetValue(receiver);
        }

        private void VerifyNoPropertyGet<T>(T instance, PropertyInfo property)
        {
            var receiveMethod = typeof(DynamicMockHelper).GetMethod(nameof(DynamicMockHelper.DidNotReceive));

            receiveMethod = receiveMethod.MakeGenericMethod(typeof(T));

            var receiver = receiveMethod.Invoke(null, new object[] { instance });

            property.GetValue(receiver);
        }

        public static class DynamicMockHelper
        {
            public static T Received<T>(T instance) where T : class
            {
                return instance.Received();
            }

            public static T DidNotReceive<T>(T instance) where T : class
            {
                return instance.DidNotReceive();
            }
        }
    }
}
