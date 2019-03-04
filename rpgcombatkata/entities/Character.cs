using rpgcombatkata.events;
using rpgcombatkata.infrastructure;

namespace rpgcombatkata.entities {
    public class Character {
        public int Id { get; }
        public int Level { get; private set; }
        public int Health { get; private set; }
        public bool IsAlive { get; private set; }

        private const int MaxHealth = 1000;

        private Character() {
            Level = 1;
            Health = MaxHealth;
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
            if (healCharacterEvent.SourceCharacterId != Id || healCharacterEvent.TargetCharacterId != Id) return;
            Health += healCharacterEvent.Points;
            if (Health > MaxHealth) Health = MaxHealth;
        }

        private void HandleAttack(AttackCharacter attackCharacterEvent) {
            if (attackCharacterEvent.SourceCharacter.Id == Id) return;
            if (attackCharacterEvent.TargetCharacter.Id != Id) return;
            Health -= CalculatePointsToDiscount(attackCharacterEvent);
            if (Health <= 0) Die();
        }

        private int CalculatePointsToDiscount(AttackCharacter attackCharacterEvent) {
            var points = attackCharacterEvent.Points;
            if ((Level - attackCharacterEvent.SourceCharacter.Level) >= 5) points /= 2;
            return points;
        }

        private void Die() {
            IsAlive = false;
            
            EventBus.Unsubscribe<AttackCharacter>(HandleAttack);
        }

        public void IncreaseLevel() {
            Level++;
        }
    }
    
    internal static class CharacterIdGenerator {

        private static int nextId = 1;
        
        public static int Next() {
            return nextId++;
        }
    }
}