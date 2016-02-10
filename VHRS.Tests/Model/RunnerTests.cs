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
        public void Name_IsInvalid_WhenLongerThan18Characters()
        {
            var name = "abcdefghijklmnopqrstuvwxyz";
            var runner = new Runner(name, "1/2");

            var error = runner.Error;

            error.Should().NotBeNull($"because {name} is longer than 18 characters");
        }

        [Test]
        public void Name_IsValid_When18CharactersLong()
        {
            var name = "abcdefghijklmnopqr";
            var runner = new Runner(name, "1/2");

            var error = runner.Error;

            error.Should().BeNull($"because {name} is 18 characters long");
        }

        [Test]
        public void Name_IsValid_WhenShorterThan18Characters()
        {
            var name = "abcdefghi";
            var runner = new Runner(name, "1/2");

            var error = runner.Error;

            error.Should().BeNull($"because {name} is less than 18 characters long");
        }
    }
}
