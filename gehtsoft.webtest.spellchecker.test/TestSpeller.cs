using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Gehtsoft.Webtest.Spellchecker.Test
{
    public class TestSpeller
    {
        [Fact]
        public void Initialize()
        {
            var instance = SpellCheckerFactory.Instance;
            instance.Should().NotBeNull();

            var instance1 = SpellCheckerFactory.Instance;
            instance.Should().BeSameAs(instance1);
        }

        [Fact]
        public void InitializeDictionary()
        {
            SpellCheckerFactory.Instance.Reset();
            var instance = SpellCheckerFactory.Instance;
            instance.Initialize("en_US");
            instance["en_US"].Should().NotBeNull();
            var i1 = instance["en_US"];
            var i2 = instance["en_US"];
            i1.Should().BeSameAs(i2);
        }

        [Fact]
        public void InitializeExceptionList1()
        {
            SpellCheckerFactory.Instance.Reset();
            var instance = SpellCheckerFactory.Instance;
            instance.Initialize("en_US", new string[] { "a", "b", "c" });
            var i1 = instance["en_US"];
            instance.Initialize("en_US", new string[] { "a", "b", "c" });
            var i2 = instance["en_US"];
            i1.Should().BeSameAs(i2);
        }

        [Fact]
        public void InitializeExceptionList2()
        {
            SpellCheckerFactory.Instance.Reset();
            var instance = SpellCheckerFactory.Instance;
            instance.Initialize("en_US", new string[] { "a", "b", "c" });
            var i1 = instance["en_US"];
            instance.Initialize("en_US", new string[] { "c", "b", "a" });
            var i2 = instance["en_US"];
            i1.Should().BeSameAs(i2);
        }

        [Fact]
        public void InitializeExceptionList3()
        {
            SpellCheckerFactory.Instance.Reset();
            var instance = SpellCheckerFactory.Instance;
            instance.Initialize("en_US", new string[] { "a", "b", "c" });
            var i1 = instance["en_US"];
            instance.Initialize("en_US", new string[] { "d", "b", "a" });
            var i2 = instance["en_US"];
            i1.Should().NotBeSameAs(i2);
        }

        [Fact]
        public void InitializeExceptionList4()
        {
            SpellCheckerFactory.Instance.Reset();
            var instance = SpellCheckerFactory.Instance;
            instance.Initialize("en_US", null);
            var i1 = instance["en_US"];
            instance.Initialize("en_US", new string[] { "d", "b", "a" });
            var i2 = instance["en_US"];
            i1.Should().NotBeSameAs(i2);
        }

        [Fact]
        public void InitializeExceptionList5()
        {
            SpellCheckerFactory.Instance.Reset();
            var instance = SpellCheckerFactory.Instance;
            instance.Initialize("en_US", new string[] { "a", "b", "c" });
            var i1 = instance["en_US"];
            instance.Initialize("en_US", null);
            var i2 = instance["en_US"];
            i1.Should().NotBeSameAs(i2);
        }

        [Theory]
        [InlineData("Apple", null, true)]
        [InlineData("apple", null, true)]
        [InlineData("Aple", null, false)]
        [InlineData("aple", null, false)]
        [InlineData("Gehtsoft", null, false)]
        [InlineData("Gehtsoft", "gehtsoft", true)]

        public void Spell_OK(string word, string exceptions, bool correct)
        {
            var instance = SpellCheckerFactory.Instance;
            if (exceptions != null)
                instance.Initialize("en_US", exceptions.Split(new char[] { ' ', ',' }));
            instance["en_US"].SpellOne(word).Should().Be(correct);
            instance.Reset();
        }

        [Theory]
        [InlineData("Gehtsoft management decided to add aple support to pdfflow library", "Gehtsoft,aple,pdfflow")]
        [InlineData("Microsoft management decided to add apple support to windows library", "")]
        public void SpellMany(string text, string incorrectWords)
        {
            var instance = SpellCheckerFactory.Instance;
            var words = instance["en_US"].SpellMany(text);
            if (incorrectWords.Length == 0)
                words.Should().BeNullOrEmpty();
            else
            {
                words.Should().NotBeNullOrEmpty();
                var expectedWords = incorrectWords.Split(new char[] { ',' });
                words.Should().HaveCount(expectedWords.Length);
                foreach (var word in expectedWords)
                    words.Should().Contain(word);
            }
        }
    }

    public class TestAssertions
    {
        [Theory]
        [InlineData("text to be spelled", null)]
        [InlineData("Gehtsoft is unknown word", "gehtsoft")]
        public void TestSpelling_Ok(string text, string exemptions)
        {
            ((Action)(() => text.Should().BeSpelledCorrectly(exemptions: exemptions))).Should().NotThrow();
        }
    }
}
