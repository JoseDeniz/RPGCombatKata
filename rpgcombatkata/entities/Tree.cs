using rpgcombatkata.events;

namespace rpgcombatkata.entities {
    public class Tree : AttackableObject {
        public sealed override int Health { get; protected set; }

        public Tree(int health) {
            Health = health;
        }
        
        protected override void HandleAttack(AttackObject attackObjectEvent) {
            Health -= attackObjectEvent.Points;
        }
    }
}