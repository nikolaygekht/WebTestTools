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
    public static class StringExtensions
    {
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

        public static JsonElement AsJson(this string jsonText) => JsonSerializer.Deserialize<JsonElement>(jsonText ?? "null");

        public static bool EqualsTo(this JsonElement element, object value)
        {
            if (element.ValueKind == JsonValueKind.Null)
                return value == null;

            if (element.ValueKind == JsonValueKind.True)
                return value is bool b && b;

            if (element.ValueKind == JsonValueKind.False)
                return value is bool b && !b;

            if (element.ValueKind == JsonValueKind.String)
                return value is string s && s == element.GetString();

            if (element.ValueKind == JsonValueKind.Number)
            {
                if (value is Int32 i32)
                    return i32 == element.GetInt32();

                if (value is Int16 i16)
                    return i16 == element.GetInt16();

                if (value is Int64 i64)
                    return i64 == element.GetInt64();

                if (value is Decimal dec)
                    return dec == element.GetDecimal();

                if (value is double dbl)
                    return dbl == element.GetDouble();

                return false;
            }

            return false;
        }
    }
}
