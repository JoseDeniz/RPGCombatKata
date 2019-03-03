using FluentAssertions;
using NUnit.Framework;
using rpgcombatkata;

namespace rpgcombatkatatests {
    [TestFixture]
    public class CharacterShould {
        [SetUp]
        public void Setup() { }

        [Test]
        public void be_alive_with_level_one_and_1000_points_of_health() {
            var character = Character.Create();

            character.IsAlive.Should().BeTrue();
            character.Level.Should().Be(1);
            character.Health.Should().Be(1000);
        }
    }
}