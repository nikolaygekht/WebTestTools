using System;
using System.Linq;
using System.Linq.Expressions;
using Esprima.Ast;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    /// <summary>
    /// Assertions for JavaScript statements
    /// </summary>
    public class NodeAssertion : ReferenceTypeAssertions<Node, NodeAssertion>
    {
        internal NodeAssertion(Node subject) : base(subject)
        {
        }

        /// <summary>
        /// The node found by the last invocation of the Contain method
        /// </summary>
        public Node Found { get; private set; }

        /// <summary>
        /// Context identifier
        /// </summary>
        protected override string Identifier => "node";

        /// <summary>
        /// Checks whether the node has any child nodes
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseArgs"></param>
        /// <returns></returns>
        public AndConstraint<NodeAssertion> HaveChildren(string because = null, params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(node => node.ChildNodes.Any())
                .FailWith("Expected {context:node} to have children but it does not");
            return new AndConstraint<NodeAssertion>(this);
        }

        /// <summary>
        /// Checks whether the node has any child nodes
        /// </summary>
        /// <param name="because"></param>
        /// <param name="becauseArgs"></param>
        /// <returns></returns>
        public AndConstraint<NodeAssertion> HaveNoChildren(string because = null, params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(node => !node.ChildNodes.Any())
                .FailWith("Expected {context:node} to have no children but it does");
            return new AndConstraint<NodeAssertion>(this);
        }

        /// <summary>
        /// Checks whether the node contains the node matching the predicate specified
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseArgs"></param>
        /// <returns></returns>
        public AndConstraint<NodeAssertion> Contain(Expression<Func<Node, bool>> predicate, string because = null, params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(node => (Found = node.Find(predicate.Compile())) != null)
                .FailWith("Expected {context:node} contain a node matching {0} but it does not", predicate);
            return new AndConstraint<NodeAssertion>(this);
        }

        /// <summary>
        /// Checks whether the node does not contain the node matching the predicate specified
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseArgs"></param>
        /// <returns></returns>
        public AndConstraint<NodeAssertion> NotContain(Expression<Func<Node, bool>> predicate, string because = null, params object[] becauseArgs)
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(node => node.Find(predicate.Compile()) == null)
                .FailWith("Expected {context:node} does not contain a node matching {0} but it does", predicate);
            return new AndConstraint<NodeAssertion>(this);
        }

        /// <summary>
        /// Checks whether the node contains the node matching the predicate specified
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseArgs"></param>
        /// <returns></returns>
        public AndConstraint<NodeAssertion> Contain<T>(Expression<Func<T, bool>> predicate, string because = null, params object[] becauseArgs)
                where T : Node
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(node => (Found = node.Find<T>(predicate.Compile())) != null)
                .FailWith("Expected {context:node} contain a node matching {0} but it does not", predicate);
            return new AndConstraint<NodeAssertion>(this);
        }

        /// <summary>
        /// Checks whether the node does not contain the node matching the predicate specified
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseArgs"></param>
        /// <returns></returns>
        public AndConstraint<NodeAssertion> NotContain<T>(Expression<Func<T, bool>> predicate, string because = null, params object[] becauseArgs)
                where T : Node
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(node => node.Find<T>(predicate.Compile()) == null)
                .FailWith("Expected {context:node} does not contain a node matching {0} but it does", predicate);
            return new AndConstraint<NodeAssertion>(this);
        }


        /// <summary>
        /// Checks whether the node is a type specified and matches the expression specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseArgs"></param>
        /// <returns></returns>

        public AndConstraint<NodeAssertion> Match<T>(Expression<Func<T, bool>> predicate, string because = null, params object[] becauseArgs)
            where T : Node
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(s => s is T)
                .FailWith("Expected {context:node} to be {0} but it is {1}", typeof(T).FullName, Subject?.GetType()?.FullName)
                .Then
                .ForCondition(s => predicate.Compile()(s as T))
                .FailWith("Expected {context:node} to match {0} it does not", predicate);

            return new AndConstraint<NodeAssertion>(this);
        }

        /// <summary>
        /// Checks whether the node is a type specified and is not matches the expression specified
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="predicate"></param>
        /// <param name="because"></param>
        /// <param name="becauseArgs"></param>
        /// <returns></returns>

        public AndConstraint<NodeAssertion> NotMatch<T>(Expression<Func<T, bool>> predicate, string because = null, params object[] becauseArgs)
            where T : Node
        {
            Execute.Assertion
                .BecauseOf(because, becauseArgs)
                .Given(() => Subject)
                .ForCondition(s => s is T)
                .FailWith("Expected {context:node} to be {0} but it is {1}", typeof(T).FullName, Subject?.GetType()?.FullName)
                .Then
                .ForCondition(s => !(predicate.Compile()(s as T)))
                .FailWith("Expected {context:node} does not match {0} the predicate, but it does", predicate);

            return new AndConstraint<NodeAssertion>(this);
        }
    }

}
