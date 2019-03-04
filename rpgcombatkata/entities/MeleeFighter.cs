namespace rpgcombatkata.entities {
    public class MeleeFighter : Character {
        public sealed override int AttackRange { get; protected set; }

        private MeleeFighter() {
            AttackRange = 2;
        }
        
        public static Character Create() {
            return new MeleeFighter();
        }
    }
}