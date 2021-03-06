using System.Collections.Generic;
using System.Linq;
using rpgcombatkata.events;
using rpgcombatkata.infrastructure;

namespace rpgcombatkata.entities.character {
    public abstract class Character : Attackable {
        public int Id { get; }
        public int Level { get; private set; }
        public int Health { get; private set; }
        public abstract int AttackRange { get; protected set; }
        public bool IsAlive { get; private set; }
        public IList<Faction> Factions { get; }

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
        
        public void Join(Faction faction) {
            Factions.Add(faction);
        }

        public void Leave(Faction faction) {
            Factions.Remove(faction);
        }
        
        private void HandleHealing(HealCharacter healCharacterEvent) {
            if (!IsAlive) return;
            if (!ItsMe(healCharacterEvent) && !IsFromSameFaction(healCharacterEvent.SourceCharacter)) return;
            Health += healCharacterEvent.Points;
            if (Health > MaxHealth) Health = MaxHealth;
        }

        private bool ItsMe(HealCharacter healCharacterEvent) {
            return healCharacterEvent.SourceCharacter.Id == Id && healCharacterEvent.TargetCharacter.Id == Id;
        }

        private void HandleAttack(AttackCharacter attackCharacterEvent) {
            if (IAmTheAttacker(attackCharacterEvent.SourceCharacter)) return;
            if (IAmNotTheAttacked(attackCharacterEvent.TargetCharacter)) return;
            if (IsFromSameFaction(attackCharacterEvent.SourceCharacter)) return;
            if (IAmIntoTheAttackRange(attackCharacterEvent)) return;
            Health -= CalculatePointsToDiscount(attackCharacterEvent);
            if (Health <= 0) Die();
        }

        private bool IAmTheAttacker(Character attacker) {
            return attacker.Id == Id;
        }
        
        private bool IAmNotTheAttacked(Character attacked) {
            return attacked.Id != Id;
        }
        
        private bool IsFromSameFaction(Character attacker) {
            return Factions.Any(attacker.IsInFaction);
        }
        
        private bool IsInFaction(Faction faction) {
            return Factions.Contains(faction);
        }
        
        private static bool IAmIntoTheAttackRange(AttackCharacter attackCharacterEvent) {
            return attackCharacterEvent.Range > attackCharacterEvent.SourceCharacter.AttackRange;
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