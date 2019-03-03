using rpgcombatkata.events;
using rpgcombatkata.infrastructure;

namespace rpgcombatkata.entities {
    public class Character {
        public int Id { get; }
        public int Level { get; }
        public int Health { get; private set; }
        public bool IsAlive { get; }

        private Character() {
            Level = 1;
            Health = 1000;
            IsAlive = true;
            Id = CharacterIdGenerator.Next();
            
            EventBus.Subscribe<AttackCharacter>(HandleAttack);
        }

        private void HandleAttack(AttackCharacter attackCharacterEvent) {
            if (attackCharacterEvent.CharacterId == Id) {
                Health -= attackCharacterEvent.Points;
            }
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