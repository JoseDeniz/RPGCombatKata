using System.Collections.Generic;
using rpgcombatkata.events;
using rpgcombatkata.infrastructure;

namespace rpgcombatkata.entities {
    public abstract class Character {
        public int Id { get; }
        public int Level { get; private set; }
        public int Health { get; private set; }
        public abstract int AttackRange { get; protected set; }
        public bool IsAlive { get; private set; }
        public IEnumerable<Faction> Factions { get; }

        private const int MaxHealth = 1000;

        protected Character() {
            Id = CharacterIdGenerator.Next();
            Level = 1;
            IsAlive = true;
            Health = MaxHealth;
            Factions = new List<Faction>();
            
            EventBus.Subscribe<HealCharacter>(HandleHealing);
            EventBus.Subscribe<AttackCharacter>(HandleAttack);
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
            if (attackCharacterEvent.Range > attackCharacterEvent.SourceCharacter.AttackRange) return;
            Health -= CalculatePointsToDiscount(attackCharacterEvent);
            if (Health <= 0) Die();
        }

        private int CalculatePointsToDiscount(AttackCharacter attackCharacterEvent) {
            if ((Level - attackCharacterEvent.SourceCharacter.Level) >= 5) return attackCharacterEvent.Points / 2;
            if ((attackCharacterEvent.SourceCharacter.Level - Level) >= 5) return attackCharacterEvent.Points * 2;
            return attackCharacterEvent.Points;
        }

        private void Die() {
            IsAlive = false;
            Health = 0;
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