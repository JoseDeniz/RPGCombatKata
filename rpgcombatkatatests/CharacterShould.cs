using FluentAssertions;
using NUnit.Framework;
using rpgcombatkata.entities;
using rpgcombatkata.events;
using rpgcombatkata.infrastructure;

namespace rpgcombatkatatests {
    [TestFixture]
    public class CharacterShould {
        private Character character1;
        private Character character2;

        [SetUp]
        public void Setup() {
            character1 = Character.Create();
            character2 = Character.Create();
        }

        [Test]
        public void be_alive_with_level_one_and_1000_points_of_health_when_is_created() {
            character1.IsAlive.Should().BeTrue();
            character1.Level.Should().Be(1);
            character1.Health.Should().Be(1000);
        }

        [Test]
        public void receive_damage_that_reduces_its_health() {
            EventBus.Raise(new AttackCharacter(character1.Id, character2.Id, points: 100));
            
            character2.IsAlive.Should().BeTrue();
            character2.Health.Should().Be(900);
        }
        
        [Test]
        public void receive_damage_that_reduces_its_health_only_if_has_same_id() {
            EventBus.Raise(new AttackCharacter(character1.Id,  123, points: 100));
            EventBus.Raise(new AttackCharacter(character1.Id, character2.Id, points: 100));
            
            character2.IsAlive.Should().BeTrue();
            character2.Health.Should().Be(900);
        }

        [Test]
        public void dies_when_the_health_becomes_0() {
            EventBus.Raise(new AttackCharacter(character1.Id, character2.Id, points: 1000));
            
            character2.IsAlive.Should().BeFalse();
            character2.Health.Should().Be(0);
        }
        
        [Test]
        public void can_only_receive_health_from_itself() {
            EventBus.Raise(new AttackCharacter(character2.Id, character1.Id, points: 900));
            EventBus.Raise(new HealCharacter(character1.Id, character1.Id, points: 100));
            EventBus.Raise(new HealCharacter(character2.Id, character1.Id, points: 100));
            
            character1.Health.Should().Be(200);
        }
        
        [Test]
        public void can_only_receive_health_from_itself_not_over_1000() {
            EventBus.Raise(new HealCharacter(character1.Id, character1.Id, points: 100));
            
            character1.Health.Should().Be(1000);
        }
        
        [Test]
        public void can_not_receive_health_when_is_death() {
            EventBus.Raise(new AttackCharacter(character1.Id, character2.Id, points: 1000));
            EventBus.Raise(new HealCharacter(character1.Id, character2.Id, points: 100));
            
            character2.Health.Should().Be(0);
            character2.IsAlive.Should().BeFalse();
        }

        [Test]
        public void can_not_damage_itself() {
            EventBus.Raise(new AttackCharacter(character1.Id, character1.Id, points: 1000));
            
            character1.IsAlive.Should().BeTrue();
            character1.Health.Should().Be(1000);
        }
    }
}