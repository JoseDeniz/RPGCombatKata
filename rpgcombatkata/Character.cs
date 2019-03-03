namespace rpgcombatkata {
    public class Character {

        public bool IsAlive { get; }
        public int Level { get; }
        public int Health { get; }

        private Character() {
            IsAlive = true;
            Level = 1;
            Health = 1000;
        }
            
        public static Character Create() {
            return new Character();
        }
    }
}