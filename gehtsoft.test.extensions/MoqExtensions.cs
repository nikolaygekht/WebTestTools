using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace Gehtsoft.Template.Test.Tools
{
    /// <summary>
    /// Extensions of MOQ
    /// </summary>
    public static class MoqExtensions
    {
        /// <summary>
        /// An interface to a verifiable method definition.
        /// </summary>
        public interface IVertifiableMethod
        {
            /// <summary>
            /// Verifies that the method has been called at least once
            /// </summary>
            void Verify();
            /// <summary>
            /// Verifies that the method has been called the specified number of times.
            /// </summary>
            /// <param name="times"></param>
            void Verify(Times times);
        }

        private class Verifiable<T> : IVertifiableMethod
            where T : class
        {
            private readonly Mock<T> mMock;
            private readonly Expression<Action<T>> mExpression;

            public Verifiable(Mock<T> mock, Expression<Action<T>> expression)
            {
                mMock = mock;
                mExpression = expression;
            }

            public void Verify() => mMock.Verify(mExpression);
            public void Verify(Times times) => mMock.Verify(mExpression, times);
        }

        private class Verifiable<T, R> : IVertifiableMethod
            where T : class
        {
            private readonly Mock<T> mMock;
            private readonly Expression<Func<T, R>> mExpression;

            public Verifiable(Mock<T> mock, Expression<Func<T, R>> expression)
            {
                mMock = mock;
                mExpression = expression;
            }

            public void Verify() => mMock.Verify(mExpression);
            public void Verify(Times times) => mMock.Verify(mExpression, times);
        }

        /// <summary>
        /// Setups a verifiable method and returns an object that allows individual verification of this method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mock"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IVertifiableMethod SetupVerifiable<T>(this Mock<T> mock, Expression<Action<T>> expression)
            where T : class
        {
            mock.Setup(expression).Verifiable();
            return new Verifiable<T>(mock, expression);
        }

        /// <summary>
        /// Setups a verifiable async method and returns an object that allows individual verification of this method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="mock"></param>
        /// <param name="expression"></param>
        /// <returns></returns>
        public static IVertifiableMethod SetupVerifiable<T>(this Mock<T> mock, Expression<Func<T, Task>> expression)
            where T : class
        {
            mock.Setup(expression).Returns(Task.CompletedTask).Verifiable();
            return new Verifiable<T, Task>(mock, expression);
        }

        /// <summary>
        /// Setups verifiable method with a return and returns an object that allows individual verification of this method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="mock"></param>
        /// <param name="expression"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IVertifiableMethod SetupVerifiable<T, R>(this Mock<T> mock, Expression<Func<T, R>> expression, R result)
            where T : class
        {
            mock.Setup(expression).Returns(result).Verifiable();
            return new Verifiable<T, R>(mock, expression);
        }

        /// <summary>
        /// Setups verifiable async method with a return and returns an object that allows individual verification of this method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="mock"></param>
        /// <param name="expression"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IVertifiableMethod SetupVerifiable<T, R>(this Mock<T> mock, Expression<Func<T, Task<R>>> expression, R result)
            where T : class
        {
            mock.Setup(expression).Returns(Task.FromResult(result)).Verifiable();
            return new Verifiable<T, Task<R>>(mock, expression);
        }

        /// <summary>
        /// Setups a verifiable method that returns the result specified and returns an object that allows individual verification of this method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="mock"></param>
        /// <param name="expression"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IVertifiableMethod SetupVerifiable<T, R>(this Mock<T> mock, Expression<Func<T, R>> expression, Func<R> result)
            where T : class
        {
            mock.Setup(expression).Returns(result()).Verifiable();
            return new Verifiable<T, R>(mock, expression);
        }

        /// <summary>
        /// Setups an async verifiable method that returns the result specified and returns an object that allows individual verification of this method.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="mock"></param>
        /// <param name="expression"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static IVertifiableMethod SetupVerifiable<T, R>(this Mock<T> mock, Expression<Func<T, Task<R>>> expression, Func<R> result)
            where T : class
        {
            mock.Setup(expression).Returns(Task.FromResult(result())).Verifiable();
            return new Verifiable<T, Task<R>>(mock, expression);
        }
    }
}
