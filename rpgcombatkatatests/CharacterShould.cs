using FluentAssertions;
using NUnit.Framework;
using rpgcombatkata.entities;
using rpgcombatkata.events;
using rpgcombatkata.infrastructure;

namespace rpgcombatkatatests {
    [TestFixture]
    public class CharacterShould {
        private Character character;

        [SetUp]
        public void Setup() {
            character = Character.Create();
        }

        [Test]
        public void be_alive_with_level_one_and_1000_points_of_health_when_is_created() {
            character.IsAlive.Should().BeTrue();
            character.Level.Should().Be(1);
            character.Health.Should().Be(1000);
        }

        [Test]
        public void receive_damage_that_reduces_its_health() {
            EventBus.Raise(new AttackCharacter(character.Id, points: 100));
            
            character.IsAlive.Should().BeTrue();
            character.Health.Should().Be(900);
        }
    }
}