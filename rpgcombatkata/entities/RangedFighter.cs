namespace rpgcombatkata.entities {
    public class RangedFighter : Character {
        public sealed override int AttackRange { get; protected set; }

        private RangedFighter() {
            AttackRange = 20;
        }
        
        public static Character Create() {
            return new RangedFighter();
        }
    }
}