using rpgcombatkata.events;
using rpgcombatkata.infrastructure;

namespace rpgcombatkata.entities.objects {
    public abstract class AttackableObject : Attackable {
        public int Id { get; }
        public int Health { get; private set; }

        public AttackableObject(int health) {
            Id = DestroyableObjectIdGenerator.Next();
            Health = health;
            
            EventBus.Subscribe<AttackObject>(HandleAttack);
        }

        private void HandleAttack(AttackObject attackObjectEvent) {
            Health -= attackObjectEvent.Points;
            if (Health <= 0) Destroy();
        }

        private void Destroy() {
            Health = 0;
            EventBus.Raise(new DestroyedObject(Id));
            EventBus.Unsubscribe<AttackObject>(HandleAttack);
        }
    }

    internal static class DestroyableObjectIdGenerator {

        private static int nextId = 1;
        
        public static int Next() {
            return nextId++;
        }
    }
}