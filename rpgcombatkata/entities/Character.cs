using rpgcombatkata.events;
using rpgcombatkata.infrastructure;

namespace rpgcombatkata.entities {
    public class Character {
        public int Id { get; }
        public int Level { get; }
        public int Health { get; protected set; }
        public bool IsAlive { get; protected set; }

        protected Character() {
            Level = 1;
            Health = 1000;
            IsAlive = true;
            Id = CharacterIdGenerator.Next();
            
            EventBus.Subscribe<HealCharacter>(HandleHealing);
            EventBus.Subscribe<AttackCharacter>(HandleAttack);
        }
        
        public static Character Create() {
            return new Character();
        }
        
        private void HandleHealing(HealCharacter healCharacterEvent) {
            if (!IsAlive) return;
            if (healCharacterEvent.CharacterId != Id) return;
            Health += healCharacterEvent.Points;
        }

        private void HandleAttack(AttackCharacter attackCharacterEvent) {
            if (attackCharacterEvent.CharacterId != Id) return;
            Health -= attackCharacterEvent.Points;
            if (Health <= 0) Die();
        }

        private void Die() {
            IsAlive = false;
            
            EventBus.Unsubscribe<AttackCharacter>(HandleAttack);
        }
    }
    
    internal static class CharacterIdGenerator {

        private static int nextId = 1;
        
        public static int Next() {
            return nextId++;
        }
    }
}