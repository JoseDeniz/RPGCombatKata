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
        
        [Test]
        public void receive_damage_that_reduces_its_health_only_if_has_same_id() {
            EventBus.Raise(new AttackCharacter(characterId: 123, points: 100));
            EventBus.Raise(new AttackCharacter(character.Id, points: 100));
            
            character.IsAlive.Should().BeTrue();
            character.Health.Should().Be(900);
        }

        [Test]
        public void dies_when_the_health_becomes_0() {
            EventBus.Raise(new AttackCharacter(character.Id, points: 1000));
            
            character.IsAlive.Should().BeFalse();
            character.Health.Should().Be(0);
        }
        
        [Test]
        public void receive_health_from_other_characters_not_over_1000() {
            EventBus.Raise(new HealCharacter(character.Id, points: 100));
            
            character.Health.Should().Be(1000);
        }
        
        [Test]
        public void receive_health_from_other_characters_only_if_has_same_id() {
            EventBus.Raise(new AttackCharacter(character.Id, points: 100));
            EventBus.Raise(new HealCharacter(character.Id, points: 100));
            EventBus.Raise(new HealCharacter(characterId: 123, points: 100));
            
            character.Health.Should().Be(1000);
        }
        
        [Test]
        public void can_not_receive_health_when_is_death() {
            EventBus.Raise(new AttackCharacter(character.Id, points: 1000));
            EventBus.Raise(new HealCharacter(character.Id, points: 100));
            
            character.Health.Should().Be(0);
            character.IsAlive.Should().BeFalse();
        }
    }
}