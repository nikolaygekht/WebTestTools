using System.Collections.Generic;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Gehtsoft.Webtest.Spellchecker
{
    /// <summary>
    /// The extension for FluentAssertions's string assertions to validate spelling.
    /// </summary>
    public static class SpellingStringExtensions
    {
        /// <summary>
        /// Asserts that the string is spelled correctly.
        /// </summary>
        /// <param name="assertions"></param>
        /// <param name="dictionary"></param>
        /// <param name="exemptions"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public static AndConstraint<StringAssertions> BeSpelledCorrectly(this StringAssertions assertions, string dictionary = "en_US", string exemptions = null, string because = null, params object[] becauseParameters)
        {
            string[] exemptionsList = null;

            if (!string.IsNullOrEmpty(exemptions))
            {
                exemptionsList = exemptions.Split(new char[] { ',' });
                SpellCheckerFactory.Instance.Initialize(dictionary, exemptionsList);
            }

            var failedList = SpellCheckerFactory.Instance[dictionary].SpellMany(assertions.Subject);

            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => failedList)
                .ForCondition(list => list == null || list.Count == 0)
                .FailWith("Expected {context:string} to be spelled correctly but it isn't. The list of misspelled words are {0}", failedList);

            return new AndConstraint<StringAssertions>(assertions);
        }
    }
}
