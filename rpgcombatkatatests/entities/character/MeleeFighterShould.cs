using FluentAssertions;
using NUnit.Framework;
using rpgcombatkata.entities.character;

namespace rpgcombatkatatests.entities.character {
    [TestFixture]
    public class MeleeFighterShould {
        private Character meleeFighter;

        [SetUp]
        public void Setup() {
            meleeFighter = MeleeFighter.Create();
        }

        [Test]
        public void be_alive_with_level_one_and_1000_points_of_health_and_with_an_attack_range_of_two_when_is_created() {
            meleeFighter.IsAlive.Should().BeTrue();
            meleeFighter.Level.Should().Be(1);
            meleeFighter.Health.Should().Be(1000);
            meleeFighter.AttackRange.Should().Be(2);
        }
    }
}