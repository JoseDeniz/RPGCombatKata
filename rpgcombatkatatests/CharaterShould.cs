using FluentAssertions;
using NUnit.Framework;

namespace rpgcombatkatatests {
    [TestFixture]
    public class CharacterShould {
        [SetUp]
        public void Setup() { }

        [Test]
        public void Test1() {
            var a = false;

            a.Should().BeFalse();
        }
    }
}