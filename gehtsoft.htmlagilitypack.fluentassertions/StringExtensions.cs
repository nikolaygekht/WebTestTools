using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Gehtsoft.HtmlAgilityPack.FluentAssertions
{
    /// <summary>
    /// Extensions for FluentAsserions' string assertion to check that the value is a json.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Checks that the string value can be parsed as a json value.
        /// </summary>
        /// <param name="assertions"></param>
        /// <param name="because"></param>
        /// <param name="becauseParameters"></param>
        /// <returns></returns>
        public static AndConstraint<StringAssertions> BeJson(this StringAssertions assertions, string because = null, params object[] becauseParameters)
        {
            Execute.Assertion
                .BecauseOf(because, becauseParameters)
                .Given(() => assertions.Subject)
                .ForCondition(subject => {
                    if (string.IsNullOrEmpty(subject))
                        return false;
                    if (subject == "null")
                        return true;
                    try
                    {
                        JsonSerializer.Deserialize<JsonElement>(subject);
                        return true;
                    }
                    catch (Exception )
                    {
                        return false;
                    }
                })
                .FailWith("Expected {context:string} to a json but it is not");
                return new AndConstraint<StringAssertions>(assertions);
        }

        /// <summary>
        /// Parses the string and returns it as a json
        /// </summary>
        /// <param name="jsonText"></param>
        /// <returns></returns>
        public static JsonElement AsJson(this string jsonText) => JsonSerializer.Deserialize<JsonElement>(jsonText ?? "null");
    }
}
