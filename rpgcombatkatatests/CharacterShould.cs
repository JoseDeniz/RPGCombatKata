using System.Collections.Generic;
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
            character1 = ACharacter.Create();
            character2 = ACharacter.Create();
        }

        [Test]
        public void be_alive_with_level_one_and_1000_points_of_health_with_an_attack_range_and_be_in_any_faction_when_is_created() {
            character1.IsAlive.Should().BeTrue();
            character1.Level.Should().Be(1);
            character1.Health.Should().Be(1000);
            character1.AttackRange.Should().Be(1);
            character1.Factions.Should().BeEmpty();
        }

        [Test]
        public void receive_damage_that_reduces_its_health_only_in_range() {
            var rangedFighter = RangedFighter.Create();
            EventBus.Raise(WhenAttackCharacter(rangedFighter, character1, 100, 15));
            
            character1.IsAlive.Should().BeTrue();
            character1.Health.Should().Be(900);
        }
        
        [Test]
        public void do_not_receive_damage_when_is_not_in_range() {
            EventBus.Raise(WhenAttackCharacter(character1, character2, 100, 200));
            
            character2.IsAlive.Should().BeTrue();
            character2.Health.Should().Be(1000);
        }
        
        [Test]
        public void if_the_target_is_five_or_more_Levels_above_the_attacker_Damage_is_reduced_by_50_percent() {
            character2.IncreaseLevel();
            character2.IncreaseLevel();
            character2.IncreaseLevel();
            character2.IncreaseLevel();
            character2.IncreaseLevel();
            EventBus.Raise(WhenAttackCharacter(character1, character2, points: 1000));
            
            character2.IsAlive.Should().BeTrue();
            character2.Health.Should().Be(500);
        }
        
        [Test]
        public void if_the_target_is_five_or_more_Levels_below_the_attacker_Damage_is_increased_by_50_percent() {
            character1.IncreaseLevel();
            character1.IncreaseLevel();
            character1.IncreaseLevel();
            character1.IncreaseLevel();
            character1.IncreaseLevel();
            EventBus.Raise(WhenAttackCharacter(character1, character2, points: 250));
            
            character2.IsAlive.Should().BeTrue();
            character2.Health.Should().Be(500);
        }

        [Test]
        public void dies_when_the_health_becomes_0() {
            EventBus.Raise(WhenAttackCharacter(character1, character2, points: 1000));
            
            character2.IsAlive.Should().BeFalse();
            character2.Health.Should().Be(0);
        }
        
        [Test]
        public void can_only_receive_health_from_itself() {
            EventBus.Raise(WhenAttackCharacter(character2, character1, points: 900));
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
            EventBus.Raise(WhenAttackCharacter(character1, character2, 1000));
            EventBus.Raise(new HealCharacter(character1.Id, character2.Id, points: 100));
            
            character2.Health.Should().Be(0);
            character2.IsAlive.Should().BeFalse();
        }

        [Test]
        public void can_not_damage_itself() {
            EventBus.Raise(WhenAttackCharacter(character1, character1, points: 1000));
            
            character1.IsAlive.Should().BeTrue();
            character1.Health.Should().Be(1000);
        }

        [Test]
        public void can_join_to_a_faction() {
            character1.Join(new AFaction());
            
            character1.Factions.Should().BeEquivalentTo(new List<Faction> { new AFaction() });
        }
        
        [Test]
        public void can_leave_from_a_faction() {
            var faction = new AFaction();
            character1.Join(faction);

            character1.Leave(faction);
            
            character1.Factions.Should().BeEmpty();
        }

        [Test]
        public void allies_of_a_faction_can_not_damage_each_others() {
            var faction = new AFaction();
            character1.Join(faction);
            character2.Join(faction);
            
            EventBus.Raise(WhenAttackCharacter(character1, character2, 1000));

            character2.Health.Should().Be(1000);
        }
        
        private static AttackCharacter WhenAttackCharacter(Character sourceCharacter, Character targetCharacter, int points, int range = 1) {
            return new AttackCharacter(sourceCharacter, targetCharacter, points, range);
        }

        private class ACharacter : Character {
            public sealed override int AttackRange { get; protected set; }

            private ACharacter() {
                AttackRange = 1;
            }


            public static Character Create() {
                return new ACharacter();
            }
        }
        
        private class AFaction : Faction {
            public override string Name { get; }

            public AFaction() {
                Name = "AFaction";
            }
        }
    }
}