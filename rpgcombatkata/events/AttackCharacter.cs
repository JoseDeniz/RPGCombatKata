namespace rpgcombatkata.events {
    public class AttackCharacter {
        public int SourceCharacterId { get; }
        public int TargetCharacterId { get; }
        public int Points { get; }
        
        public AttackCharacter(int sourceCharacterId, int targetCharacterId, int points) {
            SourceCharacterId = sourceCharacterId;
            TargetCharacterId = targetCharacterId;
            Points = points;
        }
    }
}