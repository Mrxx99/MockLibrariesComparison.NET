using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Moq;
using Xunit;

namespace MockLibrariesComparison.Moq
{
    public class DynamicMocking
    {
        [Fact]
        public void ExpectAllPropertiesCalled()
        {
            var person = new Mock<IPerson>(MockBehavior.Strict);

            //person.Setup(x => x.FirstName).Returns((string)null).Verifiable();
            //person.Setup(x => x.LastName).Returns((string)null).Verifiable();
            //person.Setup(x => x.Birthday).Returns(new DateTime()).Verifiable();
            //person.Setup(x => x.Heigth).Returns(0).Verifiable();


            foreach (var propertyInfo in typeof(IPerson).GetProperties().Where(p => Attribute.IsDefined(p, typeof(ShouldBeCalledAttribute))))
            {
                ExpectPropertyGet(person, propertyInfo);

            }

            PersonHasher.GetHashCode(person.Object);

            person.Verify();
        }

        private void ExpectPropertyGet<T>(Mock<T> instance, PropertyInfo property) where T : class
        {
            var expectMethod = typeof(DynamicMockHelper).GetMethod(nameof(DynamicMockHelper.Expect));

            expectMethod = expectMethod.MakeGenericMethod(typeof(T), property.PropertyType);

            var getter = CreateGetterExpression(property, instance);

            expectMethod.Invoke(null, new object[] { instance, getter });
        }

        private void ExpectPropertyGet2<T>(T instance, PropertyInfo property)
        {
            var expectMethod = typeof(DynamicMockHelper).GetMethod(nameof(DynamicMockHelper.Expect));

            expectMethod = expectMethod.MakeGenericMethod(typeof(T), property.PropertyType);

            var getter = CreateGetter(property);

            expectMethod.Invoke(null, new object[] { instance, getter });
        }

        private static Delegate CreateGetter(PropertyInfo property)
        {
            var objParm = Expression.Parameter(property.DeclaringType, "o");
            Type delegateType = typeof(Func<,>).MakeGenericType(property.DeclaringType, property.PropertyType);
            var lambda = Expression.Lambda(delegateType, Expression.Property(objParm, property.Name), objParm);
            return lambda.Compile();
        }

        private static Expression CreateGetterExpression<T>(PropertyInfo property, Mock<T> instance) where T : class
        {
            var objParm = Expression.Parameter(typeof(T), "o");
            Type delegateType = typeof(Func<,>).MakeGenericType(typeof(T), property.PropertyType);
            var lambda = Expression.Lambda(delegateType, Expression.Property(objParm, property.Name), objParm);
            return lambda;
        }

        public static class DynamicMockHelper
        {
            public static void Expect<T, TResult>(Mock<T> mock, Expression<Func<T, TResult>> expr) where T : class
            {
                mock.Setup(expr).Returns(default(TResult)).Verifiable();
            }
        }
    }
}
