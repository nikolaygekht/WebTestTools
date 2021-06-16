using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using FluentAssertions.Specialized;

namespace Gehtsoft.Test.Extensions
{
    /// <summary>
    /// Object extensions
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Finds a property for the object.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static PropertyInfo FindProperty(this object v, string propertyName)
        {
            if (v == null)
                return null;
            return v.GetType().GetProperty(propertyName);
        }

        /// <summary>
        /// Checks whether the object has a property specified.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static bool HasProperty(this object v, string propertyName) => v.FindProperty(propertyName) != null;

        /// <summary>
        /// Gets a property value
        /// </summary>
        /// <param name="v"></param>
        /// <param name="propertyName"></param>
        /// <param name="expectedType"></param>
        /// <returns></returns>
        public static object GetProperty(this object v, string propertyName, Type expectedType = null)
        {
            var p = v.FindProperty(propertyName);
            if (p == null)
                throw new ArgumentException($"Property {propertyName} does not exist", nameof(propertyName));

            var pv = p.GetValue(v);
            if (pv == null)
                return null;

            if (expectedType != null)
                expectedType = Nullable.GetUnderlyingType(expectedType) ?? expectedType;
            if (expectedType != null && pv.GetType() != expectedType && !expectedType.IsInstanceOfType(pv))
                pv = Convert.ChangeType(pv, expectedType);

            return pv;
        }

        /// <summary>
        /// Gets a property value of the type specified.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="v"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static T GetProperty<T>(this object v, string propertyName) => (T)GetProperty(v, propertyName);

        /// <summary>
        /// Checks whether the object has a property with the value specified.
        /// </summary>
        /// <param name="v"></param>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool HasProperty(this object v, string propertyName, object value)
        {
            var pi = v.FindProperty(propertyName);
            if (pi == null)
                return false;
            var pv = pi.GetValue(v);
            if (value == null)
                return pv == null;
            return value.Equals(pv);
        }

        /// <summary>
        /// Asserts that the object has a property with the value specified.
        /// </summary>
        /// <param name="assertions"></param>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public static AndConstraint<ObjectAssertions> HaveProperty(this ObjectAssertions assertions, string name, object value, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => assertions.Subject)
                .ForCondition(v => v.HasProperty(name, value))
                .FailWith("Expected the object to have a property {0} equals to {1}, but it does not", name, value);
            return new AndConstraint<ObjectAssertions>(assertions);
        }

        /// <summary>
        /// Asserts that the object has a property.
        /// </summary>
        /// <param name="assertions"></param>
        /// <param name="name"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public static AndConstraint<ObjectAssertions> HaveProperty(this ObjectAssertions assertions, string name, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => assertions.Subject)
                .ForCondition(v => v.HasProperty(name))
                .FailWith("Expected the object to have a property {0}, but it does not", name);
            return new AndConstraint<ObjectAssertions>(assertions);
        }
    }
}
