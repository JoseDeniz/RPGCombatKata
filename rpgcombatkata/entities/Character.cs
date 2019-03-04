using rpgcombatkata.events;
using rpgcombatkata.infrastructure;

namespace rpgcombatkata.entities {
    public class Character {
        public int Id { get; }
        public int Level { get; }
        public int Health { get; private set; }
        public bool IsAlive { get; private set; }

        private Character() {
            Level = 1;
            Health = 1000;
            IsAlive = true;
            Id = CharacterIdGenerator.Next();
            
            EventBus.Subscribe<AttackCharacter>(HandleAttack);
            EventBus.Subscribe<HealCharacter>(HandleHealing);
        }

        private void HandleAttack(AttackCharacter attackCharacterEvent) {
            if (attackCharacterEvent.CharacterId != Id) return;
            Health -= attackCharacterEvent.Points;
            if (Health <= 0) Die();
        }

        private void HandleHealing(HealCharacter healCharacterEvent) {
            Health += healCharacterEvent.Points;
        }

        private void Die() {
            IsAlive = false;
            
            EventBus.Unsubscribe<AttackCharacter>(HandleAttack);
        }

        public static Character Create() {
            return new Character();
        }
    }
    
    internal static class CharacterIdGenerator {

        private static int nextId = 1;
        
        public static int Next() {
            return nextId++;
        }
    }
}