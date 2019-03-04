using rpgcombatkata.events;
using rpgcombatkata.infrastructure;

namespace rpgcombatkata.entities {
    public abstract class AttackableObject {
        public int Id { get; }
        public abstract int Health { get; protected set; }

        public AttackableObject() {
            Id = DestroyableObjectIdGenerator.Next();
            
            EventBus.Subscribe<AttackObject>(HandleAttack);
        }

        protected abstract void HandleAttack(AttackObject attackObjectEvent);
    }
    
    internal static class DestroyableObjectIdGenerator {

        private static int nextId = 1;
        
        public static int Next() {
            return nextId++;
        }
    }
}