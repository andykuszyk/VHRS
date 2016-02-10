using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHRS.Model;

namespace VHRS.Tests.Model
{
    [TestFixture]
    public sealed class RunnerTests
    {
        [Test]
        public void Name_IsInvalid_WhenEmpty()
        {
            var name = String.Empty;
            var runner = new Runner(name, "1/2");

            var error = runner.Error;

            error.Should().NotBeEmpty($"because {name} is empty");
        }

        [Test]
        public void Name_IsInvalid_WhenLongerThan18Characters()
        {
            var name = "abcdefghijklmnopqrstuvwxyz";
            var runner = new Runner(name, "1/2");

            var error = runner.Error;

            error.Should().NotBeEmpty($"because {name} is longer than 18 characters");
        }

        [Test]
        public void Name_IsValid_When18CharactersLong()
        {
            var name = "abcdefghijklmnopqr";
            var runner = new Runner(name, "1/2");

            var error = runner.Error;

            error.Should().BeEmpty($"because {name} is 18 characters long");
        }

        [Test]
        public void Name_IsValid_WhenShorterThan18Characters()
        {
            var name = "abcdefghi";
            var runner = new Runner(name, "1/2");

            var error = runner.Error;

            error.Should().BeEmpty($"because {name} is less than 18 characters long");
        }

        [Test]
        public void Odds_IsValid_WhenInCorrectFormat()
        {
            var odds = "1/2";
            var runner = new Runner("abcde", odds);

            var error = runner.Error;

            error.Should().BeEmpty($"becuase {odds} is a valid odds string");
        }

        [Test]
        public void Odds_IsInvalid_WhenContainsLetters()
        {
            var odds = "a/2";
            var runner = new Runner("abcde", odds);

            var error = runner.Error;

            error.Should().NotBeEmpty($"becuase {odds} contains a letter");
        }

        [Test]
        public void Odds_IsInvalid_WhenContainsRealAtStart()
        {
            var odds = "1.5/2";
            var runner = new Runner("abcde", odds);

            var error = runner.Error;

            error.Should().NotBeEmpty($"because {odds} contains a real number");
        }

        [Test]
        public void Odds_IsInvalid_WhenContainsRealAtEnd()
        {
            var odds = "1/2.5";
            var runner = new Runner("abcde", odds);

            var error = runner.Error;

            error.Should().NotBeEmpty($"because {odds} contains a real number");
        }

        [Test]
        public void Odds_IsInvalid_WhenDoesNotContainForwardSlash()
        {
            var odds = "12";
            var runner = new Runner("abcde", odds);

            var error = runner.Error;

            error.Should().NotBeEmpty($"because {odds} does not contain a forward slash");
        }
    }
}
