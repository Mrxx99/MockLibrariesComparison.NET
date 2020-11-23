using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Telerik.JustMock;
using Xunit;

namespace MockLibrariesComparison.JustMock
{
    public class DynamicMocking
    {
        [Fact]
        public void ExpectAllPropertiesCalled()
        {
            var person = Mock.Create<IPerson>(Behavior.Strict);

            //Mock.Arrange(person, x => x.FirstName).Returns((string)null).MustBeCalled();
            //Mock.Arrange(() => person.LastName).Returns((string)null).MustBeCalled();
            //Mock.Arrange(() => person.Birthday).Returns(new DateTime()).MustBeCalled();
            //Mock.Arrange(() => person.Heigth).Returns(0).MustBeCalled();


            foreach (var propertyInfo in typeof(IPerson).GetProperties().Where(p => Attribute.IsDefined(p, typeof(ShouldBeCalledAttribute))))
            {
                ExpectPropertyGet(person, propertyInfo);

            }

            PersonHasher.GetHashCode(person);

            Mock.Assert(person);
        }

        private void ExpectPropertyGet<T>(T instance, PropertyInfo property)
        {
            var expectMethod = typeof(DynamicMockHelper).GetMethod(nameof(DynamicMockHelper.Expect));

            expectMethod = expectMethod.MakeGenericMethod(typeof(T), property.PropertyType);

            var getter = CreateGetterExpression(property, instance);

            expectMethod.Invoke(null, new object[] { getter });
        }

        private void ExpectPropertyGet2<T>(T instance, PropertyInfo property)
        {
            var expectMethod = typeof(DynamicMockHelper).GetMethod(nameof(DynamicMockHelper.ExpectFunc));

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

        private static Expression CreateGetterExpression<T>(PropertyInfo property, T instance)
        {
            Type delegateType = typeof(Func<>).MakeGenericType(property.PropertyType);
            var lambda = Expression.Lambda(delegateType, Expression.Property(Expression.Constant(instance, typeof(T)), property.Name));
            return lambda;
        }
    }

    public static class DynamicMockHelper
    {
        public static void ExpectFunc<T, TResult>(T obj, Func<T, TResult> func)
        {
            Mock.Arrange(obj, func).Returns(default(TResult)).MustBeCalled();
        }

        public static void Expect<T, TResult>(Expression<Func<TResult>> expr)
        {
            Mock.Arrange(expr).Returns(default(TResult)).MustBeCalled();
        }
    }
}
