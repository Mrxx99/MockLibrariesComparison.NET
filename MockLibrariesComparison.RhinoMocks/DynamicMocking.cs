using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Rhino.Mocks;
using Xunit;

namespace MockLibrariesComparison.RhinoMocks
{
    public class DynamicMocking
    {
        [Fact]
        public void ExpectAllPropertiesCalled()
        {
            var person = MockRepository.GenerateStrictMock<IPerson>();

            //person.Expect(x => x.FirstName).Return(null);
            //person.Expect(x => x.LastName).Return(null);
            //person.Expect(x => x.Birthday).Return(new DateTime());
            //person.Expect(x => x.Heigth).Return(0);

            foreach (var propertyInfo in typeof(IPerson).GetProperties().Where(p => Attribute.IsDefined(p, typeof(ShouldBeCalledAttribute))))
            {
                ExpectPropertyGet(person, propertyInfo);
            }

            PersonHasher.GetHashCode(person);

            person.VerifyAllExpectations();
        }

        private void ExpectPropertyGet<T>(T instance, PropertyInfo property)
        {
            var expectMethod = typeof(RhinoMocksExtensions).GetMethods(BindingFlags.Static | BindingFlags.Public)
                .Where(m => m.Name == nameof(RhinoMocksExtensions.Expect))
                .First(m => m.GetParameters()[1].ParameterType.Name == typeof(Function<,>).Name);

            expectMethod = expectMethod.MakeGenericMethod(typeof(T), property.PropertyType);

            var getter = CreateGetter(property);

            var returner = expectMethod.Invoke(null, new object[] { instance, getter });

            var returnMethod = returner.GetType().GetMethod("Return");

            returnMethod.Invoke(returner, new[] { GetDefault(property.PropertyType) });
        }

        private object GetDefault(Type Type)
        {
            return Type.IsValueType ? Activator.CreateInstance(Type) : null;
        }

        private static Delegate CreateGetter(PropertyInfo property)
        {
            var objParm = Expression.Parameter(property.DeclaringType, "o");
            Type delegateType = typeof(Function<,>).MakeGenericType(property.DeclaringType, property.PropertyType);
            var lambda = Expression.Lambda(delegateType, Expression.Property(objParm, property.Name), objParm);
            return lambda.Compile();
        }
    }
}
