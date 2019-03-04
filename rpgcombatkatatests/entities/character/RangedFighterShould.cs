using FluentAssertions;
using NUnit.Framework;
using rpgcombatkata.entities.character;

namespace rpgcombatkatatests.entities.character {
    [TestFixture]
    public class RangedFighterShould {
        private Character rangedFighter;

        [SetUp]
        public void Setup() {
            rangedFighter = RangedFighter.Create();
        }

        [Test]
        public void be_alive_with_level_one_and_1000_points_of_health_and_with_an_attack_range_of_twenty_when_is_created() {
            rangedFighter.IsAlive.Should().BeTrue();
            rangedFighter.Level.Should().Be(1);
            rangedFighter.Health.Should().Be(1000);
            rangedFighter.AttackRange.Should().Be(20);
        }
    }
}